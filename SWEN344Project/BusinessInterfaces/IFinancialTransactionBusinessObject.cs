using SWEN344Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN344Project.BusinessInterfaces
{
    public interface IFinancialTransactionBusinessObject
    {
        List<FinancialTransaction> GetTransactionsForUser(User user);

        List<UserFinance> GetUserFinances(User user);

        UserFinance GetUserFinance(User user, string Currency);
    }
}
