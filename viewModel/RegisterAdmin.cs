using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using projectMVC.enums;
namespace projectMVC.viewModel
{
    public class RegisterAdmin
    {
        [Required(ErrorMessage = "please eenter yor user name ")]
        public string AdminUserName { get; set; }


        [Required(ErrorMessage = "please eenter yor user password ")]
     
        public string AdminPassword { get; set; }

        [Required(ErrorMessage = "please select your bussines type")]
        public BussinessType BussinesType { get; set; }

        [Required(ErrorMessage = "please eenter your code ")]
      

        public string Code { get; set; }
       
    }
}