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
    [RoutePrefix("event")]
    public class EventController : BaseAPIController
    {
        private readonly IEventBusinessObject _ebo;
        public EventController(
            IEventBusinessObject ebo
            )
        {
            this._ebo = ebo;
        }

        [HttpGet]
        [Route("all")]
        public async Task<HttpResponseMessage> GetUserEvents()
        {
            try
            {
                var user = this.GetCurrentUser();
                var events = this._ebo.GetEventsForUser(user);
                return this.CreateOKResponse(events);
            }
            catch (Exception)
            {
                return this.CreateErrorResponse();
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