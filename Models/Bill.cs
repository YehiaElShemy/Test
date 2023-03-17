using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace projectMVC.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserName  { get; set; }
        public string Names { get; set; }
        public String Total { get; set; }
        public string Code { get; set; }



    }
}