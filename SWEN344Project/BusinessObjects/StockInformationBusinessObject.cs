using SWEN344Project.Helpers;
using SWEN344Project.Models;
using SWEN344Project.Models.TransientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN344Project.BusinessInterfaces
{
    public class StockInformationBusinessObject : IStockInformationBusinessObject
    {
        private readonly string BaseAPIUrl;
        public StockInformationBusinessObject()
        {
            this.BaseAPIUrl = Constants.ExternalAPIs.YahooFinance.GetBaseUrl();
        }

        public StockQuote GetStockQuote(string StockSymbol)
        {
            var quotes = this.GetStockQuotes(new List<string> { StockSymbol });
            return quotes.First();
        }

        public List<StockQuote> GetStockQuotes(List<string> StockSymbols)
        {
            if (StockSymbols == null || StockSymbols.Count == 0)
            {
                return new List<StockQuote>();
            }

            var symbolsFormatted = StockSymbols.Select(x => "\"" + x + "\"").ToList();
            var yqlQuery = "select * from yahoo.finance.quotes where symbol in (" + string.Join(",", symbolsFormatted) + ")";

            var requestUrl = this.BaseAPIUrl + "?q=" + Uri.EscapeDataString(yqlQuery);
            requestUrl += "&format=json&env=http://datatables.org/alltables.env";

            if (StockSymbols.Count > 1)
            {
                var quotes = new HttpRequestHelper().PerformRequest<YahooFinanceStockQuoteResponseMultiple>(requestUrl);
                return quotes.query.results.quote;
            }
            else
            {
                var quotes = new HttpRequestHelper().PerformRequest<YahooFinanceStockQuoteResponseSingle>(requestUrl);
                return new List<StockQuote> { quotes.query.results.quote };
            }
        }

    }
}
