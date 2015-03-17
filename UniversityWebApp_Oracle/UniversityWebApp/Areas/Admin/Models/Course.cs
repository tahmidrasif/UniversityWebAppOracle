using System.Collections.Generic;

namespace UniversityWebApp.Areas.Admin.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Credit { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Departments { get; set; }
        public List<Student> Students { get; set; }
    }
}