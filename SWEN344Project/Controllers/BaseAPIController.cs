using Autofac;
using SWEN344Project.BusinessInterfaces;
using SWEN344Project.Helpers;
using SWEN344Project.Models;
using SWEN344Project.Models.PersistentModels;
using System;
using System.Collections.Generic;
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
        protected IUserBusinessObject ubo;

        protected HttpResponseMessage CreateResponse(HttpStatusCode code, object data = null)
        {
            if (data != null)
            {
                var response = Request.CreateResponse(code);
                response.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return response;
            }
            else
            {
                return Request.CreateResponse(code, "null");
            }
        }

        protected HttpResponseMessage CreateOKResponse(HttpStatusCode code, object data = null)
        {
            return this.CreateResponse(code, data);
        }

        protected HttpResponseMessage CreateOKResponse(object data = null)
        {
            return this.CreateResponse(HttpStatusCode.OK, data);
        }

        protected HttpResponseMessage CreateErrorResponse(object data = null)
        {
            if (data != null)
            {
                Exception exc = data as Exception;

                if (exc != null) // It is an exception
                {
                    bool ShowException = false;
#if DEBUG
                    ShowException = true;
#endif

                    if (ShowException)
                    {

                        string msg = "";
                        int layers = 0;
                        do
                        {
                            if (layers++ > 100)
                                break;

                            if (layers > 1)
                                msg += "Inner Exception " + (layers - 1) + ": \r\n";
                            else
                                msg += "Exception: \r\n";

                            msg += exc.Message + "\r\n\r\n";
                            msg += "Stack Trace: \r\n";
                            msg += exc.StackTrace;
                            msg += "\r\n\r\n";

                            exc = exc.InnerException;
                        }
                        while (exc != null);

                        return Request.CreateResponse(HttpStatusCode.InternalServerError, msg);
                    }
                    else // don't show the exception, so just respond with InternalServerError
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError);
                    }
                }
                else // The data is not an exepction, just serialize it and move on
                {
                    var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                    response.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    return response;
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }


        private static Dictionary<string, string> useridcache = new Dictionary<string, string>();
        private string GetCurrentUserID()
        {
            string token = HttpContext.Current.Request.Headers.Get("accessToken");
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }


            string id;
            if (!useridcache.TryGetValue(token, out id))
            {
                var requestUrl = Constants.ExternalAPIs.Facebook.GetUserRequestUrl(token);

                var userobj = new HttpRequestHelper().PerformRequest<FacebookUserInfo>(requestUrl);
                if (userobj == null || userobj.id == null)
                {
                    return null;
                }
                id = userobj.id;

                lock (useridcache)
                {
                    if (!useridcache.ContainsKey(token))
                        useridcache.Add(token, id);
                }
            }
            return id;
        }

        protected User GetCurrentUser()
        {
            var uid = this.GetCurrentUserID();
            if (string.IsNullOrWhiteSpace(uid))
            {
                return null;
            }
            var user = ubo.GetOrCreateUser(uid);
            return user;
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