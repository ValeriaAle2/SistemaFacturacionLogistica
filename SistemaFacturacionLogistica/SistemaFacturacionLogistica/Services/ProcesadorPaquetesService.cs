using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using SistemaFacturacionLogistica.Models;

namespace SistemaFacturacionLogistica.Services
{
    public class ProcesadorPaquetesService
    {
        private static readonly Regex _patronCodigo = new Regex(@"\A[A-Z0-9]+(-[A-Z0-9]+)*\z");
        private static readonly Regex _patronCliente = new Regex(@"\A[\p{L}\p{M}\p{Nd} .,'’&""()/-]+\z");
        private static readonly Regex _letraODigito = new Regex(@"[\p{L}\p{Nd}]");

        private ResultadoLote[] _historial = new ResultadoLote[5];
        private int _cantidadLotes;

        public IReadOnlyList<ResultadoLote> ObtenerHistorial()
        {
            return Array.AsReadOnly(Copiar(_historial, _cantidadLotes, _cantidadLotes));
        }

        public ResultadoLote Procesar(string texto, CalculoFacturaService calculo)
        {
            if (calculo == null)
                throw new ArgumentNullException(nameof(calculo));

            var paquetes = new Paquete[5];
            var errores = new ErrorProcesamiento[5];
            int cantidadPaquetes = 0;
            int cantidadErrores = 0;
            int numeroLinea = 0;
            decimal total = 0;

            using (var lector = new StringReader(texto))
            {
                string linea;
                while ((linea = lector.ReadLine()) != null)
                {
                    numeroLinea++;
                    string motivo = Validar(linea, out Paquete paquete);
                    if (motivo == null)
                    {
                        try
                        {
                            calculo.Calcular(paquete);
                        }
                        catch (OverflowException)
                        {
                            motivo = "ImporteFueraDeRango";
                        }
                    }

                    if (motivo == null)
                    {
                        total += paquete.Total;
                        Agregar(ref paquetes, ref cantidadPaquetes, paquete);
                    }
                    else
                    {
                        Agregar(ref errores, ref cantidadErrores,
                            new ErrorProcesamiento(numeroLinea, linea, motivo));
                    }
                }
            }

            if (numeroLinea == 0)
                Agregar(ref errores, ref cantidadErrores, new ErrorProcesamiento(1, "", "LineaVacia"));

            OrdenarPorTotal(paquetes, cantidadPaquetes);
            var resultado = new ResultadoLote(
                Copiar(paquetes, cantidadPaquetes, cantidadPaquetes),
                Copiar(errores, cantidadErrores, cantidadErrores), total);
            Agregar(ref _historial, ref _cantidadLotes, resultado);
            return resultado;
        }

        private static string Validar(string linea, out Paquete paquete)
        {
            paquete = null;
            if (string.IsNullOrWhiteSpace(linea))
                return "LineaVacia";

            string[] datos = linea.Split(';');
            if (datos.Length != 5)
                return "FormatoInvalido";

            for (int i = 0; i < datos.Length; i++)
                datos[i] = datos[i].Trim();

            string codigo = datos[0].ToUpperInvariant();
            string cliente = datos[1];
            if (codigo.Length == 0 || cliente.Length == 0)
                return "CamposObligatorios";
            if (!_patronCodigo.IsMatch(codigo))
                return "CodigoInvalido";
            if (!_patronCliente.IsMatch(cliente) || !_letraODigito.IsMatch(cliente))
                return "ClienteInvalido";

            string zona = datos[2].ToUpperInvariant();
            switch (zona)
            {
                case "A":
                case "B":
                case "C":
                    zona = "ZONA " + zona;
                    break;
                case "ZONA A":
                case "ZONA B":
                case "ZONA C":
                    break;
                default:
                    return "ZonaInvalida";
            }

            if (!LeerNumero(datos[3], out decimal peso) || peso <= 0)
                return "PesoInvalido";
            if (!LeerNumero(datos[4], out decimal valor) || valor < 0)
                return "ValorInvalido";

            paquete = new Paquete
            {
                Codigo = codigo,
                Cliente = cliente,
                Zona = zona,
                Peso = peso,
                ValorDeclarado = valor
            };
            return null;
        }

        private static bool LeerNumero(string texto, out decimal numero)
        {
            return decimal.TryParse(texto.Replace(',', '.'),
                NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture, out numero);
        }

        internal static void OrdenarPorTotal(Paquete[] paquetes, int cantidad)
        {
            // Ordenamiento por inserción, de mayor a menor y estable en caso de empate.
            for (int i = 1; i < cantidad; i++)
            {
                Paquete actual = paquetes[i];
                int posicion = i - 1;
                while (posicion >= 0 && paquetes[posicion].Total < actual.Total)
                {
                    paquetes[posicion + 1] = paquetes[posicion];
                    posicion--;
                }

                paquetes[posicion + 1] = actual;
            }
        }

        private static void Agregar<T>(ref T[] arreglo, ref int cantidad, T elemento)
        {
            if (cantidad == arreglo.Length)
                arreglo = Copiar(arreglo, cantidad, arreglo.Length * 2);

            arreglo[cantidad++] = elemento;
        }

        private static T[] Copiar<T>(T[] origen, int cantidad, int capacidad)
        {
            var destino = new T[capacidad];
            for (int i = 0; i < cantidad; i++)
                destino[i] = origen[i];

            return destino;
        }
    }
}
