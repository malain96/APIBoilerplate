using Domaine.Base;
using System;

namespace Domaine.WeatherForecasts
{
    public class WeatherForecast : BaseEntity<int>
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }
    }
}