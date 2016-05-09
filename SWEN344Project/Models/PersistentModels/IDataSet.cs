using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SWEN344Project.Models.PersistentModels
{
    public interface IDataSet<T> where T : class
    {
        IQueryable<T> All { get; }

        void DeleteEntity(T toDelete);

        void AddEntity(T toAdd);
    }
}