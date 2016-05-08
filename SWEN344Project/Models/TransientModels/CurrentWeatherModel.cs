using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWEN344Project.Models.TransientModels
{
    public class CurrentWeatherModel
    {
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public string dt_txt { get; set; }

        public List<Weather> weather { get; set; }
        public Wind wind { get; set; }
        public Main main { get; set; }

        public class Clouds
        {
            public int all { get; set; }
        }

        public class Weather
        {
            public string description { get; set; }
            public string icon { get; set; }
            public int id { get; set; }
            public string main { get; set; }
        }

        public class Wind
        {
            public decimal speed { get; set; }
            public decimal deg { get; set; }
        }

        public class Main
        {
            public decimal temp { get; set; }
            public decimal temp_min { get; set; }
            public decimal temp_max { get; set; }
            public decimal pressure { get; set; }
            public decimal humidity { get; set; }

            public decimal? grnd_level { get; set; }
            public decimal? sea_level { get; set; }
            public decimal? temp_kf { get; set; }
        }
    }
}