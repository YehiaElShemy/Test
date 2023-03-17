using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace projectMVC.Models
{
    public class Requirement
    {
        public int Id { get; set; }
        public float Rent { get; set; }
        public float WaterBill { get; set; }

        public float ElectricityBill { get; set; }

        public string Code { get; set; }

    }
}