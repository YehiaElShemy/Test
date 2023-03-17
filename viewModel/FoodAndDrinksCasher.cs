using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using projectMVC.Models;
namespace projectMVC.viewModel
{
    public class FoodAndDrinksCasher
    {
        public List<Food> foods { get; set; }
        public List<Drink> drinks { get; set; }


    }
}