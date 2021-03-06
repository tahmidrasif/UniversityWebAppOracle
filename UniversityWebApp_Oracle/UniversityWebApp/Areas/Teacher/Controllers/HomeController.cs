﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityWebApp.Areas.Admin.Models;
using UniversityWebApp.Areas.Teacher.Models;
using UniversityWebApp.Repository.Gateway;

namespace UniversityWebApp.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class HomeController : Controller
    {
        //
        // GET: /Teacher/Home/
        private int flag = 0;
        TeacherViewModel aModel=new TeacherViewModel();
        TeacherGateway aTeacherGateway=new TeacherGateway();
        UserGateway aUserGateway=new UserGateway();
        DepartmentGateway aDepartmentGateway=new DepartmentGateway();
        
        public ActionResult Index()
        {
            SetViewModel();

            if (flag == 1)
            {
                return View(aModel); 
            }
            return RedirectToAction("LogOut", "Home", new { area = "" });
        }

        private void SetViewModel()
        {
            var user = (User)Session["User"];
            var aUser = aUserGateway.GetById(user.UserId);

            if (TeacherIsCreated(aUser))
            {
                aModel.UserName = aUser.UserName;
                aModel.Email = aUser.Email;
                aModel.Password = aUser.Password;
                var teachers = aTeacherGateway.GetAll();
                var teacher = teachers.FirstOrDefault(x => x.UserId == aUser.UserId);
                var department = aDepartmentGateway.GetById(teacher.DepartmentId);
                var deptName = department.Name;
                if (teacher != null)
                {
                    aModel.CreditToBeTaken = teacher.CreditToBeTaken;
                    aModel.RemainingCredit = teacher.RemainingCredit;
                    aModel.Name = teacher.Name;
                    aModel.ImagePath = teacher.ImagePath;
                    aModel.DepartmentName = deptName;
                    aModel.Designation = teacher.Designation;
                    aModel.TeacherId = teacher.TeacherId;
                    aModel.UserId = user.UserId;
                }
                flag = 1;
            }

           
        }

        private bool TeacherIsCreated(User aUser)
        {
            var teachr = aTeacherGateway.GetAll().FirstOrDefault(x => x.UserId == aUser.UserId);
            if (teachr != null)
                return true;
            return false;
        }


        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                SetViewModel();
            }
          return View(aModel);
        }

        [HttpPost]
        public ActionResult Edit(TeacherViewModel aViewModel, string PasswordOne, string PasswordTwo, HttpPostedFileBase file)
        {
            SetViewModel();
            aViewModel.TeacherId = aModel.TeacherId;
            aViewModel.UserId = aModel.UserId;
            aViewModel.ImagePath = aModel.ImagePath;
            if (PasswordOne != null && PasswordTwo != null)
            {
                if (MatchPassWord(PasswordOne, PasswordTwo))
                {
                    aViewModel.Password = PasswordOne;
                }
            }
            if (file != null)
            {
                string pic = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Image"), pic);
                file.SaveAs(path);
                path = Path.Combine("/Image", pic);
                aViewModel.ImagePath = path;

            }
            aTeacherGateway.EditByTeacher(aViewModel);
            aUserGateway.EditByTeacher(aViewModel);
            return RedirectToAction("Index");
        }

        private bool MatchPassWord(string password, string confirmPassword)
        {
            if (password == confirmPassword)
                return true;
            return false;
        }
        public ActionResult ViewCourse(int id)
        {
            return View();
        }
    }
}