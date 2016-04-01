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
                var events = this._ebo.GetEventsForUser(user);
                return this.CreateOKResponse(events);
            }
            catch (Exception)
            {
                return this.CreateErrorResponse();
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateNewEvent()
        {
            try
            {
                var str = await Request.Content.ReadAsStringAsync();
                var toCreate = JsonConvert.DeserializeObject<Event>(str);
                var user = this.GetCurrentUser();
                this._ebo.CreateNewEvent(user, toCreate);
                return this.CreateOKResponse();
            }
            catch (Exception)
            {
                return this.CreateErrorResponse();
            }
        }

        //[HttpPut]
        //[Route("{eventid}")]
        //public async Task<HttpResponseMessage> UpdateEvent(int eventid)
        //{
        //    try
        //    {
        //        var str = await Request.Content.ReadAsStringAsync();
        //        var toUpdate = JsonConvert.DeserializeObject<Event>(str);
        //        var user = this.GetCurrentUser();


        //        this._ebo.EditEvent(user, eventid, toUpdate);
        //        return Request.CreateResponse(HttpStatusCode.Created);
        //    }
        //    catch (Exception)
        //    {
        //        return this.CreateErrorResponse();
        //    }
        //}

        //[HttpDelete]
        //[Route("{eventid}")]
        //public async Task<HttpResponseMessage> DeleteEvent(int eventid)
        //{
        //    try
        //    {
        //        var user = this.GetCurrentUser();
        //        var e = this._ebo.GetEvent(eventid);
        //        if (e.UserID != user.UserID)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.Forbidden);
        //        }
        //        this._ebo.DeleteEvent(eventid);
        //        return this.CreateOKResponse();
        //    }
        //    catch (Exception)
        //    {
        //        return this.CreateErrorResponse();
        //    }
        //}

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