using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWEN344Project.Models.PersistentModels
{
    public class UserStock
    {
        [Key]
        public int UserStockID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public int NumberOfStocks { get; set; }
        public string StockName { get; set; }

        public int TotalNumberOfStocksSold { get; set; }
        public int TotalNumberOfStocksBought { get; set; }
        public decimal TotalValueOfStocksSold { get; set; }
        public decimal TotalValueOfStocksBought { get; set; }

        public string UserNote { get; set; }
        public DateTime? UserNoteSet { get; set; }
    }
}