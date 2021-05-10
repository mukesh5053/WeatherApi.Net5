using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace WeatherApi.Net5
{

    public class ExternalAPIHealthCheck : IHealthCheck
    {
        private WeatherSettings settings;

        public ExternalAPIHealthCheck(IOptions<WeatherSettings> options)
        {
            settings = options.Value;
        }


        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Ping ping = new();
            var result = await ping.SendPingAsync(settings.WeatherApiHostName);
            if (result.Status != IPStatus.Success)
            {
                return HealthCheckResult.Unhealthy();
            }

            return HealthCheckResult.Healthy();
        }
    }
}