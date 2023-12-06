using System.Globalization;
using Uprise.Authentication;
using Uprise.Dto;

namespace Uprise.Services;

public class WeatherForecastService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherForecastService> _logger;

    public WeatherForecastService(IConfiguration configuration, ILogger<WeatherForecastService> logger)
    {
        _configuration = configuration;
        _httpClient = new HttpClient();
        _logger = logger;
    }

    public HttpRequestMessage CreateHttpRequestMessage(string url)
    {
        return new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url),
            Headers =
            {
                { "X-RapidAPI-Key", _configuration.GetValue<string>(AuthConstants.WEATHER_API_KEY) },
                { "X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com" },
            },
        };
    }

    public async Task<WeatherForecastDto> GetWeatherForecast(float lat, float lon, int days)
    {
        string url = 
            $"{_configuration.GetValue<string>(AuthConstants.WEATHER_API_URL)}/forecast.json?" +
            $"q={lat.ToString(CultureInfo.InvariantCulture)}%2C{lon.ToString(CultureInfo.InvariantCulture)}&days={days}";
        using var response = await _httpClient.SendAsync(CreateHttpRequestMessage(url));
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        WeatherForecastDto? weatherResponse = System.Text.Json.JsonSerializer.Deserialize<WeatherForecastDto>(body);

        if (weatherResponse == null) 
            throw new Exception("WeatherForecastDto parsing error.");
        else 
            return weatherResponse;
    }

    public async Task<string> GetWeatherForecastSimple(float lat, float lon, int days)
    {
        WeatherForecastDto res = await GetWeatherForecast(lat, lon, days);
        if (res.forecast.forecastday.Count > 0)
            return res.forecast.forecastday[0].day.condition.text;
        else
            throw new Exception("No forecast.");
    }
}
