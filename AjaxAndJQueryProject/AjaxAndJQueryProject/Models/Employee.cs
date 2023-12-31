﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AjaxAndJQueryProject.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} Should be Given")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} Should be Given")]
        public string State { get; set; }
        [Required(ErrorMessage = "{0}  Should be Given")]
        public string City { get; set; }
        [Required(ErrorMessage = "{0} Should be Given")]
        public double Salary { get; set; } 
    }
}
