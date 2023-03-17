using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace projectMVC.viewModel
{
    public class FoodViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "please enter drink Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "please enter the buyment cost")]
        public decimal Buy { get; set; }
        [Required(ErrorMessage = "please enter the salies ")]
        public decimal Sale { get; set; }
        [Required(ErrorMessage = "please enter the profit ")]
        public decimal Profit { get; set; }
        [Required(ErrorMessage = "please enter amount")]
        public int Amonut { get; set; }
    }
}