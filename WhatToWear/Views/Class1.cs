using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class WeatherForecast
{
    private readonly HttpClient client = new HttpClient();

    public async Task<WeatherForecastResponse> GetWeatherForecastAsync(double latitude, double longitude)
    {
        try
        {
            string url =
                $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,relative_humidity_2m,apparent_temperature&hourly=temperature_2m,relative_humidity_2m,dew_point_2m,apparent_temperature,precipitation_probability,precipitation&daily=apparent_temperature_max,apparent_temperature_min,sunrise,sunset&timezone=Europe%2FBerlin&forecast_days=1";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); 

            string responseBody = await response.Content.ReadAsStringAsync();
            WeatherForecastResponse weatherData = JsonConvert.DeserializeObject<WeatherForecastResponse>(responseBody);

            if (weatherData.Hourly != null)
            {
                int index = 0;
                weatherData.HourlyTemperature = weatherData.Hourly.Temperature_2m[index];
                weatherData.HourlyRelativeHumidity = weatherData.Hourly.Relative_Humidity_2m[index];
                weatherData.HourlyDewPoint = weatherData.Hourly.Dew_Point_2m[index];
                weatherData.HourlyApparentTemperature = weatherData.Hourly.Apparent_Temperature[index];
                weatherData.HourlyPrecipitationProbability = weatherData.Hourly.Precipitation_Probability[index];
                weatherData.HourlyPrecipitation = weatherData.Hourly.Precipitation[index];
            }

            if (weatherData.Current != null)
            {
                weatherData.CurrentTemperature = weatherData.Current.Temperature_2m;
                weatherData.CurrentRelativeHumidity = weatherData.Current.Relative_Humidity_2m;
                weatherData.CurrentApparentTemperature = weatherData.Current.Apparent_Temperature;
            }

            if (weatherData.Daily != null)
            {
                int index = 0;
                weatherData.DailyApparentTemperatureMax = weatherData.Daily.Apparent_Temperature_Max[index];
                weatherData.DailyApparentTemperatureMin = weatherData.Daily.Apparent_Temperature_Min[index];
            }

            return weatherData;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
            throw; 
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw; 
        }
    }

    public class WeatherForecastResponse
    {
        public Current Current { get; set; }
        public Hourly Hourly { get; set; }
        public Daily Daily { get; set; }

        public double CurrentTemperature { get; set; }
        public double CurrentRelativeHumidity { get; set; }
        public double CurrentApparentTemperature { get; set; }
        public double HourlyTemperature { get; set; }
        public double HourlyRelativeHumidity { get; set; }
        public double HourlyDewPoint { get; set; }
        public double HourlyApparentTemperature { get; set; }
        public double HourlyPrecipitationProbability { get; set; }
        public double HourlyPrecipitation { get; set; }
        public double DailyApparentTemperatureMax { get; set; }
        public double DailyApparentTemperatureMin { get; set; }
    }

    public class Current
    {
        public double Temperature_2m { get; set; }
        public double Relative_Humidity_2m { get; set; }
        public double Apparent_Temperature { get; set; }
    }

    public class Hourly
    {
        public List<string> Time { get; set; }
        public List<double> Temperature_2m { get; set; }
        public List<double> Relative_Humidity_2m { get; set; }
        public List<double> Dew_Point_2m { get; set; }
        public List<double> Apparent_Temperature { get; set; }
        public List<double> Precipitation_Probability { get; set; }
        public List<double> Precipitation { get; set; }
    }

    public class Daily
    {
        public List<double> Apparent_Temperature_Max { get; set; }
        public List<double> Apparent_Temperature_Min { get; set; }
        //public string Sunrise { get; set; }
        //public string Sunset { get; set; }
    }
}

