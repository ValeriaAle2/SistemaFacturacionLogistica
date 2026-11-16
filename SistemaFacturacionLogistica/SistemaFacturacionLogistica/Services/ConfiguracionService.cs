using System;
using System.IO;
using Newtonsoft.Json;
using SistemaFacturacionLogistica.Models;

namespace SistemaFacturacionLogistica.Services
{
    internal class ConfiguracionService
    {
        public Configuracion Cargar()
        {
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            var config = JsonConvert.DeserializeObject<Configuracion>(File.ReadAllText(ruta));
            if (config == null || config.PrecioBaseKg <= 0 || config.Iva < 0 || config.Iva > 1 ||
                config.CostoFijoEnvio < 0)
            {
                throw new InvalidDataException("Revise las tarifas de appsettings.json.");
            }

            return config;
        }
    }
}
