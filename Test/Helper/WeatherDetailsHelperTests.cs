using Xunit;
using WeatherDetails.Helper;
using WeatherDetails.Connector.Model;
using System.Reflection;
using System.Text.Json;
using WeatherDetails.Model;

namespace WeatherDetailsTests.Helper
{
    public  class WeatherDetailsHelperTests
    {
        [Theory]
        [InlineData(100,212)]
        [InlineData(0, 32)]
        [InlineData(-10, 14)]
        [InlineData(10.34, 50.612)]
        public void ToFarenheit_ReturnsValue(double input, double expectedOutput)
        {
            var output = input.ToFarenheit();
            Assert.Equal(expectedOutput, output);

        }

        [Theory]
        [InlineData("0", true)]
        [InlineData("390", false)]
        [InlineData("-150", true)]
        [InlineData("52.52", true)]

        public void IsValidLongitude_ReturnsValue(string longitude, bool isValid)
        {
            var output = longitude.IsValidLongitude();
            Assert.Equal(isValid, output);

        }


        [Theory]
        [InlineData("0", true)]
        [InlineData("200", false)]
        [InlineData("-100", false)]
        [InlineData("60", true)]
        [InlineData("13.42", true)]
        public void IsValidLatitude_ReturnsValue(string latitude, bool isValid)
        {
            var output = latitude.IsValidLatitude();
            Assert.Equal(isValid, output);

        }

        [Fact]
        public void ToResponse_ReturnsSuccess()
        {
            var expectedResponse = new WeatherDetailsResponse();

            expectedResponse=  JsonSerializer.Deserialize<WeatherDetailsResponse>(ReadFromFile("WeatherDetailsTest.Inputs.weatherExport_20230503.json"));

            var response = CreateMeteoResponse().ToResponse();
            Assert.Equal(JsonSerializer.Serialize(expectedResponse), JsonSerializer.Serialize(response));
        }

        [Fact]
        public void ToResponse_Returns_EmptySnowFall()
        {
            var expectedResponse = new WeatherDetailsResponse();

            expectedResponse = JsonSerializer.Deserialize<WeatherDetailsResponse>(ReadFromFile("WeatherDetailsTest.Inputs.weatherExport_20230503.json"));

            var response = CreateMeteoResponse().ToResponse();
            Assert.Equal(JsonSerializer.Serialize(expectedResponse), JsonSerializer.Serialize(response));
        }


        private MeteoResponse CreateMeteoResponse()
        {
            return JsonSerializer.Deserialize<MeteoResponse>(ReadFromFile("WeatherDetailsTest.Inputs.Meteo_Success_EmptySnowfall.json"));
        }



        private string ReadFromFile(string resourceName)
        {

            var assembly = Assembly.GetExecutingAssembly();            
            var result = string.Empty;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;

        }
    }
}
