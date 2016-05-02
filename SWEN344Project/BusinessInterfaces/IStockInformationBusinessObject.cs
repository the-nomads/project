using SWEN344Project.Models;
using SWEN344Project.Models.TransientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN344Project.BusinessInterfaces
{
    public interface IStockInformationBusinessObject
    {
        StockQuote GetStockQuote(string StockSymbol);

        List<StockQuote> GetStockQuotes(List<string> StockSymbols);
    }
}
