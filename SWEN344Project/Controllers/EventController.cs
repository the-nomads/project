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
using SWEN344Project.Models;
using Newtonsoft.Json;

namespace SWEN344Project.Controllers
{
    [RoutePrefix("users/events")]
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
                if (user == null)
                {
                    return this.CreateResponse(HttpStatusCode.Unauthorized);
                }

                var events = this._ebo.GetEventsForUser(user);
                return this.CreateOKResponse(events);
            }
            catch (Exception exc)
            {
                return this.CreateErrorResponse(exc);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateNewEvent()
        {
            try
            {
                var user = this.GetCurrentUser();
                if (user == null)
                {
                    return this.CreateResponse(HttpStatusCode.Unauthorized);
                }

                var str = await Request.Content.ReadAsStringAsync();
                var toCreate = JsonConvert.DeserializeObject<Event>(str);
                this._ebo.CreateNewEvent(user, toCreate);
                return this.CreateOKResponse();
            }
            catch (Exception exc)
            {
                return this.CreateErrorResponse(exc);
            }
        }

        [HttpPut]
        [Route("{eventid}")]
        public async Task<HttpResponseMessage> UpdateEvent(int eventid)
        {
            try
            {
                var user = this.GetCurrentUser();
                if (user == null)
                {
                    return this.CreateResponse(HttpStatusCode.Unauthorized);
                }

                var str = await Request.Content.ReadAsStringAsync();
                var toUpdate = JsonConvert.DeserializeObject<Event>(str);

                var e = this._ebo.GetEvent(eventid);
                if (e == null)
                {
                    return this.CreateResponse(HttpStatusCode.NotFound);
                }
                if (e.UserID != user.UserID)
                {
                    return this.CreateResponse(HttpStatusCode.Forbidden);
                }

                this._ebo.EditEvent(eventid, toUpdate);
                return this.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception exc)
            {
                return this.CreateErrorResponse(exc);
            }
        }

        [HttpDelete]
        [Route("{eventid}")]
        public async Task<HttpResponseMessage> DeleteEvent(int eventid)
        {
            try
            {
                var user = this.GetCurrentUser();
                if (user == null)
                {
                    return this.CreateResponse(HttpStatusCode.Unauthorized);
                }

                var e = this._ebo.GetEvent(eventid);
                if (e == null)
                {
                    return this.CreateResponse(HttpStatusCode.NotFound);
                }
                if (e.UserID != user.UserID)
                {
                    return this.CreateResponse(HttpStatusCode.Forbidden);
                }

                this._ebo.DeleteEvent(eventid);
                return this.CreateOKResponse();
            }
            catch (Exception exc)
            {
                return this.CreateErrorResponse(exc);
            }
        }

        [HttpOptions]
        [Route("all")]
        [Route("{eventid}")]
        [Route("")]
        public HttpResponseMessage Options()
        {
            return this.GetOptionsRequest();
        }
    }
}