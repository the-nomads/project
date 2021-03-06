﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using SWEN344Project.BusinessInterfaces;
using System.IO;

namespace SWEN344Project.Controllers
{
    [RoutePrefix("weather")]
    public class WeatherController : BaseAPIController
    {
        private readonly IWeatherBusinessObject _wbo;
        public WeatherController(
            IWeatherBusinessObject wbo,
            IUserBusinessObject ubo
            )
        {
            this._wbo = wbo;
            base.ubo = ubo;
        }


        [HttpGet]
        [Route("current/{zipcode}")]
        public HttpResponseMessage GetCurrentWeather(int zipcode)
        {
            try
            { 
                var weather = this._wbo.GetCurrentWeather(zipcode);
                return this.CreateOKResponse(weather);
            }
            catch (Exception exc)
            {
                return this.CreateErrorResponse(exc);
            }
        }

        [HttpGet]
        [Route("forecast/{zipcode}")]
        public HttpResponseMessage GetWeatherForecast(int zipcode)
        {
            try
            {
                var weather = this._wbo.GetWeatherForecast(zipcode);
                return this.CreateOKResponse(weather);
            }
            catch (Exception exc)
            {
                return this.CreateErrorResponse(exc);
            }
        }

        [HttpOptions]
        [Route("")]
        [Route("forecast/{zipcode}")]
        [Route("current/{zipcode}")]
        public HttpResponseMessage Options()
        {
            return this.GetOptionsRequest();
        }
    }
}