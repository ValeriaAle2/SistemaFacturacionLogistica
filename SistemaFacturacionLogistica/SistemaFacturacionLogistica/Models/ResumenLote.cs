using System;

namespace SistemaFacturacionLogistica.Models
{
    public class ResumenLote
    {
        internal ResumenLote(long id, DateTime fechaUtc, int paquetes, int errores, decimal total)
        {
            Id = id;
            FechaUtc = fechaUtc;
            Paquetes = paquetes;
            Errores = errores;
            Total = total;
        }

        public long Id { get; }
        public DateTime FechaUtc { get; }
        public int Paquetes { get; }
        public int Errores { get; }
        public decimal Total { get; }
    }
}
