using SWEN344Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWEN344Project.Models.TransientModels;
using System.Net;
using System.IO;
using SWEN344Project.Helpers;

namespace SWEN344Project.BusinessInterfaces
{
    public class WeatherBusinessObject : IWeatherBusinessObject
    {
        private readonly string APIKey;
        private readonly string BaseAPIUrl;
        public WeatherBusinessObject()
        {
            this.APIKey = Constants.ExternalAPIs.OpenWeatherMap.GetOpenWeatherMapAPIKey();
            this.BaseAPIUrl = Constants.ExternalAPIs.OpenWeatherMap.GetOpenWeatherMapAPIBaseUrl();
        }

        public CurrentWeatherModel GetCurrentWeather(int zipCode)
        {
            var requestUrl = this.BaseAPIUrl + "weather?zip=" + zipCode + ",us&appid=" + this.APIKey;
            var weather = new HttpRequestHelper().PerformRequest<CurrentWeatherModel>(requestUrl);
            return weather;
        }

        public WeatherForecastModel GetWeatherForecast(int zipCode)
        {
            var requestUrl = this.BaseAPIUrl + "forecast?zip=" + zipCode + ",us&appid=" + this.APIKey + "&cnt=12";
            var weather = new HttpRequestHelper().PerformRequest<WeatherForecastModel>(requestUrl);
            return weather;
        }
    }
}
