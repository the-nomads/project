using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWEN344Project.Models.TransientModels
{
    public class WeatherForecastModel
    {
        public City city { get; set; }
        public List<CurrentWeatherModel> list { get; set; }
        public int cnt { get; set; }
        public string cod { get; set; }
        public decimal message { get; set; }

        public class City
        {
            public class Coord
            {
                public decimal lat { get; set; }
                public decimal lon { get; set; }
            }

            public Coord coord { get; set; }
            public string country { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int population { get; set; }
        }
    }
}