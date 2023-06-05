//using Forecast.Models;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//namespace Forecast.Services
//{
//    public class WeatherService
//    {
//        private readonly HttpClient _httpClient;
//        private readonly string _apiKey = "0b5c68bffc39020e6ae4d0e16c14cc11"; //Better to store sensitive data like API key in a secure place

//        public WeatherService()
//        {
//            _httpClient = new HttpClient();
//        }

//        public async Task<RootObject> GetWeatherDataAsync(string city)
//        {
//            string url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={_apiKey}";
//            string response = await _httpClient.GetStringAsync(url);
//            return JsonConvert.DeserializeObject<RootObject>(response);
//        }

//        public IEnumerable<DateTime> GetForecastDates(RootObject weatherData)
//        {
//            var forecastData = weatherData.List;
//            return forecastData.Select(data => DateTimeOffset.FromUnixTimeSeconds(data.Dt).DateTime.Date).Distinct();
//        }

//        public IEnumerable<WeatherList> GetDataForDay(RootObject weatherData, DateTime selectedDay)
//        {
//            var forecastData = weatherData.List;
//            return forecastData.Where(data => DateTimeOffset.FromUnixTimeSeconds(data.Dt).DateTime.Date == selectedDay.Date);
//        }
//    }
//}
