using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityWebApp.Areas.Admin.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}