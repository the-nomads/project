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
    [RoutePrefix("financialtransactions")]
    public class FinancialTransactionController : ApiController
    {
        private readonly IFinancialTransactionBusinessObject _ftbo;
        public FinancialTransactionController(
            IFinancialTransactionBusinessObject ftbo
            )
        {
            this._ftbo = ftbo;
        }


        [HttpGet]
        [Route("get_weather")]
        public async Task<HttpResponseMessage> getWeather([FromUri]string zip = null)
        {
            var response = PerformRequest("http://www.se.rit.edu/~swen-344/activities/rest/RESTAPI-Weather.php?action=get_weather&zip=" + zip);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [Route("get_weather_list")]
        public async Task<HttpResponseMessage> getWeatherList()
        {
            var response = PerformRequest("http://www.se.rit.edu/~swen-344/activities/rest/RESTAPI-Weather.php?action=get_weather_list");
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [Route("get_secret_key")]
        public async Task<HttpResponseMessage> getWeatherKey()
        {
            var response = PerformRequest("http://www.se.rit.edu/~swen-344/activities/rest/RESTAPI-Weather.php?action=get_secret_key");
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        private string PerformRequest(string url)
        {
            var req = WebRequest.Create(url);
            var response = req.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }

        [HttpGet]
        [Route("all")]
        public async Task<HttpResponseMessage> GetAllTransactions([FromUri]string messageOptional = null)
        {
            if (string.IsNullOrEmpty(messageOptional))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Add ?messageOptional= to your URL or /num for an ID");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "message was " + messageOptional);
            }
        }

        [HttpGet]
        [Route("{financialtransactionid:int}")]
        public async Task<HttpResponseMessage> GetOneTransactions(int financialtransactionid)
        {
            var id = this._ftbo.GetTransactions();
            return Request.CreateResponse(HttpStatusCode.OK, "Your id was " + financialtransactionid + " my id was " + id);
        }
    }
}