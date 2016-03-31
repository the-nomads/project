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

namespace SWEN344Project
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
    }
}
