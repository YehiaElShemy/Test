using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace projectMVC.viewModel
{
    public class LoginUser
    {
        [Required(ErrorMessage = "please eenter yor user name ")]
        public string CasherUserName { get; set; }


        [Required(ErrorMessage = "please eenter yor user password ")]

        public string CasherPassword { get; set; }



        [Required(ErrorMessage = "please eenter your code ")]

        public string Code { get; set; }
    }
}