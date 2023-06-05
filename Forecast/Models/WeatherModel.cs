using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherForecast.Models;
public class WeatherInfo
{
    public string Description { get; set; }
}

public class MainWeatherInfo
{
    public double Temp { get; set; }
    public double Feels_like { get; set; }
    public double Temp_min { get; set; }
    public double Temp_max { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
}

public class WeatherResponse
{
    [JsonProperty("weather")]
    public List<WeatherInfo> WeatherInfo { get; set; }
    public MainWeatherInfo Main { get; set; }
    public int Dt { get; set; } // Unix timestamp for the current weather

    [JsonProperty("dt_txt")]
    public string DateText { get; set; } // Date and time of the weather data
}

public class RootObject
{
    public List<WeatherResponse> List { get; set; }
}
