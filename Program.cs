using Microsoft.Extensions.DependencyInjection;
using WeatherDetails.Helper;
using Microsoft.Extensions.Http;
using WeatherDetails.Connector;
using System.Text.Json;
using WeatherDetails.Helper.Constants;
using WeatherDetails.Model;

namespace WeatherDetails;
class Program
{
    static void Main()
    {
        var services = new ServiceCollection();
        services.AddHttpClient<MeteoClient>(c => c.BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast"));
        var serviceProvider = services.BuildServiceProvider();

        Console.WriteLine("Welcome to WeatherDetails Project!");
        Console.WriteLine("Please enter Longitude!");
        var longitude = Console.ReadLine();

        if (string.IsNullOrEmpty(longitude) && !longitude.IsValidLongitude())
        {
            Console.WriteLine("Please enter a valid value for longitude!");
        }
        else
        {
            Console.WriteLine("Please enter Latitude!");
            var latitude = Console.ReadLine();

            Console.WriteLine("Please enter Number of Days!");
            var days = Console.ReadLine();

            if (string.IsNullOrEmpty(latitude) && !latitude.IsValidLatitude())
            {
                Console.WriteLine("Please enter a valid value for Latitude!");
            }

            else
            {
                try
                {
                    var client = serviceProvider.GetService<MeteoClient>();
                    var response = InvokeMeteoAPI(client, longitude, latitude, String.IsNullOrEmpty(days) ? "0" : days);

                    try
                    {
                        string jsonString = JsonSerializer.Serialize(response);
                        File.WriteAllText(Constants.FILE_NAME, jsonString);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception while saving the file");
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Exception while calling the client");

                }


            }
        }
    }


    public static  WeatherDetailsResponse InvokeMeteoAPI(MeteoClient client, string? longitude, string latitude, string days)
    {
        var response =  client.GetWeatherDetails(longitude, latitude, string.IsNullOrEmpty(days) ? "7" : days);
        return response.Result.ToResponse();


    }
}




