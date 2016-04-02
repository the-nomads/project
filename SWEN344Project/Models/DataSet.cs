using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SWEN344Project.Models
{
    public class DataSet<T> where T : class
    {
        public DataSet(DbSet<T> backingSource)
        {
            this.backing = backingSource;
        }

        public IQueryable<T> All { get { return getBacking(); } }

        public void DeleteEntity(T toDelete)
        {
            getBacking().Remove(toDelete);
        }

        public void AddEntity(T toAdd)
        {
            getBacking().Add(toAdd);
        }

        // for some compiler reason, you can't say "private DbSet<T> backing;"
        // So for now, here's a workaround where we just cast it instead
        private object backing;
        private DbSet<T> getBacking() { return (DbSet<T>)this.backing; }
    }
}