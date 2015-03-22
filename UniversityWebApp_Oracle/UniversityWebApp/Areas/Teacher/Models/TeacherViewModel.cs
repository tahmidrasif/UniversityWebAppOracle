using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UniversityWebApp.Areas.Admin.Models;

namespace UniversityWebApp.Areas.Teacher.Models
{
    public class TeacherViewModel
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Designation { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Credit is Required")]
        public double CreditToBeTaken { get; set; }
        public double RemainingCredit { get; set; }
        public string DepartmentName { get; set; }
        public string ImagePath { get; set; }
        public virtual Department Department { get; set; }
        public virtual List<Course> Courses { get; set; }
        public virtual User User { get; set; }
    }
}