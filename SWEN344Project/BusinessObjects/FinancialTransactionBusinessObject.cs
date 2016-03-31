using SWEN344Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN344Project.BusinessInterfaces
{
    public class FinancialTransactionBusinessObject : IFinancialTransactionBusinessObject
    {
        public List<FinancialTransaction> GetTransactionsForUser(User user)
        {
            using (var ctx = new CaveWallContext())
            {
                var events = ctx.FinancialTransactions.Where(x => x.UserID == user.UserID).OrderByDescending(x => x.TransactionDate).ToList();
                return events;
            }
        }

        public List<UserFinance> GetUserFinances(User user)
        {
            using (var ctx = new CaveWallContext())
            {
                var finances = ctx.UserFinance.Where(x => x.UserID == user.UserID).ToList();
                return finances;
            }
        }
        public UserFinance GetUserFinance(User user, string Currency)
        {
            using (var ctx = new CaveWallContext())
            {
                var finance = ctx.UserFinance.First(x => x.UserID == user.UserID && x.Currency == Currency);
                return finance;
            }
        }


        private FinancialTransaction CreateAndSaveFinancialTransaction(
            User user, 
            decimal amount, 
            Constants.FinancialTransactionType transactiontype,
            string stockName = null)
        {
            using (var ctx = new CaveWallContext())
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

                ctx.FinancialTransactions.Add(ft);
                ctx.SaveChanges();

                return ft;
            }
        } 
    }
}
