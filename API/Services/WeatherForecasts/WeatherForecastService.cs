using API.DTOs.WeatherForecast;
using AutoMapper;
using Domaine.Interfaces;
using Domaine.WeatherForecasts;

namespace API.Services.WeatherForecasts
{
    public class WeatherForecastService : BaseService
    {
        public WeatherForecastService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public GetWeatherForecastResponse GetByDate(GetWeatherForecastRequest request)
        {
            var repository = UnitOfWork.Repository<WeatherForecast>();
            var wf = repository.GetFirstOrDefault(x => x.Date.Date == request.Date.Date);

            return Mapper.Map<WeatherForecast, GetWeatherForecastResponse>(wf);
        }
    }
}