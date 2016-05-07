using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWEN344Project.Models.TransientModels
{
    public class StockNote
    {
        public string StockName { get; set; }
        public string NoteToPost { get; set; }
    }
}