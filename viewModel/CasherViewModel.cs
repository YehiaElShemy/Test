using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace projectMVC.viewModel
{
    public class CasherViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "please enter your Name ")]

        public string Name { get; set; }

        [Required(ErrorMessage = "please enter your phone ")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "please enter your salary ")]

        public decimal Salary { get; set; }

        [Required(ErrorMessage = "please enter your Adress ")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "please enter your UserName ")]

        public string CasherUserName { get; set; }
        [Required(ErrorMessage = "please enter your Password ")]

        public string CasherPassword { get; set; }

    }
}