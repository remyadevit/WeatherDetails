using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDetails.Model
{
    public class DailyWeatherDetails
    {

        public DateTime Date { get; set; }
        public double MaxTemperatureInCelsius { get; set; }

        public double MaxTemperatureInFarenheit { get; set; }

        public double MinTemperatureInCelsius { get; set; }

        public double MinTemperatureInFarenheit { get; set; }


        public double MaxSnowfall { get; set; }

        public double MinSnowfall { get; set; }

    }
}
