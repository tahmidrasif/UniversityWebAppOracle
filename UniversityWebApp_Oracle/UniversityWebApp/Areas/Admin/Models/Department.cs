using System.Collections.Generic;

namespace UniversityWebApp.Areas.Admin.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual List<Student> Students { get; set; }
        public virtual List<Teacher> Teachers { get; set; }
        public virtual List<Course> Courses { get; set; }
    }
}