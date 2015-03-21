using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityWebApp.Areas.Admin.Models;
using UniversityWebApp.Repository;
using UniversityWebApp.Repository.Gateway;

namespace UniversityWebApp.Areas.Admin.Controllers
{
    public class TeacherController : Controller
    {
        UserGateway aUserGateway=new UserGateway();
        TeacherGateway aTeacherGateway=new TeacherGateway();
        private List<string> designation = new List<string>()
            {
                "Head","Associate Professor","Assistant Professor","Lecturer"
            };
        // GET: /Admin/Teacher/
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult List(int jtStartIndex, int jtPageSize)
        {

            try
            {
                var teachers = aTeacherGateway.GetAll();
                    var teachercount = teachers.Count;
                    var teacherlist = teachers.Distinct().Skip(jtStartIndex).Take(jtPageSize).ToList();
                    return Json(new { Result = "OK", Records = teacherlist, TotalRecordCount = teachercount });
                    //Higlighted text are for pagination

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult Create(Teacher teacher)
        {
            try
            {
                teacher.RemainingCredit = teacher.CreditToBeTaken;
                aTeacherGateway.Insert(teacher);
                return Json(new { Result = "OK", Record = teacher });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }



        [HttpPost]
        public JsonResult Delete(int TeacherId)
        {
            try
            {
                aTeacherGateway.Delete(TeacherId);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Edit(Teacher teacher)
        {
            try
            {
                teacher.RemainingCredit = teacher.CreditToBeTaken;
                aTeacherGateway.Edit(teacher);
                return Json(new { Result = "OK", Record = teacher });
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
                        .Select(c => new {DisplayText = c.Code, Value = c.DepartmentId})
                        .OrderBy(s => s.DisplayText);
                return Json(new {Result = "OK", Options = continentals});
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", Message = ex.Message});
            }
        }
        public JsonResult GetDesignation()
        {
            try
            {

                return Json(new { Result = "OK", Options = designation });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public JsonResult GetEmail()
        {
            try
            {
                var names = aUserGateway.GetAll().Where(x => x.UserType == "Teacher").Select(x => x.Email);
                return Json(new { Result = "OK", Options = names });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public JsonResult GetUserId()
        {
            try
            {
                var names = aUserGateway.GetAll().Where(x => x.UserType == "Teacher").Select(c => new { DisplayText = c.UserName, Value = c.UserId }).OrderBy(s => s.DisplayText);
                return Json(new { Result = "OK", Options = names });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
