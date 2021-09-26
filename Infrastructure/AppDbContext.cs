using Domaine.WeatherForecasts;
using Infrastructure.EntitiesConfig;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            WeatherForecastConfig.OnModelCreating(modelBuilder.Entity<WeatherForecast>());
        }
    }
}