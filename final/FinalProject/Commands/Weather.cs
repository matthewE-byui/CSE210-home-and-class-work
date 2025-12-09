using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace FinalProject.Commands
{
    public class WeatherCommand : Command
    {
        private static readonly HttpClient client = new HttpClient();

        public WeatherCommand() : base("weather") {}

        public override string Execute(string input)
        {
            // Example: weather rexburg
            string city = input.Replace("weather", "").Trim();

            if (string.IsNullOrWhiteSpace(city))
                return "Usage: weather <city>";

            return GetWeather(city).Result;
        }

        private async Task<string> GetWeather(string city)
        {
            try
            {
                // 1. Convert city name to coordinates
                string geoUrl =
                    $"https://geocoding-api.open-meteo.com/v1/search?name={city}&count=1";

                string geoResponse = await client.GetStringAsync(geoUrl);
                var geoData = JsonDocument.Parse(geoResponse);

                if (!geoData.RootElement.TryGetProperty("results", out var results))
                    return $"Couldn't find city: {city}";

                double lat = results[0].GetProperty("latitude").GetDouble();
                double lon = results[0].GetProperty("longitude").GetDouble();

                // 2. Get current weather
                string weatherUrl =
                    $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true&temperature_unit=fahrenheit&windspeed_unit=mph";

                string weatherResponse = await client.GetStringAsync(weatherUrl);
                var weatherData = JsonDocument.Parse(weatherResponse);

                var current = weatherData.RootElement.GetProperty("current_weather");

                double temp = current.GetProperty("temperature").GetDouble();
                double wind = current.GetProperty("windspeed").GetDouble();
                int code = current.GetProperty("weathercode").GetInt32();

                string description = WeatherCodeToString(code);

                return
$@"Weather for {city}:
Temperature: {temp}°F
Wind Speed: {wind} mph
Conditions: {description}";
            }
            catch (Exception ex)
            {
                return "Error retrieving weather: " + ex.Message;
            }
        }

        private string WeatherCodeToString(int code)
        {
            return code switch
            {
                0 => "Clear sky",
                1 => "Mainly clear",
                2 => "Partly cloudy",
                3 => "Overcast",
                45 or 48 => "Fog",
                51 or 53 or 55 => "Drizzle",
                61 or 63 or 65 => "Rain",
                71 or 73 or 75 => "Snow",
                95 => "Thunderstorm",
                _ => "Unknown",
            };
        }
    }
}
