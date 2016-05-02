using SWEN344Project.Models;
using SWEN344Project.Models.PersistentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN344Project.BusinessInterfaces
{
    public class PersistenceBusinessObject : IPersistenceBusinessObject, IDisposable
    {
        public PersistenceBusinessObject()
        {
            this._ctx = new CaveWallContext();
            this.FinancialTransactions = new DataSet<FinancialTransaction>(this._ctx.FinancialTransactions);
            this.Users = new DataSet<User>(this._ctx.Users);
            this.Events = new DataSet<Event>(this._ctx.Events);
            this.UserFinances = new DataSet<UserFinance>(this._ctx.UserFinances);
            this.UserStocks = new DataSet<UserStock>(this._ctx.UserStocks);
        }

        private CaveWallContext _ctx;
        public IQueryable<Event> ValidEvents { get { return this.Events.All.Where(x => x.IsDeleted == false); } }

        public DataSet<FinancialTransaction> FinancialTransactions { get; private set; }
        public DataSet<User> Users { get; private set; }
        public DataSet<Event> Events { get; private set; }
        public DataSet<UserFinance> UserFinances { get; private set; }
        public DataSet<UserStock> UserStocks { get; private set; }

        public void SaveChanges()
        {
            this._ctx.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._ctx.Dispose();
            }

        }
    }
}
