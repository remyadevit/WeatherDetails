using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WeatherDetails.Connector.Model;
using WeatherDetails.Model;

namespace WeatherDetails.Helper.Constants
{
    public static  class Constants
    {
       public static string  FILE_NAME = String.Format("weatherExport_{0}.json", DateTime.Now.ToString("yyyyMMdd"));
    }
}
