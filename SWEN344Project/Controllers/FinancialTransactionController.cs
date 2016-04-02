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
            try
            {
                var user = this.GetCurrentUser();
                if (user == null)
                {
                    return this.CreateResponse(HttpStatusCode.Unauthorized);
                }

                var transactions = this._ftbo.GetTransactionsForUser(user);
                return this.CreateOKResponse(transactions);
            }
            catch (Exception exc)
            {
                return this.CreateErrorResponse(exc);
            }
        }


        [HttpOptions]
        [Route("all")]
        [Route("")]
        public HttpResponseMessage Options()
        {
            return this.GetOptionsRequest();
        }
    }
}