using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWEN344Project.BusinessInterfaces;
using SWEN344Project.Tests.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace SWEN344Project.Tests.UnitTests
{
    [TestClass]
    public class StockTests
    {
        [TestMethod]
        public void Test_Stocks_GetStockQuote()
        {
            this.SetupTest();

            var stock = this.sbo.GetStockQuote("GOOG");
            Assert.IsNotNull(stock);
            Assert.IsNotNull(stock.Ask);
            Assert.AreEqual(stock.Symbol, "GOOG");
        }

        [TestMethod]
        public void Test_Stocks_GetStockQuotes()
        {
            this.SetupTest();

            var stocks = this.sbo.GetStockQuotes(new List<string> { "GOOG", "YHOO" });
            Assert.IsNotNull(stocks);
            Assert.AreEqual(stocks.Count, 2);
            var yahoo = stocks.FirstOrDefault(x => x.Symbol == "YHOO");
            Assert.IsNotNull(yahoo);
            Assert.IsNotNull(yahoo.Ask);
            var google = stocks.FirstOrDefault(x => x.Symbol == "GOOG");
            Assert.IsNotNull(google);
            Assert.IsNotNull(google.Ask);
        }

        private StockInformationBusinessObject sbo;
        private TestUserData tud;
        private TestPersistenceObject pbo;
        private void SetupTest()
        {
            this.pbo = new TestPersistenceObject();
            this.sbo = new StockInformationBusinessObject();
            this.tud = new TestUserData(pbo);
        }
    }
}
