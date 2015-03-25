using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UniversityWebApp.Areas.Admin.Models;
using UniversityWebApp.Repository.Gateway;

namespace UniversityWebApp.Areas.Student.Controllers
{
    public class HomeController : Controller
    {
        StudentGateway aStudentGateway=new StudentGateway();
        DepartmentGateway aDepartmentGateway=new DepartmentGateway();
        UserGateway aUserGateway=new UserGateway();
        //
        // GET: /Student/Home/
        [Authorize(Roles = "Student")]
        public ActionResult Index()
        {
            var user = (User)Session["User"];
            var aUser = aUserGateway.GetById(user.UserId);
            ViewBag.userName = aUser.UserName;
            var students = aStudentGateway.GetAll();
            var student = students.FirstOrDefault(x => x.UserId == aUser.UserId);
            if (student!=null)
            {
                ViewBag.departmentName = aDepartmentGateway.GetById(student.DepartmentId).Name;
                
                return View(student);
            }
            return RedirectToAction("LogOut","Home",new {area=""});
        }
        public ActionResult Edit(int? id)
        {
            var student = aStudentGateway.GetById(id);
            return View(student);
        }
        [HttpPost]
        public ActionResult Edit(Admin.Models.Student student, string Password,string ConfirmPassword,HttpPostedFileBase file)
        {
            var astudent = aStudentGateway.GetById(student.StudentId);
            if (file != null)
            {
                string pic = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Image"), pic);
                file.SaveAs(path);
                path = Path.Combine("/Image", pic);
                student.ImagePath = path;
                
            }

            aStudentGateway.Edit(student);
            if (Password != "" && ConfirmPassword!="")
            {
                if (Password == ConfirmPassword)
                {
                    User aUser=new User();
                    aUser.UserId = astudent.UserId;
                    aUser.Password = Password;
                    aUserGateway.EditByStudent(aUser);
                }
            }
            if (student.Email!="")
            {
                    Regex regex=new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                    Match match = regex.Match(student.Email);
                    if (match.Success)
                    {
                        User aUser = new User();
                        aUser.UserId = astudent.UserId;
                        aUser.Email = student.Email;
                        aUserGateway.EditByStudent(aUser);
                    }   

            }
            return RedirectToAction("Index");
        }
 

	}
}