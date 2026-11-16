namespace SistemaFacturacionLogistica.Models
{
    public class ErrorProcesamiento
    {
        public ErrorProcesamiento(int numeroLinea, string linea, string motivo)
        {
            NumeroLinea = numeroLinea;
            Linea = linea;
            Motivo = motivo;
        }

        public int NumeroLinea { get; }
        public string Linea { get; }
        public string Motivo { get; }
    }
}
