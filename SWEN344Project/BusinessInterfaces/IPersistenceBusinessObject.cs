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

        DataSet<User> Users { get; }
        DataSet<FinancialTransaction> FinancialTransactions { get; }
        DataSet<Event> Events { get; }
        DataSet<UserFinance> UserFinances { get; }

        void SaveChanges();
    }
}
