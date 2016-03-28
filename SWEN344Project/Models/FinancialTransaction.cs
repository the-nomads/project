using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWEN344Project.Models
{
    public class FinancialTransaction
    {
        public int FinancialTransactionID { get; set; }
        public User User { get; set; }
        public decimal Amount { get; set; }
        public bool AmountMovedInToUserAccount { get; set; }
    }
}