using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityWebApp.Areas.Admin.Models
{
    public class Student
    {
        public int StudentId { get; set; }   
        [Required]
        [DisplayName("Registration Number")]
        public string RegistrationNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [DisplayName("CGPA")]
        public double Cgpa { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        [Required]
        [DisplayName("User ID")]
        public int UserId { get; set; }
        [DisplayName("Department ID")]
        [Required]
        public int DepartmentId { get; set; }
        public  Department Departments { get; set; }
        public  User User { get; set; }
        public virtual List<Course> Courses { get; set; }
    }
}