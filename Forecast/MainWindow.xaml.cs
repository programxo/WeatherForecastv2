using Forecast.Models;
using Forecast.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Forecast.Models.GeoModel;

namespace Forecast
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RootObject weatherData;

        public MainWindow()
        {
            InitializeComponent();

            // 
            for (var i = 0; i <= 4; i++)
            {
                comboBoxDays.Items.Add(DateTime.Now.AddDays(i).ToShortDateString());
            }
            comboBoxDays.SelectedIndex = 0;  // 
        }

        private async void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var city = textBoxCity.Text;

                var geo = new GeoService();

                var coordinates = geo.GetCoordinates(city);

                string apiKey = "0b5c68bffc39020e6ae4d0e16c14cc11";

                string url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}";


                HttpClient client = new HttpClient();
                string response = await client.GetStringAsync(url);

                weatherData = JsonConvert.DeserializeObject<RootObject>(response);

                UpdateForecastDays();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ein Fehler ist aufgetreten: " + ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateForecastDays()
        {
            comboBoxDays.Items.Clear();
            var forecastData = weatherData.List;
            var dates = forecastData.Select(data => DateTimeOffset.FromUnixTimeSeconds(data.Dt).DateTime.Date).Distinct();

            foreach (var date in dates)
            {
                comboBoxDays.Items.Add(date.ToShortDateString());
            }

            if (comboBoxDays.Items.Count > 0)
            {
                comboBoxDays.SelectedIndex = 0;  // 
            }
            else
            {
                MessageBox.Show("Keine Wetterdaten gefunden.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonShowForecast_Click(object sender, RoutedEventArgs e)
        {
            var forecastData = weatherData.List;

            StringBuilder forecastText = new StringBuilder();

            // 
            DateTime selectedDay = DateTime.Parse(comboBoxDays.SelectedItem.ToString());

            var selectedDayData = forecastData.Where(data => DateTimeOffset.FromUnixTimeSeconds(data.Dt).DateTime.Date == selectedDay.Date);

            // 
            var morningData = selectedDayData.Where(data => DateTimeOffset.FromUnixTimeSeconds(data.Dt).DateTime.Hour >= 6 && DateTimeOffset.FromUnixTimeSeconds(data.Dt).DateTime.Hour < 12);
            var afternoonData = selectedDayData.Where(data => DateTimeOffset.FromUnixTimeSeconds(data.Dt).DateTime.Hour >= 12 && DateTimeOffset.FromUnixTimeSeconds(data.Dt).DateTime.Hour < 18);
            var eveningData = selectedDayData.Where(data => DateTimeOffset.FromUnixTimeSeconds(data.Dt).DateTime.Hour >= 18 && DateTimeOffset.FromUnixTimeSeconds(data.Dt).DateTime.Hour < 22);
            var nightData = selectedDayData.Where(data => DateTimeOffset.FromUnixTimeSeconds(data.Dt).DateTime.Hour >= 22 || DateTimeOffset.FromUnixTimeSeconds(data.Dt).DateTime.Hour < 6);


            // 
            forecastText.AppendLine("Morgen:");
            foreach (var data in morningData)
            {
                forecastText.AppendLine($"{data.DateText} - {data.WeatherInfo[0].Description} - Temperatur: {data.Main.Temp}°C");
            }

            forecastText.AppendLine("Mittag:");
            foreach (var data in afternoonData)
            {
                forecastText.AppendLine($"{data.DateText} - {data.WeatherInfo[0].Description} - Temperatur: {data.Main.Temp}°C");
            }

            forecastText.AppendLine("Abend:");
            foreach (var data in eveningData)
            {
                forecastText.AppendLine($"{data.DateText} - {data.WeatherInfo[0].Description} - Temperatur: {data.Main.Temp}°C");
            }

            forecastText.AppendLine("Nacht:");
            foreach (var data in nightData)
            {
                forecastText.AppendLine($"{data.DateText} - {data.WeatherInfo[0].Description} - Temperatur: {data.Main.Temp}°C");
            }

            textBlockWeather.Text = forecastText.ToString();
        }

        
    }





}


