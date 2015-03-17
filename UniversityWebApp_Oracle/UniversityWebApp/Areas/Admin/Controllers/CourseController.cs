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
    public class CourseController : Controller
    {
       // private MyDbContext db = new MyDbContext();
        CourseGateway aCourseGateway=new CourseGateway();

        // GET: /Admin/Course/
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult List(int jtStartIndex, int jtPageSize)
        {
            try
            {
                var courses = aCourseGateway.GetAll();
                var courseCount = courses.Count;
                var courseList = courses.Distinct().Skip(jtStartIndex).Take(jtPageSize);
                return Json(new { Result = "OK", Records = courseList, TotalRecordCount = courseCount });
                //Higlighted text are for pagination
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult Create(Course course)
        {
            try
            {
                aCourseGateway.Insert(course);
                return Json(new { Result = "OK", Record = course });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }



        [HttpPost]
        public JsonResult Delete(int CourseId)
        {
            try
            {
                aCourseGateway.Delete(CourseId);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Edit(Course course)
        {
            try
            {
                aCourseGateway.Edit(course);
                return Json(new { Result = "OK", Record = course });

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
                var aDepartmentGateway=new DepartmentGateway();
                var continentals = aDepartmentGateway.GetAll().Select(c => new { DisplayText = c.Name, Value = c.DepartmentId}).OrderBy(s => s.DisplayText);
                return Json(new { Result = "OK", Options = continentals });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
