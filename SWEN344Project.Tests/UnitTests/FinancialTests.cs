using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWEN344Project.BusinessInterfaces;
using SWEN344Project.Tests.Helpers;
using SWEN344Project.Models.PersistentModels;
using SWEN344Project.Helpers;
using System.Linq;

namespace SWEN344Project.Tests.UnitTests
{
    [TestClass]
    public class FinancialTests
    {
        [TestMethod]
        public void Test_Finance_GetUserFinance()
        {
            this.SetupTest();

            var finance = this.fbo.GetUserFinance(this.user, Constants.Currency.USD);
            Assert.IsNotNull(finance);
            Assert.AreEqual(finance.Amount, Constants.MoneyUsersStartWith);
            Assert.AreEqual(finance.UserID, this.user.UserID);
        }

        [TestMethod]
        public void Test_Finance_GetUserFinances()
        {
            this.SetupTest();

            var finances = this.fbo.GetUserFinances(this.user);
            var finance = finances.First();
            Assert.IsNotNull(finance);
            Assert.AreEqual(finance.Amount, Constants.MoneyUsersStartWith);
            Assert.AreEqual(finance.UserID, this.user.UserID);
        }

        [TestMethod]
        public void Test_Finance_AddStockNote()
        {
            this.SetupTest();

            this.fbo.AddNoteToUserStock(this.user, "GOOG", "Notey Note note");
            var stock = this.fbo.GetUserStock(this.user, "GOOG");
            Assert.IsNotNull(stock);
            Assert.AreEqual(stock.UserNote, "Notey Note note");

            this.fbo.AddNoteToUserStock(this.user, "GOOG", "Notey Note note 2");
            stock = this.fbo.GetUserStock(this.user, "GOOG");
            Assert.IsNotNull(stock);
            Assert.AreEqual(stock.UserNote, "Notey Note note 2");
        }

        [TestMethod]
        public void Test_Finance_GetUserStock()
        {
            this.SetupTest();

            this.fbo.AddNoteToUserStock(this.user, "GOOG", "Notey Note note");
            var stock = this.fbo.GetUserStock(this.user, "GOOG");
            Assert.IsNotNull(stock);
            Assert.AreEqual(stock.UserNote, "Notey Note note");
        }


        [TestMethod]
        public void Test_Finance_GetUserStocks()
        {
            this.SetupTest();

            this.fbo.AddNoteToUserStock(this.user, "GOOG", "Notey Note note");
            this.fbo.AddNoteToUserStock(this.user, "YHOO", "Notey Note note 2");
            var stocks = this.fbo.GetUserStocks(this.user);
            Assert.AreEqual(stocks.Count, 2);
            var google = stocks.FirstOrDefault(x => x.StockName == "GOOG");
            Assert.IsNotNull(google);
            var yahoo = stocks.FirstOrDefault(x => x.StockName == "YHOO");
            Assert.IsNotNull(yahoo);
        }

        [TestMethod]
        public void Test_Finance_BuyStock()
        {
            this.SetupTest();

            var res = this.fbo.BuyStock(this.user, "GOOG", 1);
            var stocks = this.fbo.GetUserStocks(this.user);

            var stock = stocks.FirstOrDefault(x => x.StockName == "GOOG");
            Assert.AreEqual(res.Item1, Constants.ReturnValues.StockTransactionResult.Success);
            Assert.IsNotNull(stock);
            Assert.AreEqual(stock.NumberOfStocks, 1);

            res = this.fbo.BuyStock(this.user, "ASDFDFDFDFEFEFGRFGTRIUGGRJGFGIOFGJDOJDFAOSDJFASDFASDFJRG", 1);
            Assert.AreEqual(res.Item1, Constants.ReturnValues.StockTransactionResult.StockNotFound);

            res = this.fbo.BuyStock(this.user, "GOOG", 999999);
            Assert.AreEqual(res.Item1, Constants.ReturnValues.StockTransactionResult.InsufficientFunds);
        }

        [TestMethod]
        public void Test_Finance_SellStock()
        {
            this.SetupTest();

            var res = this.fbo.BuyStock(this.user, "GOOG", 1);
            var stocks = this.fbo.GetUserStocks(this.user);

            var stock = stocks.FirstOrDefault(x => x.StockName == "GOOG");
            Assert.AreEqual(res.Item1, Constants.ReturnValues.StockTransactionResult.Success);
            Assert.IsNotNull(stock);
            Assert.AreEqual(stock.NumberOfStocks, 1);

            res = this.fbo.SellStock(this.user, "ASDFDFDFDFEFEFGRFGTRIUGGRJGFGIOFGJDOJDFAOSDJFASDFASDFJRG", 1);
            Assert.AreEqual(res.Item1, Constants.ReturnValues.StockTransactionResult.StockNotFound);

            res = this.fbo.SellStock(this.user, "GOOG", 999999);
            Assert.AreEqual(res.Item1, Constants.ReturnValues.StockTransactionResult.InsufficientStocks);

            res = this.fbo.SellStock(this.user, "GOOG", 1);
            Assert.AreEqual(res.Item1, Constants.ReturnValues.StockTransactionResult.Success);
        }

        private FinancialTransactionBusinessObject fbo;
        private TestUserData tud;
        private TestPersistenceObject pbo;
        private User user;
        private void SetupTest()
        {
            this.pbo = new TestPersistenceObject();
            var sbo = new StockInformationBusinessObject();
            this.fbo = new FinancialTransactionBusinessObject(pbo, sbo);
            this.tud = new TestUserData(pbo);
            this.user = new UserBusinessObject(this.pbo).GetOrCreateUser("9999");
        }
    }
}
