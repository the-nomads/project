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
    public interface IFinancialTransactionBusinessObject
    {
        /// <summary>
        /// Gets all the transactions for a given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<FinancialTransaction> GetTransactionsForUser(User user);

        /// <summary>
        /// Gets all the finances (balances) for a given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<UserFinance> GetUserFinances(User user);

        /// <summary>
        /// Gets the finance (balance) for a given user and currency
        /// <para>Refer to Constants.Currency for more information</para>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Currency"></param>
        /// <returns></returns>
        UserFinance GetUserFinance(User user, string Currency);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stockName"></param>
        /// <param name="numSharesToSell"></param>
        /// <returns></returns>
        Tuple<Constants.ReturnValues.StockTransactionResult, FinancialTransaction> SellStock(User user, string stockName, int numSharesToBuy);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stockName"></param>
        /// <param name="numSharesToSell"></param>
        /// <returns></returns>
        Tuple<Constants.ReturnValues.StockTransactionResult, FinancialTransaction> BuyStock(User user, string stockName, int numSharesToBuy);

        /// <summary>
        /// Gets all the stocks a user has or ever has had
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<UserStock> GetUserStocks(User user);

        /// <summary>
        /// Gets a particular stock a user has
        /// <para>May return null if the user has no stocks</para>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="StockName"></param>
        /// <returns></returns>
        UserStock GetUserStock(User user, string StockName);


        /// <summary>
        /// Adds a note to a user stock
        /// <para>If the user hasn't owned this stock before, creates a record with 0 stocks owned.</para>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stockName"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        UserStock AddNoteToUserStock(User user, string stockName, string note);

        /// <summary>
        /// Deletes all transactions for a user
        /// </summary>
        /// <param name="user"></param>
        void DeleteTransactionsForUser(User user);
    }
}
