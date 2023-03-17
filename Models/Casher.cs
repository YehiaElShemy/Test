using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace projectMVC.Models
{
    public class Casher
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "please eenter yor user name ")]

        [StringLength(100)]
        [Index(IsUnique =true)]
        public string CasherUserName { get; set; }

        [Required(ErrorMessage = "please eenter yor user password ")]
        [StringLength(100)]

        public string CasherPassword { get; set; }

        [ForeignKey("Admin")]
        public int Admin_Id { get; set; }
        public Admin Admin { get; set; }

        [ForeignKey("Employe")]
        public int Emp_Id { get; set; }
        public Employe Employe { get; set; }
    }
}