using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using projectMVC.enums;

namespace projectMVC.viewModel
{
    public class SeetsViewModel
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "please enter number")]
        public int Number { get; set; }
        [Required(ErrorMessage = "please select type")]
        public SeetType SeetType { get; set; }

        [Required(ErrorMessage = "please enter the cost ")]
        public decimal Cost { get; set; }
    

    }
}