using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SWEN344Project.Models.PersistentModels
{
    public class CaveWallContext : DbContext
    {
        public CaveWallContext()
            : base("DefaultConnection")
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UserFinance> UserFinances { get; set; }
        public DbSet<UserStock> UserStocks { get; set; }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}