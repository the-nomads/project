using SWEN344Project.Helpers;
using SWEN344Project.Models;
using SWEN344Project.Models.PersistentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN344Project.BusinessInterfaces
{
    public class FinancialTransactionBusinessObject : IFinancialTransactionBusinessObject
    {
        private readonly IPersistenceBusinessObject _pbo;
        private readonly IStockInformationBusinessObject _sibo;
        public FinancialTransactionBusinessObject(
            IPersistenceBusinessObject pbo,
            IStockInformationBusinessObject sibo
            )
        {
            this._pbo = pbo;
            this._sibo = sibo;
        }

        public List<FinancialTransaction> GetTransactionsForUser(User user)
        {
            var events = this._pbo.FinancialTransactions.All
                .Where(x => x.UserID == user.UserID)
                .OrderByDescending(x => x.TransactionDate)
                .ToList();

            return events;
        }

        public List<UserFinance> GetUserFinances(User user)
        {
            var finances = this._pbo.UserFinances.All
                .Where(x => x.UserID == user.UserID)
                .ToList();

            return finances;
        }
        public UserFinance GetUserFinance(User user, string Currency)
        {
            var finance = this._pbo.UserFinances.All
                .First(x => x.UserID == user.UserID && x.Currency == Currency);

            return finance;
        }

        public List<UserStock> GetUserStocks(User user)
        {
            var userStocks = this._pbo.UserStocks.All.Where(x => x.UserID == user.UserID).ToList();

            return userStocks;
        }

        public UserStock GetUserStock(User user, string StockName)
        {
            var userStock = this._pbo.UserStocks.All.FirstOrDefault(x => x.UserID == user.UserID && x.StockName == StockName);

            return userStock;
        }

        private FinancialTransaction CreateAndSaveFinancialTransaction(
            User user,
            decimal amount,
            Constants.FinancialTransactionType transactiontype,
            string stockName = null, 
            int? numStocks = null)
        {
            var ft = new FinancialTransaction();
            ft.Amount = amount;
            if (ft.Amount < 0)
            {
                ft.Amount *= -1; // all transactions must be positive
            }

            ft.StockName = stockName;
            ft.NumSharesBoughtOrSold = numStocks;
            ft.TransactionDate = DateTime.Now;
            ft.UserID = user.UserID;


            string direction;
            switch (transactiontype)
            {
                case Constants.FinancialTransactionType.Deposit:
                    direction = Constants.FinancialTransactionDirection.IN;
                    break;
                case Constants.FinancialTransactionType.Withdrawal:
                    direction = Constants.FinancialTransactionDirection.OUT;
                    break;
                case Constants.FinancialTransactionType.StockPurchase:
                    direction = Constants.FinancialTransactionDirection.OUT;
                    break;
                case Constants.FinancialTransactionType.StockSale:
                    direction = Constants.FinancialTransactionDirection.IN;
                    break;
                default:
                    throw new NotImplementedException("Must implement financial direction for financial type: " + transactiontype.ToString());
            }

            ft.FinancialTransactionDirection = direction;

            this._pbo.FinancialTransactions.AddEntity(ft);
            this._pbo.SaveChanges();

            return ft;
        }

        public Constants.ReturnValues.StockTransactionResult SellStock(User user, string stockName, int numSharesToSell)
        {
            // Load the stock record containing how many stocks the user has
            // If they have none, then return InsufficientStocks
            var userStock = this.GetUserStock(user, stockName);
            if (userStock == null || userStock.NumberOfStocks < numSharesToSell)
            {
                return Constants.ReturnValues.StockTransactionResult.InsufficientStocks;
            }

            // Load up the price of the stock
            var stockQuote = this._sibo.GetStockQuote(stockName);
            var stockPrice = stockQuote.Ask; // Ask is the asking price of the stock, what you sell it for

            var totalPrice = (stockPrice * numSharesToSell);

            // Make a new transaction for the sale
            this.CreateAndSaveFinancialTransaction(user, totalPrice, Constants.FinancialTransactionType.StockSale, stockName, numSharesToSell);

            // Update the user's finance to have the amount they just sold
            var uf = this.GetUserFinance(user, Constants.Currency.USD);
            uf.Amount += totalPrice;
            this._pbo.SaveChanges();

            // Update the user's stock to now have the number of shares and total values
            userStock.NumberOfStocks -= numSharesToSell;
            userStock.TotalNumberOfStocksSold += numSharesToSell;
            userStock.TotalValueOfStocksSold += totalPrice;
            this._pbo.SaveChanges();

            return Constants.ReturnValues.StockTransactionResult.Success;
        }

        public Constants.ReturnValues.StockTransactionResult BuyStock(User user, string stockName, int numSharesToBuy)
        {
            var stockQuote = this._sibo.GetStockQuote(stockName);
            var stockPrice = stockQuote.Bid; // Bid is the bididng price of the stock, what you purchase it for

            var totalPrice = (stockPrice * numSharesToBuy);

            // Load up the user's finance. If they don't have enough money, return Insufficient Funds
            var uf = this.GetUserFinance(user, Constants.Currency.USD);
            if (uf.Amount < totalPrice)
            {
                return Constants.ReturnValues.StockTransactionResult.InsufficientFunds;
            }

            // Make a new transaction for the purchase
            this.CreateAndSaveFinancialTransaction(user, totalPrice, Constants.FinancialTransactionType.StockPurchase, stockName, numSharesToBuy);

            // Update the user's finance to remove the money they just used
            uf.Amount -= totalPrice;
            this._pbo.SaveChanges();

            // Load the stock record containing how many stocks the user has
            // If they have none, it means they have not dealt with this stock before
            var userStock = this.GetUserStock(user, stockName);
            if (userStock == null)
            {
                // Create the record for this stock and add it to the table
                userStock = new UserStock();
                userStock.StockName = stockName;
                userStock.NumberOfStocks = 0;
                userStock.TotalNumberOfStocksBought = 0;
                userStock.TotalNumberOfStocksSold = 0;
                userStock.TotalValueOfStocksBought = 0;
                userStock.TotalValueOfStocksSold = 0;
                userStock.UserID = user.UserID;
                this._pbo.UserStocks.AddEntity(userStock);
            }

            // Update the user's stock to now have the number of shares and total values
            userStock.TotalNumberOfStocksBought += numSharesToBuy;
            userStock.TotalValueOfStocksBought += totalPrice;
            userStock.NumberOfStocks += numSharesToBuy;

            this._pbo.SaveChanges();

            return Constants.ReturnValues.StockTransactionResult.Success;
        }
    }
}
