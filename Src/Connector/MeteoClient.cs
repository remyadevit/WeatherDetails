using System.Net.Http.Json;
using WeatherDetails.Connector.Model;

namespace WeatherDetails.Connector
{
    public class MeteoClient
    {
        private readonly HttpClient _httpClient;

        public MeteoClient(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<MeteoResponse> GetWeatherDetails(string longitude, string latitude, string days) => await _httpClient.GetFromJsonAsync<MeteoResponse>(string.Format("?latitude={0}&longitude={1}&forecast_days={2}&hourly=temperature_2m&hourly=snowfall", latitude, longitude, days));
    }
}