using Domaine.WeatherForecasts;

namespace Infrastructure.Repositories
{
    public class WeatherForecastRepository : Repository<WeatherForecast>, IWeatherForecastRepository
    {
        public WeatherForecastRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}