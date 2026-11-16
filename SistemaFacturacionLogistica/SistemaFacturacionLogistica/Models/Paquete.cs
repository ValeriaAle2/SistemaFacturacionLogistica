using System;

namespace SistemaFacturacionLogistica.Models
{
    public class Paquete
    {
        private string _codigo;
        private string _cliente;
        private string _zona;
        private decimal _peso;
        private decimal _valorDeclarado;

        public string Codigo
        {
            get => _codigo;
            set => _codigo = value;
        }

        public string Cliente
        {
            get => _cliente;
            set => _cliente = value;
        }

        public string Zona
        {
            get => _zona;
            set => _zona = value;
        }

        public decimal Peso
        {
            get => _peso;
            set => _peso = value;
        }

        public decimal ValorDeclarado
        {
            get => _valorDeclarado;
            set => _valorDeclarado = value;
        }

        public decimal Subtotal { get; set; }
        public decimal RecargoZona { get; set; }
        public decimal Penalizacion { get; set; }
        public decimal Seguro { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public long IdPersistido { get; internal set; }
        public DateTime? FechaRecepcionUtc { get; internal set; }
    }
}
