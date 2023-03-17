using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using projectMVC.Models;
namespace projectMVC.viewModel
{
    public class DrinksAndSeetCasher
    {
        public List<Seet> Seets { get; set; }
        public List<Drink> drinks { get; set; }
    }
}