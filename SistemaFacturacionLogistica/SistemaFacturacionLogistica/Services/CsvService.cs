using System.Globalization;
using System.IO;
using System.Text;
using SistemaFacturacionLogistica.Models;

namespace SistemaFacturacionLogistica.Services
{
    public class CsvService
    {
        public void Exportar(string ruta, ResultadoLote lote, Configuracion config)
        {
            using (var archivo = new StreamWriter(ruta, false, new UTF8Encoding(true)))
            {
                archivo.WriteLine("Codigo,Cliente,Zona,Total,Tipo,PesoKg,ValorDeclarado,Subtotal," +
                    "RecargoZona,Penalizacion,Seguro,CostoFijoEnvio,IVA,NumeroLinea,Linea,Motivo");
                foreach (Paquete paquete in lote.Paquetes)
                {
                    EscribirFila(archivo, paquete.Codigo, paquete.Cliente, paquete.Zona,
                        DecimalCsv(paquete.Total), "PAQUETE", DecimalCsv(paquete.Peso),
                        DecimalCsv(paquete.ValorDeclarado), DecimalCsv(paquete.Subtotal),
                        DecimalCsv(paquete.RecargoZona), DecimalCsv(paquete.Penalizacion),
                        DecimalCsv(paquete.Seguro), DecimalCsv(config.CostoFijoEnvio),
                        DecimalCsv(paquete.Impuesto));
                }

                foreach (ErrorProcesamiento error in lote.Errores)
                {
                    EscribirFila(archivo, "", "", "", "", "ERROR", "", "", "", "", "", "", "", "",
                        error.NumeroLinea.ToString(CultureInfo.InvariantCulture), error.Linea, error.Motivo);
                }

                EscribirFila(archivo, "", "", "", DecimalCsv(lote.Total), "LOTE");
            }
        }

        private static void EscribirFila(StreamWriter archivo, params string[] valores)
        {
            const int cantidadColumnas = 16;
            for (int i = 0; i < cantidadColumnas; i++)
            {
                if (i > 0)
                    archivo.Write(',');

                if (i < valores.Length && valores[i].Length > 0)
                {
                    archivo.Write('"');
                    archivo.Write(valores[i].Replace("\"", "\"\""));
                    archivo.Write('"');
                }
            }

            archivo.WriteLine();
        }

        private static string DecimalCsv(decimal valor) => valor.ToString(CultureInfo.InvariantCulture);
    }
}
