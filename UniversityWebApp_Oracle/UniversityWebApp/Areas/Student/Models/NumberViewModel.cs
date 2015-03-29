using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityWebApp.Areas.Admin.Models;

namespace UniversityWebApp.Areas.Student.Models
{
    public class NumberViewModel
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public double Credit { get; set; }
        public double Number { get; set; }   
    }
}