using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityWebApp.Areas.Admin.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public int UserId { get; set; }         
        public string RegistrationNo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Departments { get; set; }
        public virtual List<Course> Courses { get; set; }
    }
}