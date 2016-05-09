using SWEN344Project.Models;
using SWEN344Project.Models.PersistentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN344Project.BusinessInterfaces
{
    public interface IPersistenceBusinessObject
    {
        IQueryable<Event> ValidEvents { get; }

        IDataSet<User> Users { get; }
        IDataSet<FinancialTransaction> FinancialTransactions { get; }
        IDataSet<Event> Events { get; }
        IDataSet<UserFinance> UserFinances { get; }
        IDataSet<UserStock> UserStocks { get; }

        void SaveChanges();
    }
}
