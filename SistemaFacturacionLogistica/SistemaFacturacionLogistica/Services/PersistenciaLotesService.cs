using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using SistemaFacturacionLogistica.Models;

namespace SistemaFacturacionLogistica.Services
{
    public class PersistenciaLotesService
    {
        private const string Esquema = @"
            CREATE TABLE IF NOT EXISTS Lotes (
                Id INTEGER PRIMARY KEY,
                FechaUtc TEXT NOT NULL,
                PrecioBaseKg TEXT NOT NULL,
                TasaIva TEXT NOT NULL,
                CostoFijoEnvio TEXT NOT NULL,
                Total TEXT NOT NULL
            );
            CREATE TABLE IF NOT EXISTS Paquetes (
                Id INTEGER PRIMARY KEY,
                LoteId INTEGER NOT NULL REFERENCES Lotes(Id),
                Codigo TEXT NOT NULL,
                Cliente TEXT NOT NULL,
                Zona TEXT NOT NULL,
                Peso TEXT NOT NULL,
                ValorDeclarado TEXT NOT NULL,
                Subtotal TEXT NOT NULL,
                RecargoZona TEXT NOT NULL,
                Penalizacion TEXT NOT NULL,
                Seguro TEXT NOT NULL,
                Impuesto TEXT NOT NULL,
                Total TEXT NOT NULL
            );
            CREATE TABLE IF NOT EXISTS ErroresLote (
                Id INTEGER PRIMARY KEY,
                LoteId INTEGER NOT NULL REFERENCES Lotes(Id),
                NumeroLinea INTEGER NOT NULL,
                Linea TEXT NOT NULL,
                Motivo TEXT NOT NULL
            );
            CREATE INDEX IF NOT EXISTS IX_Paquetes_LoteId ON Paquetes(LoteId);
            CREATE INDEX IF NOT EXISTS IX_ErroresLote_LoteId ON ErroresLote(LoteId);";

        private readonly string _ruta;
        private const string EsquemaRecepciones = @"
            CREATE TABLE IF NOT EXISTS Recepciones (
                PaqueteId INTEGER PRIMARY KEY REFERENCES Paquetes(Id) ON DELETE CASCADE,
                FechaUtc TEXT NOT NULL
            );";

        public PersistenciaLotesService()
            : this(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "SistemaFacturacionLogistica", "facturacion.db"))
        {
        }

        public PersistenciaLotesService(string ruta)
        {
            _ruta = Path.GetFullPath(ruta);
        }

        public void Guardar(ResultadoLote lote, Configuracion config)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_ruta));
            var cadena = new SQLiteConnectionStringBuilder { DataSource = _ruta, ForeignKeys = true };
            using (var conexion = new SQLiteConnection(cadena.ConnectionString))
            {
                conexion.Open();
                using (SQLiteTransaction transaccion = conexion.BeginTransaction())
                {
                    using (var comando = new SQLiteCommand(Esquema, conexion, transaccion))
                        comando.ExecuteNonQuery();

                    long loteId = InsertarLote(conexion, transaccion, lote, config);
                    InsertarPaquetes(conexion, transaccion, loteId, lote);
                    InsertarErrores(conexion, transaccion, loteId, lote);
                    transaccion.Commit();
                }
            }
        }

        public IReadOnlyList<ResumenLote> ConsultarHistorial()
        {
            var lotes = new List<ResumenLote>();
            try
            {
                File.GetAttributes(_ruta);
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is DirectoryNotFoundException)
            {
                return lotes;
            }

            const string sql = @"
                SELECT l.Id, l.FechaUtc,
                    (SELECT COUNT(*) FROM Paquetes p WHERE p.LoteId = l.Id),
                    (SELECT COUNT(*) FROM ErroresLote e WHERE e.LoteId = l.Id),
                    l.Total
                FROM Lotes l
                ORDER BY l.Id DESC;";
            using (var conexion = CrearConexionLectura())
            using (var comando = new SQLiteCommand(sql, conexion))
            {
                conexion.Open();
                using (var lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        var fecha = DateTime.ParseExact(lector.GetString(1), "O",
                            CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
                        lotes.Add(new ResumenLote(lector.GetInt64(0), fecha,
                            lector.GetInt32(2), lector.GetInt32(3), LeerDecimal(lector, 4)));
                    }
                }
            }

            return lotes;
        }

        public Configuracion ConsultarTarifas(ResumenLote lote)
        {
            using (var conexion = CrearConexionLectura())
            using (var comando = new SQLiteCommand(
                "SELECT PrecioBaseKg, TasaIva, CostoFijoEnvio FROM Lotes WHERE Id = @lote AND FechaUtc = @fecha;", conexion))
            {
                conexion.Open();
                IdentificarLote(comando, lote);
                using (var lector = comando.ExecuteReader())
                {
                    if (!lector.Read())
                        throw new InvalidDataException();

                    return new Configuracion
                    {
                        PrecioBaseKg = LeerDecimal(lector, 0),
                        Iva = LeerDecimal(lector, 1),
                        CostoFijoEnvio = LeerDecimal(lector, 2)
                    };
                }
            }
        }

        public void Actualizar(ResumenLote lote, ResultadoLote resultado)
        {
            using (var conexion = CrearConexionEscritura())
            {
                conexion.Open();
                using (var transaccion = conexion.BeginTransaction())
                {
                    using (var comando = new SQLiteCommand(
                        "UPDATE Lotes SET Total = @total WHERE Id = @lote AND FechaUtc = @fecha;", conexion, transaccion))
                    {
                        comando.Parameters.Add("@total", DbType.String).Value = DecimalExacto(resultado.Total);
                        IdentificarLote(comando, lote);
                        if (comando.ExecuteNonQuery() != 1)
                            throw new InvalidDataException();
                    }

                    var conservar = IdentificarPaquetesRecibidos(conexion, transaccion, lote.Id, resultado);
                    EliminarDetalle(conexion, transaccion, lote.Id, conservarRecepciones: true);
                    InsertarPaquetes(conexion, transaccion, lote.Id, resultado, conservar);
                    InsertarErrores(conexion, transaccion, lote.Id, resultado);
                    transaccion.Commit();
                }
            }
        }

        public void Eliminar(ResumenLote lote)
        {
            using (var conexion = CrearConexionEscritura())
            {
                conexion.Open();
                using (var transaccion = conexion.BeginTransaction())
                {
                    EliminarDetalle(conexion, transaccion, lote.Id);
                    using (var comando = new SQLiteCommand(
                        "DELETE FROM Lotes WHERE Id = @lote AND FechaUtc = @fecha;", conexion, transaccion))
                    {
                        IdentificarLote(comando, lote);
                        if (comando.ExecuteNonQuery() != 1)
                            throw new InvalidDataException();
                    }

                    transaccion.Commit();
                }
            }
        }

        private static void IdentificarLote(SQLiteCommand comando, ResumenLote lote)
        {
            comando.Parameters.Add("@lote", DbType.Int64).Value = lote.Id;
            comando.Parameters.Add("@fecha", DbType.String).Value =
                lote.FechaUtc.ToString("O", CultureInfo.InvariantCulture);
        }

        private SQLiteConnection CrearConexionEscritura()
        {
            var cadena = new SQLiteConnectionStringBuilder
            {
                DataSource = _ruta,
                ForeignKeys = true,
                FailIfMissing = true
            };
            return new SQLiteConnection(cadena.ConnectionString);
        }

        private static void EliminarDetalle(SQLiteConnection conexion, SQLiteTransaction transaccion,
            long loteId, bool conservarRecepciones = false)
        {
            string filtroRecepcion = conservarRecepciones
                ? " AND Id NOT IN (SELECT PaqueteId FROM Recepciones)"
                : "";
            string sql = "DELETE FROM Paquetes WHERE LoteId = @lote" + filtroRecepcion +
                "; DELETE FROM ErroresLote WHERE LoteId = @lote;";
            using (var comando = new SQLiteCommand(sql, conexion, transaccion))
            {
                comando.Parameters.Add("@lote", DbType.Int64).Value = loteId;
                comando.ExecuteNonQuery();
            }
        }

        public void RegistrarRecepcion(ResumenLote lote, Paquete paquete)
        {
            using (var conexion = CrearConexionEscritura())
            {
                conexion.Open();
                using (var transaccion = conexion.BeginTransaction())
                {
                    using (var crear = new SQLiteCommand(EsquemaRecepciones, conexion, transaccion))
                        crear.ExecuteNonQuery();

                    const string sql = @"
                        INSERT INTO Recepciones (PaqueteId, FechaUtc)
                        SELECT p.Id, @recepcion FROM Paquetes p JOIN Lotes l ON l.Id = p.LoteId
                        WHERE p.Id = @paquete AND l.Id = @lote AND l.FechaUtc = @fecha
                          AND p.Codigo = @codigo AND p.Cliente = @cliente AND p.Zona = @zona
                          AND p.Peso = @peso AND p.ValorDeclarado = @valor
                        ON CONFLICT(PaqueteId) DO NOTHING;";
                    using (var comando = new SQLiteCommand(sql, conexion, transaccion))
                    {
                        IdentificarLote(comando, lote);
                        comando.Parameters.Add("@paquete", DbType.Int64).Value = paquete.IdPersistido;
                        comando.Parameters.Add("@codigo", DbType.String).Value = paquete.Codigo;
                        comando.Parameters.Add("@cliente", DbType.String).Value = paquete.Cliente;
                        comando.Parameters.Add("@zona", DbType.String).Value = paquete.Zona;
                        comando.Parameters.Add("@peso", DbType.String).Value = DecimalExacto(paquete.Peso);
                        comando.Parameters.Add("@valor", DbType.String).Value = DecimalExacto(paquete.ValorDeclarado);
                        comando.Parameters.Add("@recepcion", DbType.String).Value =
                            DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture);
                        if (comando.ExecuteNonQuery() != 1)
                            throw new InvalidDataException();
                    }

                    transaccion.Commit();
                }
            }
        }

        private static HashSet<Paquete> IdentificarPaquetesRecibidos(SQLiteConnection conexion,
            SQLiteTransaction transaccion, long loteId, ResultadoLote resultado)
        {
            using (var crear = new SQLiteCommand(EsquemaRecepciones, conexion, transaccion))
                crear.ExecuteNonQuery();

            var conservar = new HashSet<Paquete>();
            const string sql = @"
                SELECT p.Codigo, p.Cliente, p.Zona, p.Peso, p.ValorDeclarado
                FROM Paquetes p JOIN Recepciones r ON r.PaqueteId = p.Id WHERE p.LoteId = @lote;";
            using (var comando = new SQLiteCommand(sql, conexion, transaccion))
            {
                comando.Parameters.Add("@lote", DbType.Int64).Value = loteId;
                using (var lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        string codigo = lector.GetString(0);
                        string cliente = lector.GetString(1);
                        string zona = lector.GetString(2);
                        decimal peso = LeerDecimal(lector, 3);
                        decimal valor = LeerDecimal(lector, 4);
                        Paquete coincidencia = null;
                        foreach (Paquete paquete in resultado.Paquetes)
                        {
                            if (!conservar.Contains(paquete) && paquete.Codigo == codigo &&
                                paquete.Cliente == cliente && paquete.Zona == zona &&
                                paquete.Peso == peso && paquete.ValorDeclarado == valor)
                            {
                                coincidencia = paquete;
                                break;
                            }
                        }

                        if (coincidencia == null)
                            throw new InvalidOperationException();

                        conservar.Add(coincidencia);
                    }
                }
            }

            return conservar;
        }

        public ResultadoLote ConsultarDetalle(ResumenLote lote)
        {
            var paquetes = new List<Paquete>();
            var errores = new List<ErrorProcesamiento>();
            using (var conexion = CrearConexionLectura())
            {
                conexion.Open();
                bool hayRecepciones;
                using (var esquema = new SQLiteCommand(
                    "SELECT 1 FROM sqlite_master WHERE type = 'table' AND name = 'Recepciones';", conexion))
                    hayRecepciones = esquema.ExecuteScalar() != null;

                string fechaRecepcion = hayRecepciones ? "r.FechaUtc" : "NULL";
                string union = hayRecepciones ? " LEFT JOIN Recepciones r ON r.PaqueteId = p.Id" : "";
                string sql = @"
                    SELECT p.Codigo, p.Cliente, p.Zona, p.Peso, p.ValorDeclarado, p.Subtotal,
                        p.RecargoZona, p.Penalizacion, p.Seguro, p.Impuesto, p.Total, p.Id, " +
                    fechaRecepcion + " FROM Paquetes p" + union +
                    " WHERE p.LoteId = @lote ORDER BY p.Id;";
                using (var comando = new SQLiteCommand(sql, conexion))
                {
                    comando.Parameters.Add("@lote", DbType.Int64).Value = lote.Id;
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            paquetes.Add(new Paquete
                            {
                                Codigo = lector.GetString(0),
                                Cliente = lector.GetString(1),
                                Zona = lector.GetString(2),
                                Peso = LeerDecimal(lector, 3),
                                ValorDeclarado = LeerDecimal(lector, 4),
                                Subtotal = LeerDecimal(lector, 5),
                                RecargoZona = LeerDecimal(lector, 6),
                                Penalizacion = LeerDecimal(lector, 7),
                                Seguro = LeerDecimal(lector, 8),
                                Impuesto = LeerDecimal(lector, 9),
                                Total = LeerDecimal(lector, 10),
                                IdPersistido = lector.GetInt64(11),
                                FechaRecepcionUtc = lector.IsDBNull(12) ? (DateTime?)null :
                                    DateTime.ParseExact(lector.GetString(12), "O", CultureInfo.InvariantCulture,
                                        DateTimeStyles.RoundtripKind)
                            });
                        }
                    }
                }

                using (var comando = new SQLiteCommand(
                    "SELECT NumeroLinea, Linea, Motivo FROM ErroresLote WHERE LoteId = @lote ORDER BY Id;", conexion))
                {
                    comando.Parameters.Add("@lote", DbType.Int64).Value = lote.Id;
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                            errores.Add(new ErrorProcesamiento(lector.GetInt32(0), lector.GetString(1), lector.GetString(2)));
                    }
                }
            }

            Paquete[] ordenados = paquetes.ToArray();
            ProcesadorPaquetesService.OrdenarPorTotal(ordenados, ordenados.Length);
            return new ResultadoLote(ordenados, errores.ToArray(), lote.Total);
        }

        private SQLiteConnection CrearConexionLectura()
        {
            var cadena = new SQLiteConnectionStringBuilder { DataSource = _ruta, ReadOnly = true };
            return new SQLiteConnection(cadena.ConnectionString);
        }

        private static decimal LeerDecimal(SQLiteDataReader lector, int columna)
        {
            return decimal.Parse(lector.GetString(columna), NumberStyles.Number, CultureInfo.InvariantCulture);
        }

        private static long InsertarLote(SQLiteConnection conexion, SQLiteTransaction transaccion,
            ResultadoLote lote, Configuracion config)
        {
            const string sql = @"
                INSERT INTO Lotes (FechaUtc, PrecioBaseKg, TasaIva, CostoFijoEnvio, Total)
                VALUES (@fecha, @precio, @iva, @costo, @total)
                RETURNING Id;";
            using (var comando = new SQLiteCommand(sql, conexion, transaccion))
            {
                comando.Parameters.Add("@fecha", DbType.String).Value =
                    DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture);
                comando.Parameters.Add("@precio", DbType.String).Value = DecimalExacto(config.PrecioBaseKg);
                comando.Parameters.Add("@iva", DbType.String).Value = DecimalExacto(config.Iva);
                comando.Parameters.Add("@costo", DbType.String).Value = DecimalExacto(config.CostoFijoEnvio);
                comando.Parameters.Add("@total", DbType.String).Value = DecimalExacto(lote.Total);
                return Convert.ToInt64(comando.ExecuteScalar(), CultureInfo.InvariantCulture);
            }
        }

        private static void InsertarPaquetes(SQLiteConnection conexion, SQLiteTransaction transaccion,
            long loteId, ResultadoLote lote, HashSet<Paquete> conservar = null)
        {
            const string sql = @"
                INSERT INTO Paquetes (LoteId, Codigo, Cliente, Zona, Peso, ValorDeclarado,
                    Subtotal, RecargoZona, Penalizacion, Seguro, Impuesto, Total)
                VALUES (@lote, @codigo, @cliente, @zona, @peso, @valor,
                    @subtotal, @recargo, @penalizacion, @seguro, @impuesto, @total);";
            using (var comando = new SQLiteCommand(sql, conexion, transaccion))
            {
                comando.Parameters.Add("@lote", DbType.Int64).Value = loteId;
                var codigo = comando.Parameters.Add("@codigo", DbType.String);
                var cliente = comando.Parameters.Add("@cliente", DbType.String);
                var zona = comando.Parameters.Add("@zona", DbType.String);
                var peso = comando.Parameters.Add("@peso", DbType.String);
                var valor = comando.Parameters.Add("@valor", DbType.String);
                var subtotal = comando.Parameters.Add("@subtotal", DbType.String);
                var recargo = comando.Parameters.Add("@recargo", DbType.String);
                var penalizacion = comando.Parameters.Add("@penalizacion", DbType.String);
                var seguro = comando.Parameters.Add("@seguro", DbType.String);
                var impuesto = comando.Parameters.Add("@impuesto", DbType.String);
                var total = comando.Parameters.Add("@total", DbType.String);
                foreach (Paquete paquete in lote.Paquetes)
                {
                    if (conservar != null && conservar.Contains(paquete))
                        continue;

                    codigo.Value = paquete.Codigo;
                    cliente.Value = paquete.Cliente;
                    zona.Value = paquete.Zona;
                    peso.Value = DecimalExacto(paquete.Peso);
                    valor.Value = DecimalExacto(paquete.ValorDeclarado);
                    subtotal.Value = DecimalExacto(paquete.Subtotal);
                    recargo.Value = DecimalExacto(paquete.RecargoZona);
                    penalizacion.Value = DecimalExacto(paquete.Penalizacion);
                    seguro.Value = DecimalExacto(paquete.Seguro);
                    impuesto.Value = DecimalExacto(paquete.Impuesto);
                    total.Value = DecimalExacto(paquete.Total);
                    comando.ExecuteNonQuery();
                }
            }
        }

        private static void InsertarErrores(SQLiteConnection conexion, SQLiteTransaction transaccion,
            long loteId, ResultadoLote lote)
        {
            const string sql = @"
                INSERT INTO ErroresLote (LoteId, NumeroLinea, Linea, Motivo)
                VALUES (@lote, @numero, @linea, @motivo);";
            using (var comando = new SQLiteCommand(sql, conexion, transaccion))
            {
                comando.Parameters.Add("@lote", DbType.Int64).Value = loteId;
                var numero = comando.Parameters.Add("@numero", DbType.Int32);
                var linea = comando.Parameters.Add("@linea", DbType.String);
                var motivo = comando.Parameters.Add("@motivo", DbType.String);
                foreach (ErrorProcesamiento error in lote.Errores)
                {
                    numero.Value = error.NumeroLinea;
                    linea.Value = error.Linea;
                    motivo.Value = error.Motivo;
                    comando.ExecuteNonQuery();
                }
            }
        }

        private static string DecimalExacto(decimal valor)
        {
            // SQLite REAL usa punto flotante binario. El texto conserva el decimal de C#.
            return valor.ToString(CultureInfo.InvariantCulture);
        }
    }
}
