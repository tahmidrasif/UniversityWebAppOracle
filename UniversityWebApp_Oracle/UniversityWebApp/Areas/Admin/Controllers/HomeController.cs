using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityWebApp.Repository.Gateway;

namespace UniversityWebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        UserGateway aUserGateway = new UserGateway();
        StudentGateway aStudentGateway = new StudentGateway();
        DepartmentGateway aDepartmentGateway = new DepartmentGateway();
        TeacherGateway  aTeacherGateway=new TeacherGateway();
        //
        // GET: /Admin/Home/
        
        public ActionResult Index()
        {
            GetCountValue();
            return View();
        }

        public void GetCountValue()
        {
            Session["UserCount"] = aUserGateway.GetAll().Count;
            Session["StudentCount"] = aStudentGateway.GetAll().Count;
            Session["DepartmentCount"] = aDepartmentGateway.GetAll().Count;
            Session["TeacherCount"] = aTeacherGateway.GetAll().Count;
        }
    }
}