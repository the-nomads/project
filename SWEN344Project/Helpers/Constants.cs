using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using SWEN344Project.BusinessInterfaces;
using System.Reflection;

namespace SWEN344Project.Helpers
{
    public class Constants
    {
        public enum FinancialTransactionType
        {
            Withdrawal = 1,
            Deposit = 2,
            StockPurchase = 3,
            StockSale = 4,
        }

        public class Currency
        {
            public const string USD = "USD";
            public const string GPB = "GPB";
        }


        public class FinancialTransactionDirection
        {
            public const string IN = "IN";
            public const string OUT = "OUT";
        }

        public const int MoneyUsersStartWith = 1000;

        public class ExternalAPIs
        {
            public class OpenWeatherMap
            {
                public static string GetOpenWeatherMapAPIKey()
                {
                    return "8926acf8591f7289f1c62fb3a554b820";
                }

                public static string GetOpenWeatherMapAPIBaseUrl()
                {
                    return "http://api.openweathermap.org/data/2.5/";
                }

            }

            public class Facebook
            {
                public static string GetFacebookBaseUrl()
                {
                    return "https://graph.facebook.com/me";
                }

                public static string GetUserRequestUrl(string accessToken)
                {
                    // TODO: sanatize token
                    return GetFacebookBaseUrl() + "?access_token=" + accessToken;
                }
            }
            
        }
    }
}
