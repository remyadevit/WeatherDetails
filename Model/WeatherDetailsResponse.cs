using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDetails.Model
{
    public class WeatherDetailsResponse
    {
        public List<DailyWeatherDetails> dailyWeatherDetails { get; set; }
    }
}
