using SistemaFacturacionLogistica.Models;

namespace SistemaFacturacionLogistica.Services
{
    public class CalculoFacturaService
    {
        private readonly Configuracion _config;

        public CalculoFacturaService(Configuracion config)
        {
            _config = config;
        }

        public void Calcular(Paquete paquete)
        {
            decimal subtotal =
                paquete.Peso *
                _config.PrecioBaseKg;

            decimal recargo = 0;

            switch (paquete.Zona.ToUpper())
            {
                case "ZONA A":
                    recargo = subtotal * 0.05m;
                    break;

                case "ZONA B":
                    recargo = subtotal * 0.10m;
                    break;

                case "ZONA C":
                    recargo = subtotal * 0.18m;
                    break;
            }

            decimal penalizacion = 0;

            if (paquete.Peso > 20)
            {
                decimal extra =
                    paquete.Peso - 20m;

                penalizacion =
                    extra *
                    (_config.PrecioBaseKg * 0.25m);
            }

            decimal seguro = 0;

            if (paquete.ValorDeclarado > 1000)
            {
                seguro =
                    paquete.ValorDeclarado *
                    0.02m;
            }

            decimal antesIva =
                subtotal +
                recargo +
                penalizacion +
                seguro +
                _config.CostoFijoEnvio;

            decimal iva =
                antesIva *
                _config.Iva;

            decimal total =
                antesIva + iva;

            paquete.Subtotal = subtotal;
            paquete.RecargoZona = recargo;
            paquete.Penalizacion = penalizacion;
            paquete.Seguro = seguro;
            paquete.Impuesto = iva;
            paquete.Total = total;
        }
    }
}
