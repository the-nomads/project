using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWEN344Project.Models.TransientModels
{
    public class StockQuote
    {
        public decimal? Ask { get; set; }
        public decimal? Bid { get; set; }
        public decimal? BookValue { get; set; }
        public string Change { get; set; }
        public string ChangeFromFiftydayMovingAverage { get; set; }
        public string ChangeFromTwoHundreddayMovingAverage { get; set; }
        public string ChangeFromYearHigh { get; set; }
        public string ChangeFromYearLow { get; set; }
        public string Change_PercentChange { get; set; }
        public string ChangeinPercent { get; set; }
        public decimal? DaysHigh { get; set; }
        public decimal? DaysLow { get; set; }
        public string DaysRange { get; set; }

        public string Name { get; set; }
        public string Symbol { get; set; }
    }

    public class YahooFinanceStockQuoteResponseMultiple
    {
        public Query query { get; set; }

        public class Query
        {
            public int count { get; set; }
            public Results results { get; set; }

            public class Results
            {
                public List<StockQuote> quote { get; set; }
            }
        }
    }

    public class YahooFinanceStockQuoteResponseSingle
    {
        public Query query { get; set; }

        public class Query
        {
            public int count { get; set; }
            public Results results { get; set; }

            public class Results
            {
                public StockQuote quote { get; set; }
            }
        }
    }
}