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
    //[Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        CourseGateway aCourseGateway=new CourseGateway();
        DepartmentGateway aDepartmentGateway=new DepartmentGateway();

        // GET: /Admin/Course/
        public ActionResult Index()
        {
            ViewBag.DepartmentId = new SelectList(aDepartmentGateway.GetAll(), "DepartmentId", "Name");
            return View();
        }


        public JsonResult List(string name, int jtStartIndex, int jtPageSize, int departmentId = 0)
        {
            
            try
            {
                var courses = aCourseGateway.GetAll();
                if (name == "" && departmentId==0)
                {
                    var courseCount = courses.Count;
                    var courseList = courses.Distinct().Skip(jtStartIndex).Take(jtPageSize);
                    return Json(new { Result = "OK", Records = courseList, TotalRecordCount = courseCount });
                    //Higlighted text are for pagination
                }
                else
                {
                    var filterdcourses = courses.Where(x => x.Name == name || x.DepartmentId == departmentId).Distinct().Skip(jtStartIndex).Take(jtPageSize).ToList();
                    var filterdcoursescount = filterdcourses.Count;
                    return Json(new { Result = "OK", Records = filterdcourses, TotalRecordCount = filterdcoursescount });
                }
                
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
