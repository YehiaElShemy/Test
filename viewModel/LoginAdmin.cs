using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace projectMVC.viewModel
{
    public class LoginAdmin
    {
        [Required(ErrorMessage = "please eenter yor user name ")]
        public string AdminUserName { get; set; }


        [Required(ErrorMessage = "please eenter yor user password ")]

        public string AdminPassword { get; set; }



        [Required(ErrorMessage = "please eenter your code ")]

        public string Code { get; set; }
    }
}