using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityWebApp.Areas.Teacher.Models
{
    public class TeacherCourseResultViewModel
    {
        public int TeacherCourseResultViewModelId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double Score { get; set; }   

    }
}