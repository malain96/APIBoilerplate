using System;

namespace API.DTOs.WeatherForecast
{
    /// <summary>
    /// DTO used to respond to weather forecast search
    /// </summary>
    public class GetWeatherForecastResponse
    {
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Temperature
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Summary
        /// </summary>
        public string Summary { get; set; }
    }
}