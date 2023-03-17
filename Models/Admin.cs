using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using projectMVC.enums;
namespace projectMVC.Models
{
   public class Admin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "please eenter yor user name ")]
        [Index(IsUnique =true)]
        [StringLength(100)]
        public string AdminUserName { get; set; }


        [Required(ErrorMessage = "please eenter yor user password ")]
        [StringLength(100)]

        public string  AdminPassword { get; set; }

        [Required(ErrorMessage ="please select your bussines type")]
        public BussinessType BussinesType { get; set; }

        [Required(ErrorMessage = "please eenter your code ")]
        [Index(IsUnique = true)]
        [StringLength(100)]

        public string Code { get; set; }
      
        public ICollection<Employe> Employies { get; set; }
        public ICollection<Drink> Drinkies { get; set; }
        public ICollection<Food> foodies{ get; set; }
        public ICollection<Seet> Seets { get; set; }







    }
}
