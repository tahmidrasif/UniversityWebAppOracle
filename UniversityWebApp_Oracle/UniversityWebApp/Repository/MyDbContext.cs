﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using UniversityWebApp.Areas.Admin.Models;

namespace UniversityWebApp.Repository
{
    public class MyDbContext:DbContext
    {
        public MyDbContext()
            : base("UniversityWebAppOracle")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder); //
        }

        public DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<UniversityWebApp.Areas.Admin.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<UniversityWebApp.Areas.Admin.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<UniversityWebApp.Areas.Admin.Models.Student> Students { get; set; }

        public System.Data.Entity.DbSet<UniversityWebApp.Areas.Admin.Models.Teacher> Teachers { get; set; }

        public System.Data.Entity.DbSet<UniversityWebApp.Areas.Teacher.Models.TeacherViewModel> TeacherViewModels { get; set; }

        public System.Data.Entity.DbSet<UniversityWebApp.Areas.Admin.Models.Room> Rooms { get; set; }

        public System.Data.Entity.DbSet<UniversityWebApp.Areas.Admin.Models.CourseTeacherEnroll> CourseTeacherEnrolls { get; set; }

        public System.Data.Entity.DbSet<UniversityWebApp.Areas.Admin.Models.CourseStudentEnroll> CourseStudentEnrolls { get; set; }

        public System.Data.Entity.DbSet<UniversityWebApp.Areas.Teacher.Models.TeacherCourseResultViewModel> TeacherCourseResultViewModels { get; set; }

        public System.Data.Entity.DbSet<UniversityWebApp.Areas.Admin.Models.CourseRoomEnroll> CourseRoomEnrolls { get; set; }

        public System.Data.Entity.DbSet<UniversityWebApp.Areas.Admin.ViewModel.Result> Results { get; set; }

        public System.Data.Entity.DbSet<UniversityWebApp.Areas.Student.Models.NumberViewModel> NumberViewModels { get; set; }
    }
}