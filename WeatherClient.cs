using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace WeatherApi.Net5
{
    public class WeatherClient
    {
        private readonly HttpClient httpClient;
        public readonly WeatherSettings settings;

        public WeatherClient(HttpClient httpClient, IOptions<WeatherSettings> options)
        {
            this.httpClient = httpClient;
            settings = options.Value;
        }

        public record Weather(string description);
        public record Main(decimal temp);

        public record Forecast(Weather[] Weathers, Main Main, long dt);
        public async Task<Forecast> GetWeatherAsync(string city)
        {
            var forecast = await httpClient.GetFromJsonAsync<Forecast>($"Https>//{settings.WeatherApiHostName}/data/2.5/weather?q={city}&appid={settings.ApiKey}");
            return forecast;
        }


    }
}