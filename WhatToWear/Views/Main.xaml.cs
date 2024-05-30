using System;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace WhatToWear.Views
{
    public partial class Main : ContentPage
    {
        private readonly WeatherForecast weatherForecast = new WeatherForecast();

        public Main()
        {
            InitializeComponent();
            FetchWeatherData(null, null);
            
        }

        private async void FetchWeatherData(object sender, EventArgs e)
        {
            try
            {
                
                double latitude = 52.52;
                double longitude = 13.41;

                var weatherData = await weatherForecast.GetWeatherForecastAsync(latitude, longitude);

                
                temperatureLabel.Text = $"Temperature: {weatherData.Hourly.Temperature_2m[0]}°C";
                precipitationLabel.Text = $"Precipitation: {weatherData.Hourly.Precipitation[0]} mm";
                precipitationProbabilityLabel.Text = $"Precipitation Probability: {weatherData.Hourly.Precipitation_Probability[0]}%";
                dewPoint.Text = $"Relative Humidity: {weatherData.Hourly.Dew_Point_2m[0]}%";
                dailyMaxTemp.Text = $"Daily Max Temperature: {weatherData.Daily.Apparent_Temperature_Max[0]}°C";

                
                UpdateClothingRecommendation((double)weatherData.Hourly.Temperature_2m[0]);
            }
            catch (HttpRequestException httpEx)
            {
                await DisplayAlert("Network Error", $"Failed to fetch weather data: {httpEx.Message}", "OK");
            }
            catch (JsonException jsonEx)
            {
                await DisplayAlert("Parsing Error", $"Failed to parse weather data: {jsonEx.Message}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }

        private void UpdateClothingRecommendation(double temperature)
        {
            if (temperature > 20)
            {
                recommendationLabel.Text = "Wear a T-shirt and shorts";
            }
            else if (temperature > 10)
            {
                recommendationLabel.Text = "Wear a light jacket";
            }
            else
            {
                recommendationLabel.Text = "Wear a warm coat";
            }
        }

        void OnReloadButtonClicked(object sender, EventArgs e)
        {
            FetchWeatherData(null, null);
        }

    }
}