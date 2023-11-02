﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; } //Primary key

        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }

        [DisplayName("Date of Birth")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Email")]
        [Required]
        public string Email{ get; set; }

        [Required]
        public double Salary { get; set; }

        [NotMapped]
        public string FullName 
        { 
            get { return FirstName + " " + LastName; }
        }
    }
}
