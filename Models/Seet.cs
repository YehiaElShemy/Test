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
   public class Seet
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="please select type")]
        public SeetType SeetType { get; set; }
        [Required(ErrorMessage = "please enter number")]
        public int Number { get; set; }


        [Required(ErrorMessage = "please eenter your code ")]
        [StringLength(100)]

        public string Code { get; set; }

        public bool IsEmpty { get; set; }

        [Column(TypeName = "money")]
        [Required(ErrorMessage = "please enter the cost ")]
        public decimal Cost { get; set; }
        [Required(ErrorMessage = "please enter start time ")]
        [Column(TypeName = "datetime2")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "please enter start time ")]
        [Column(TypeName = "datetime2")]
        public DateTime EndTime { get; set; }


        public Admin Admin { get; set; }
        [ForeignKey("Admin")]
        public int Admin_Id { get; set; }

    }
}
