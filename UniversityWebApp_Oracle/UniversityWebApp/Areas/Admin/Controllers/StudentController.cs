using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using UniversityWebApp.Repository;
using UniversityWebApp.Areas.Admin.Models;
using UniversityWebApp.Repository.Gateway;

namespace UniversityWebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private StudentGateway aStudentGateway=new StudentGateway();
        private UserGateway aUserGateway=new UserGateway();
        private DepartmentGateway aDepartmentGateway=new DepartmentGateway();
        // GET: /Admin/Student/


        public ActionResult Index()
        {
            return View();
        }


        public JsonResult List(int jtStartIndex, int jtPageSize)
        {

            try
            {
                var students = aStudentGateway.GetAll();
                var studentCount = students.Count;
                var studentlist = students.Distinct().Skip(jtStartIndex).Take(jtPageSize).ToList();
                return Json(new { Result = "OK", Records = studentlist, TotalRecordCount = studentCount });
                //Higlighted text are for pagination

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult Create(Models.Student student)
        {
            try
            {
                //var students = aUserGateway.GetAll().Where(x => x.UserType == "Student");
                var user = aUserGateway.GetById(student.UserId);
                student.Email = user.Email;
                aStudentGateway.Insert(student);
                return Json(new { Result = "OK", Record = student });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }



        [HttpPost]
        public JsonResult Delete(int StudentId)
        {
            try
            {
                aStudentGateway.Delete(StudentId);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Edit(Models.Student student)
        {
            try
            {
                aStudentGateway.Edit(student);
                return Json(new { Result = "OK", Record = student });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public JsonResult GetUserName()
        {
            try
            {
                var users = aUserGateway.GetAll().Where(x => x.UserType == "Student").Select(c => new { DisplayText = c.UserName, Value = c.UserId }).OrderBy(s => s.DisplayText);
                return Json(new { Result = "OK", Options = users });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        public JsonResult GetDepartment()
        {
            try
            {
                var aDepartmentGateway = new DepartmentGateway();
                var continentals =
                    aDepartmentGateway.GetAll()
                        .Select(c => new { DisplayText = c.Code, Value = c.DepartmentId })
                        .OrderBy(s => s.DisplayText);
                return Json(new { Result = "OK", Options = continentals });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        public JsonResult GetStudentByDeptId(int id)
        {
            List<Models.Student> students = aStudentGateway.GetAll().Where(x => x.DepartmentId == id).ToList();
            //List<string> students= new List<string>(){"Rasif","Tahmid","Islam"};
            return Json(new SelectList(students, "StudentId", "Name"), JsonRequestBehavior.AllowGet);
        }
    }
}
