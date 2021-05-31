using System;

namespace bison_api
{
    /// <summary>
    /// Entidad creada por default por la plantilla de ASP.NET Core 5
    /// </summary>
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
