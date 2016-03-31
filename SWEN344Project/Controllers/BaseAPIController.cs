using Autofac;
using SWEN344Project.BusinessInterfaces;
using SWEN344Project.Models;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SWEN344Project.Controllers
{
    [RoutePrefix("financialtransactions")]
    public class BaseAPIController : ApiController
    {
        protected HttpResponseMessage CreateOKResponse(object data = null)
        {
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        protected HttpResponseMessage CreateErrorResponse(object data = null)
        {
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        private string GetCurrentUserID()
        {
            string token = HttpContext.Current.Request.Headers.Get("accessToken");
            // TODO: sanitize token
            var request = WebRequest.Create("https://graph.facebook.com/me?access_token=" + token);
            var response = request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();

            var userobj = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookUserInfo>(responseFromServer);
            return userobj.id;
        }

        protected User GetCurrentUser()
        {
            using (var s = Startup.container.BeginLifetimeScope())
            {
                var ubo = Startup.container.Resolve<IUserBusinessObject>();
                var uid = this.GetCurrentUserID();
                var user = ubo.GetOrCreateUser(uid);
                return user;
            }
        }

        private class FacebookUserInfo
        {
            public string name { get; set; }
            public string id { get; set; }
        }

        protected HttpResponseMessage GetOptionsRequest()
        {
            var res = Request.CreateResponse(HttpStatusCode.OK);
            return res;
        }
    }
}