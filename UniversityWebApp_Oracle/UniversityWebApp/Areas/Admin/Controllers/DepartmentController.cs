using System;
using System.Linq;
using System.Web.Mvc;
using UniversityWebApp.Areas.Admin.Models;
using UniversityWebApp.Repository.Gateway;

namespace UniversityWebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        private DepartmentGateway aDepartmentGateway=new DepartmentGateway();

        // GET: /Admin/Department/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(int jtStartIndex, int jtPageSize)
        {
            try
            {
                var departments = aDepartmentGateway.GetAll();
                var departmentCount = departments.Count;
                var departmentList = departments.Distinct().Skip(jtStartIndex).Take(jtPageSize);
                return Json(new { Result = "OK", Records = departmentList, TotalRecordCount = departmentCount });
                //Higlighted text are for pagination
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult Create(Department department)
        {
            try
            {
                aDepartmentGateway.Insert(department);
                return Json(new { Result = "OK", Record = department });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }



          [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                aDepartmentGateway.Delete(id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Edit(Department department)
        {
            try
            {
                    aDepartmentGateway.Edit(department);
                    return Json(new { Result = "OK", Record = department });
   
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }



    }
}
