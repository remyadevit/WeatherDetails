using Microsoft.Extensions.DependencyInjection;
using Moq;
using WeatherDetails;
using WeatherDetails.Connector;

namespace WeatherDetails.Tests
{
  
        public class WeatherConsoleAppTests
        {
            private readonly IServiceProvider _serviceProvider;

            public WeatherConsoleAppTests()
            {
                var serviceProvider = new Mock<IServiceProvider>();
                serviceProvider.Setup(x => x.GetService(typeof(MeteoClient))).Returns(new MeteoClient(Mock.Of<HttpClient>()));

                var serviceScope = new Mock<IServiceScope>();
                serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

                var serviceScopeFactory = new Mock<IServiceScopeFactory>();
                serviceScopeFactory.Setup(x => x.CreateScope()).Returns(serviceScope.Object);

                serviceProvider.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(serviceScopeFactory.Object);

                _serviceProvider = serviceProvider.Object;
            }

            
        }
    }
