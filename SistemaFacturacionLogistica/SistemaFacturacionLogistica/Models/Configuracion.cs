using Newtonsoft.Json;

namespace SistemaFacturacionLogistica.Models
{
    public class Configuracion
    {
        [JsonProperty(Required = Required.Always)]
        public decimal PrecioBaseKg { get; set; }

        [JsonProperty(Required = Required.Always)]
        public decimal Iva { get; set; }

        [JsonProperty(Required = Required.Always)]
        public decimal CostoFijoEnvio { get; set; }
    }
}
