﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using SWEN344Project.BusinessInterfaces;
using System.Reflection;
using System.Net;
using System.IO;

namespace SWEN344Project.Helpers
{
    public class HttpRequestHelper
    {
        public T PerformRequest<T>(string requestUrl)
        {
            var request = WebRequest.Create(requestUrl);
            var response = request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();

            var instantiation = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseFromServer);
            return instantiation;
        }
    }
}