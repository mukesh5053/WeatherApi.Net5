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
        public readonly WeatherClient Client;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherClient client)
        {
            _logger = logger;
            Client = client;
        }


        [HttpGet]
        [Route("{city}")]
        public async Task<WeatherForecast> GetAsync(string city)
        {
            var forecast = await Client.GetWeatherAsync(city);

            return new WeatherForecast
            {
                Summary = forecast.Weathers[0].description,
                TemperatureC = (int)forecast.Main.temp,
                Date = DateTimeOffset.FromUnixTimeSeconds(forecast.dt).DateTime

            };

        }
    }
}
