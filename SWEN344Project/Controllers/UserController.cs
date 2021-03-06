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
using SWEN344Project.Helpers;

namespace SWEN344Project.Controllers
{
    [RoutePrefix("users")]
    public class UserController : BaseAPIController
    {
        private readonly IFinancialTransactionBusinessObject _ftbo;
        public UserController(
            IFinancialTransactionBusinessObject fbo,
            IUserBusinessObject ubo
            )
        {
            this._ftbo = fbo;
            base.ubo = ubo;
        }

        [HttpGet]
        [Route("balance")]
        public HttpResponseMessage GetUserBalance()
        {
            try
            {
                var user = this.GetCurrentUser();
                if (user == null)
                {
                    return this.CreateResponse(HttpStatusCode.Unauthorized);
                }

                var finance = this._ftbo.GetUserFinance(user, Constants.Currency.USD);
                return this.CreateOKResponse(finance);
            }
            catch (Exception exc)
            {
                return this.CreateErrorResponse(exc);
            }
        }

        [HttpOptions]
        [Route("balance")]
        [Route("")]
        public HttpResponseMessage Options()
        {
            return this.GetOptionsRequest();
        }
    }
}