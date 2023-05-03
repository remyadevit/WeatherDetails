using WeatherDetails.Connector.Model;
using WeatherDetails.Model;

namespace WeatherDetails.Helper
{
    public static class WeatherDetailsHelper
    {
        public static bool IsValidLatitude(this string latitude)
     => double.TryParse(latitude, out var l) && -90 <= l && l <= 90;

        public static bool IsValidLongitude(this string longitude)
            => double.TryParse(longitude, out var l) && -180 <= l && l <= 180;

        public static WeatherDetailsResponse ToResponse(this MeteoResponse weatherDetailsResponseFromAPI)
        {
            List<DateTime> distinctDates = weatherDetailsResponseFromAPI.hourly.time.Select(v => Convert.ToDateTime(v).Date).Distinct().OrderByDescending(x => x).ToList();
            var weatherDetailsResponse = new WeatherDetailsResponse();
            weatherDetailsResponse.dailyWeatherDetails = new List<DailyWeatherDetails>();
            var skipLenght = 0;
            foreach (var date in distinctDates) 
            {
               
                var takeLenght = weatherDetailsResponseFromAPI.hourly.time.Where(v => Convert.ToDateTime(v).Date.Equals(date)).ToList().Count;
                var dailyWeatherDetails = new DailyWeatherDetails
                {
                    Date = date,
                    MaxTemperatureInCelsius = weatherDetailsResponseFromAPI.hourly.temperature_2m.Count > 0 ? weatherDetailsResponseFromAPI.hourly.temperature_2m.Skip(skipLenght).Take(takeLenght).Max(x => x) : 0.00,
                    MinTemperatureInCelsius = weatherDetailsResponseFromAPI.hourly.temperature_2m.Count > 0 ? weatherDetailsResponseFromAPI.hourly.temperature_2m.Skip(skipLenght).Take(takeLenght).Min(x => x) : 0.00,
                    MaxSnowfall = weatherDetailsResponseFromAPI.hourly.snowfall.Count>0? weatherDetailsResponseFromAPI.hourly.snowfall.Skip(skipLenght).Take(takeLenght).Max(x => x):0.00,
                    MinSnowfall = weatherDetailsResponseFromAPI.hourly.snowfall.Count > 0 ? weatherDetailsResponseFromAPI.hourly.snowfall.Skip(skipLenght).Take(takeLenght).Min(x => x) : 0.00,
                    MaxTemperatureInFarenheit = weatherDetailsResponseFromAPI.hourly.temperature_2m.Count > 0 ? weatherDetailsResponseFromAPI.hourly.temperature_2m.Skip(skipLenght).Take(takeLenght).Max(x => x).ToFarenheit() : 0.00 ,
                    MinTemperatureInFarenheit = weatherDetailsResponseFromAPI.hourly.temperature_2m.Count > 0 ? weatherDetailsResponseFromAPI.hourly.temperature_2m.Skip(skipLenght).Take(takeLenght).Min(x => x).ToFarenheit() : 0.00

                };

                skipLenght += takeLenght;
                

                weatherDetailsResponse.dailyWeatherDetails.Add(dailyWeatherDetails);
            }

            return weatherDetailsResponse;

        }

        public static double ToFarenheit(this double tempInCelsius)
        {
            return (tempInCelsius * 1.8) + 32;

        }
    }
}
