using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.WeatherForecast
{
    /// <summary>
    /// DTO used to get a weather forecast on a given date
    /// </summary>
    public class GetWeatherForecastRequest
    {
        /// <summary>
        /// Date of the weather forecast
        /// </summary>
        [Required]
        public DateTime Date { get; set; }
    }
}