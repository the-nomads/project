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
using Newtonsoft.Json;
using SWEN344Project.Models.PersistentModels;
using SWEN344Project.Helpers;
using SWEN344Project.Models.TransientModels;

namespace SWEN344Project.Controllers
{
    [RoutePrefix("stocks")]
    public class StockController : BaseAPIController
    {
        private readonly IFinancialTransactionBusinessObject _ftbo;
        private readonly IStockInformationBusinessObject _sibo;
        public StockController(
            IFinancialTransactionBusinessObject ftbo,
            IStockInformationBusinessObject sibo,
            IUserBusinessObject ubo
            )
        {
            this._ftbo = ftbo;
            this._sibo = sibo;
            base.ubo = ubo;
        }

        [HttpGet]
        [Route("{stockname}")]
        public HttpResponseMessage GetStockInfo(string stockName)
        {
            try
            {
                var user = this.GetCurrentUser();
                if (user == null)
                {
                    return this.CreateResponse(HttpStatusCode.Unauthorized);
                }

                var stock = this._sibo.GetStockQuote(stockName);
                if (stock == null)
                {
                    return this.CreateResponse(HttpStatusCode.NotFound);
                }
                return this.CreateOKResponse(stock);
            }
            catch (Exception exc)
            {
                return this.CreateErrorResponse(exc);
            }
        }

        [HttpGet]
        [Route("owned")]
        public HttpResponseMessage GetUserStocks()
        {
            try
            {
                var user = this.GetCurrentUser();
                if (user == null)
                {
                    return this.CreateResponse(HttpStatusCode.Unauthorized);
                }

                var stocks = this._ftbo.GetUserStocks(user);
                return this.CreateOKResponse(stocks);
            }
            catch (Exception exc)
            {
                return this.CreateErrorResponse(exc);
            }
        }

        [HttpPut]
        [Route("notes")]
        public HttpResponseMessage WriteUserStockNote()
        {
            try
            {
                var user = this.GetCurrentUser();
                if (user == null)
                {
                    return this.CreateResponse(HttpStatusCode.Unauthorized);
                }

                var str = Request.Content.ReadAsStringAsync().Result;
                var toCreate = JsonConvert.DeserializeObject<StockNote>(str);

                this._ftbo.AddNoteToUserStock(user, toCreate.StockName, toCreate.NoteToPost);
                return this.CreateOKResponse();
            }
            catch (Exception exc)
            {
                return this.CreateErrorResponse(exc);
            }
        }

        [HttpOptions]
        [Route("{stockname}")]
        [Route("notes")]
        [Route("owned")]
        [Route("all")]
        [Route("")]
        public HttpResponseMessage Options()
        {
            return this.GetOptionsRequest();
        }

        private static DateTime FromEpochMilliseconds(double milliSec)
        {
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return startTime.AddMilliseconds(milliSec);
        }
    }
}