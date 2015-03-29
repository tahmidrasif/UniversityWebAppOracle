using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityWebApp.Areas.Admin.Models
{
    public class CourseTeacherEnroll
    {
        public int CourseTeacherEnrollId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public string Semester { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Course Course { get; set; }
    }
}