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
    }
}
