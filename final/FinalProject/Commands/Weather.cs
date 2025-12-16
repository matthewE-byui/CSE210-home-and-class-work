using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace FinalProject.Commands
{
    /// <summary>
    /// WeatherCommand fetches weather information from Open-Meteo API.
    /// Demonstrates inheritance and polymorphic command execution.
    /// Encapsulates HTTP client and API interactions.
    /// </summary>
    public class WeatherCommand : Command
    {
        // Encapsulation: Private static HTTP client
        private static readonly HttpClient client = new HttpClient();

        public WeatherCommand() : base("weather", "Get current weather information for a city") { }

        /// <summary>
        /// Executes weather command.
        /// Demonstrates polymorphism: overrides abstract Execute method.
        /// Returns CommandResult for proper error handling.
        /// </summary>
        public override CommandResult Execute(string input)
        {
            string city = ExtractParameter(input);

            if (string.IsNullOrWhiteSpace(city))
                return CommandResult.ErrorResult("Usage: weather <city>\nExample: weather rexburg");

            try
            {
                return GetWeather(city).Result;
            }
            catch (Exception ex)
            {
                return CommandResult.ErrorResult($"Error retrieving weather: {ex.Message}");
            }
        }

        private async Task<CommandResult> GetWeather(string city)
        {
            try
            {
                // 1. Convert city name to coordinates
                string geoUrl = $"https://geocoding-api.open-meteo.com/v1/search?name={city}&count=1";
                string geoResponse = await client.GetStringAsync(geoUrl);
                var geoData = JsonDocument.Parse(geoResponse);

                if (!geoData.RootElement.TryGetProperty("results", out var results) || results.GetArrayLength() == 0)
                    return CommandResult.ErrorResult($"Couldn't find city: {city}");

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

                string output = $"🌍 {city}\n🌡️  Temperature: {temp}°F\n💨 Wind Speed: {wind} mph\n☁️  Conditions: {description}";
                return CommandResult.SuccessResult(FormatOutput("WEATHER INFORMATION", output));
            }
            catch (Exception ex)
            {
                return CommandResult.ErrorResult($"Error retrieving weather: {ex.Message}");
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