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
        public FinancialTransactionBusinessObject(IPersistenceBusinessObject pbo)
        {
            this._pbo = pbo;
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


        private FinancialTransaction CreateAndSaveFinancialTransaction(
            User user,
            decimal amount,
            Constants.FinancialTransactionType transactiontype,
            string stockName = null)
        {
            var ft = new FinancialTransaction();
            ft.Amount = amount;
            if (ft.Amount < 0)
            {
                ft.Amount *= -1; // all transactions must be positive
            }

            ft.StockName = stockName;
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
    }
}
