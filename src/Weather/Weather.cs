﻿using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;

namespace Weather;

public class WeatherCalculator
{
    public async Task<WeatherInfo> GetCurrentWeatherAndKP(double latitude, double longitude)
    {
        var apiKey = "25a7d8e49c687774b007f667f2f9f211";
        using (var client = new HttpClient())
        {
            try
            {
                var response = await client.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric");
                var weather = JsonSerializer.Deserialize<WeatherInfo>(response) ?? throw new Exception("API response was null");

                weather.KpValue = await GetKPIndex();
                return weather;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting weather data with error: ", ex);
            }
        }
    }
    private static async Task<double> GetKPIndex()
    {
        try
        {
            var endDate = DateTime.Now;
            var startDate = endDate.AddHours(-4);
            using (var client = new HttpClient())
            {
                Console.WriteLine($"https://kp.gfz-potsdam.de/app/json/?start={startDate.ToString("yyyy-MM-dd")}T{startDate.ToString("HH:mm:ss")}Z&end={endDate.ToString("yyyy-MM-dd")}T{endDate.ToString("HH:mm:ss")}Z&index=Kp&status=");
                var response = await client.GetStringAsync($"https://kp.gfz-potsdam.de/app/json/?start={startDate.ToString("yyyy-MM-dd")}T{startDate.ToString("HH:mm:ss")}Z&end={endDate.ToString("yyyy-MM-dd")}T{endDate.ToString("HH:mm:ss")}Z&index=Kp&status=");
                var jsonResponse = JsonSerializer.Deserialize<JsonElement>(response);
                var kpValue = jsonResponse.GetProperty("Kp")[0].GetDouble();
                return kpValue;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting KP index with error: ", ex);
        }
    }
    public class WeatherInfo
    {
        [JsonPropertyName("kp_value")]
        public double KpValue { get; set; }
        [JsonPropertyName("weather")]
        public Weather[]? Weather { get; set; }

        [JsonPropertyName("base")]
        public string? Base { get; set; }

        [JsonPropertyName("main")]
        public WeatherData? Main { get; set; }

        [JsonPropertyName("visibility")]
        public int Visibility { get; set; }

        [JsonPropertyName("wind")]
        public Wind? Wind { get; set; }

        [JsonPropertyName("snow")]
        public Snow? Snow { get; set; }

        [JsonPropertyName("clouds")]
        public Clouds? Clouds { get; set; }

        [JsonPropertyName("dt")]
        public long Dt { get; set; }

        [JsonPropertyName("sys")]
        public Sys? Sys { get; set; }

        [JsonPropertyName("timezone")]
        public int Timezone { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("cod")]
        public int Cod { get; set; }
    }
    public class Weather
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("main")]
        public string? Main { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("icon")]
        public string? Icon { get; set; }
    }
    public class WeatherData
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }

        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }

        [JsonPropertyName("temp_min")]
        public double TempMin { get; set; }

        [JsonPropertyName("temp_max")]
        public double TempMax { get; set; }

        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("sea_level")]
        public int SeaLevel { get; set; }

        [JsonPropertyName("grnd_level")]
        public int GroundLevel { get; set; }
    }
    public class Wind
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }

        [JsonPropertyName("deg")]
        public int Deg { get; set; }

        [JsonPropertyName("gust")]
        public double Gust { get; set; }
    }
    public class Snow
    {
        [JsonPropertyName("1h")]
        public double OneHour { get; set; }
    }
    public class Clouds
    {
        [JsonPropertyName("all")]
        public int All { get; set; }
    }
    public class Sys
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("sunrise")]
        public long Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public long Sunset { get; set; }
    }
}

