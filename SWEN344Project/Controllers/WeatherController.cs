using System;
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
    [RoutePrefix("users/financialtransactions")]
    public class WeatherController : BaseAPIController
    {
        private readonly IWeatherBusinessObject _wbo;
        public WeatherController(
            IWeatherBusinessObject wbo
            )
        {
            this._wbo = wbo;
        }


        [HttpOptions]
        [Route("")]
        public HttpResponseMessage Options()
        {
            return this.GetOptionsRequest();
        }
    }
}