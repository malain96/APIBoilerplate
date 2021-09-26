using Domaine.WeatherForecasts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.EntitiesConfig
{
    public class WeatherForecastConfig
    {
        public static void OnModelCreating(EntityTypeBuilder<WeatherForecast> entity)
        {
            entity.Property(e => e.Summary)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            entity.HasData(new WeatherForecast
            {
                Id = 1,
                Date = DateTime.Parse("2021-06-06"),
                Summary = "Test 1",
                TemperatureC = 25
            }, new WeatherForecast
            {
                Id = 2,
                Date = DateTime.Parse("2021-05-05"),
                Summary = "Test 2",
                TemperatureC = 29
            });
        }
    }
}