using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SistemaFacturacionLogistica.Services
{
    public class IdiomaService
    {
        private Dictionary<string, string> _textos = new Dictionary<string, string>
        {
            { "ErrorIdioma", "No se pudo cargar el idioma." },
            { "ErrorJson", "No se encontró el archivo de configuración." },
            { "ErrorConfiguracion", "Revise que appsettings.json contenga tarifas válidas." }
        };

        public void CargarIdioma(string idioma)
        {
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources",
                idioma == "EN" ? "textos_en.json" : "textos_es.json");
            _textos = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(ruta))
                ?? throw new JsonSerializationException("El archivo de idioma está vacío.");
        }

        public string ObtenerTexto(string clave)
        {
            return _textos.TryGetValue(clave, out string texto) ? texto : clave;
        }
    }
}
