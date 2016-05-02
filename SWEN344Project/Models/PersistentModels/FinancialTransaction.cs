using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWEN344Project.Models.PersistentModels
{
    public class FinancialTransaction
    {
        [Key]
        public int FinancialTransactionID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string FinancialTransactionDirection { get; set; }
        public DateTime TransactionDate { get; set; }

        public int? NumSharesBoughtOrSold { get; set; }

        /// <summary>
        /// Relates to Constants.TransactionType
        /// </summary>
        public int TransactionTypeID { get; set; }
        public string StockName { get; set; }
    }
}