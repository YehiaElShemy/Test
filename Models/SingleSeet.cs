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
     public  class SingleSeet
    {
        [ForeignKey("Seet")]
        public int ID { get; set; }
        [Required(ErrorMessage ="please enter status")]
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

        public Seet Seet { get; set; }
    }
}
