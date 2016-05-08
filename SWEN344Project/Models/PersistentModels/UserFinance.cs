using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWEN344Project.Models.PersistentModels
{
    public class UserFinance
    {
        [Key]
        public int UserFinanceID { get; set; }

        public int UserID { get; set; }



        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}