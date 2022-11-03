using Microsoft.AspNetCore.Mvc;

namespace API.Demo;

[ApiController]
[Route("[controller]")] //localhost:7004/Weatherforecast

public class WeatherForecastController : ControllerBase
{
    private static readonly string[] TempDescriptions = new[]
    {
"Moderate Rain", "Thunder Storm", "Broken Clouds", "Light Rain", "Heavy Rain", "Sunny", "Freezing", "Drizzling"
};
    [HttpGet]
    public IEnumerable<WeatherForecast> GetWeatherDetails()
    {
        var weatherDetails = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            TemperatureC = Random.Shared.Next(-20, 55),
            Date = DateTime.Now.AddDays(index),
            TempDescription = TempDescriptions[Random.Shared.Next(TempDescriptions.Length)]
        }).ToArray();
        return weatherDetails;
    }
}