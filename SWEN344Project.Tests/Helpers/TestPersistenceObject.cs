using SWEN344Project.Models;
using SWEN344Project.Models.PersistentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN344Project.BusinessInterfaces
{
    public class TestPersistenceObject : IPersistenceBusinessObject, IDisposable
    {
        public TestPersistenceObject()
        {
            this.FinancialTransactions = new TestDataSet<FinancialTransaction>();
            this.Users = new TestDataSet<User>();
            this.Events = new TestDataSet<Event>();
            this.UserFinances = new TestDataSet<UserFinance>();
            this.UserStocks = new TestDataSet<UserStock>();
        }

        private class TestDataSet<T> : IDataSet<T> where T : class
        {
            public TestDataSet()
            {
                this.backing = new List<T>();
            }

            public IQueryable<T> All { get { return this.backing.AsQueryable(); } }

            public void DeleteEntity(T toDelete)
            {
                this.backing.Remove(toDelete);
            }

            public void AddEntity(T toAdd)
            {
                this.backing.Add(toAdd);
            }

            private List<T> backing;
        }

        public IQueryable<Event> ValidEvents { get { return this.Events.All.Where(x => x.IsDeleted == false); } }

        public IDataSet<FinancialTransaction> FinancialTransactions { get; private set; }
        public IDataSet<User> Users { get; private set; }
        public IDataSet<Event> Events { get; private set; }
        public IDataSet<UserFinance> UserFinances { get; private set; }
        public IDataSet<UserStock> UserStocks { get; private set; }

        public void SaveChanges()
        {

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
            }

        }
    }
}
