using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WeatherApi.Net5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        public readonly WeatherClient client;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherClient client)
        {
            _logger = logger;
            this.client = client;
        }


        [HttpGet]
        [Route("{city}")]
        public async Task<WeatherForecast> GetAsync(string city)
        {
            var forecast = await client.GetWeatherAsync(city);

            return new WeatherForecast
            {
                Summary = forecast.weather[0].description,
                TemperatureC = (int)forecast.main.temp,
                Date = DateTimeOffset.FromUnixTimeSeconds(forecast.dt).DateTime

            };

        }
    }
}
