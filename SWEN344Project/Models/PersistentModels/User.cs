using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SWEN344Project.Models.PersistentModels
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string FacebookID { get; set; }
        //public virtual ICollection<Event> Events { get; set; }
        //public virtual ICollection<FinancialTransaction> FinancialTransactions { get; set; }
        //public virtual ICollection<UserFinance> UserFinances { get; set; } 
    }
}