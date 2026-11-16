using System;
using System.Collections.Generic;

namespace SistemaFacturacionLogistica.Models
{
    public class ResultadoLote
    {
        internal ResultadoLote(Paquete[] paquetes, ErrorProcesamiento[] errores, decimal total)
        {
            Paquetes = Array.AsReadOnly(paquetes);
            Errores = Array.AsReadOnly(errores);
            Total = total;
        }

        public IReadOnlyList<Paquete> Paquetes { get; }
        public IReadOnlyList<ErrorProcesamiento> Errores { get; }
        public decimal Total { get; }
    }
}
