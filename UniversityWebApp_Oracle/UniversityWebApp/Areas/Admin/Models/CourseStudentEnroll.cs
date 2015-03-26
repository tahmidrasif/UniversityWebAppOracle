using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace UniversityWebApp.Areas.Admin.Models
{
    public class CourseStudentEnroll
    {
        public int CourseStudentEnrollId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string Semester { get; set; }
        public DateTime DateTime { get; set; }
        public double Score { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}