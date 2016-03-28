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
    public class FinancialTransactionController : BaseAPIController
    {
        private readonly IFinancialTransactionBusinessObject _ftbo;
        public FinancialTransactionController(
            IFinancialTransactionBusinessObject ftbo
            )
        {
            this._ftbo = ftbo;
        }

        [HttpGet]
        [Route("all")]
        public async Task<HttpResponseMessage> GetAllTransactions()
        {
            return this.CreateOKResponse(new { a = 1, b = 2 });
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