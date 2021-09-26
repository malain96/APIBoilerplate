using API.DTOs.WeatherForecast;
using AutoMapper;
using Domaine.WeatherForecasts;

namespace API.Mapper
{
    public class WeatherForecastMappingProfile : Profile
    {
        public WeatherForecastMappingProfile()
        {
            CreateMap<WeatherForecast, GetWeatherForecastResponse>().ReverseMap();
        }
    }
}