using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityWebApp.Areas.Admin.Models
{
    public class CourseRoomEnroll
    {
        public int CourseRoomEnrollId { get; set; }
        public int CourseId { get; set; }
        public int RoomId { get; set; }
        public int TeacherId { get; set; }
        public DateTime Date { get; set; }
        public DateTime StratingTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}