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

        [HttpDelete]
        [Route("all")]
        public async Task<HttpResponseMessage> DeleteAllTransactions()
        {
            try
            {
                var user = this.GetCurrentUser();
                if (user == null)
                {
                    return this.CreateResponse(HttpStatusCode.Unauthorized);
                }

                this._ftbo.DeleteTransactionsForUser(user);
                return this.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                return this.CreateErrorResponse(exc);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> PurchaseOrSellStock()
        {
            try
            {
                var user = this.GetCurrentUser();
                if (user == null)
                {
                    return this.CreateResponse(HttpStatusCode.Unauthorized);
                }

                var str = await Request.Content.ReadAsStringAsync();
                var toCreate = JsonConvert.DeserializeObject<FinancialTransaction>(str);
                Tuple<Constants.ReturnValues.StockTransactionResult, FinancialTransaction> message;

                if (!toCreate.NumSharesBoughtOrSold.HasValue)
                {
                    return this.CreateResponse(HttpStatusCode.BadRequest, "NumSharesBoughtOrSold is required");
                }
                else if (toCreate.NumSharesBoughtOrSold < 1)
                {
                    return this.CreateResponse(HttpStatusCode.BadRequest, "NumSharesBoughtOrSold must be greater than 0");
                }
                else if (string.IsNullOrEmpty(toCreate.StockName))
                {
                    return this.CreateResponse(HttpStatusCode.BadRequest, "StockName is required");
                }
                else if (toCreate.FinancialTransactionDirection == Constants.FinancialTransactionDirection.IN)
                {
                    // "IN" means the user is getting money, so it's selling a stock
                    message = this._ftbo.SellStock(user, toCreate.StockName, toCreate.NumSharesBoughtOrSold.Value);
                }
                else if (toCreate.FinancialTransactionDirection == Constants.FinancialTransactionDirection.OUT)
                {
                    // "OUT" means the user is losing money, so it's buying a stock
                    message = this._ftbo.BuyStock(user, toCreate.StockName, toCreate.NumSharesBoughtOrSold.Value);
                }
                else
                {
                    return this.CreateResponse(HttpStatusCode.BadRequest, "FinancialTransactionDirection is required");
                }

                if (message.Item1 == Constants.ReturnValues.StockTransactionResult.Success)
                {
                    return this.CreateOKResponse(HttpStatusCode.Created, message.Item2);
                }
                else
                {
                    return this.CreateResponse(HttpStatusCode.InternalServerError, message.Item1.ToString());
                }
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