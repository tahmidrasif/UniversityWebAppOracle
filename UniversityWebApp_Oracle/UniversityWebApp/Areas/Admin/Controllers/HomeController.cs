using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityWebApp.Repository.Gateway;

namespace UniversityWebApp.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        UserGateway aUserGateway = new UserGateway();
        StudentGateway aStudentGateway = new StudentGateway();
        DepartmentGateway aDepartmentGateway = new DepartmentGateway();

        //
        // GET: /Admin/Home/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            Session["UserCount"] = aUserGateway.GetAll().Count;
            Session["StudentCount"] = aStudentGateway.GetAll().Count;
            Session["DepartmentCount"] = aDepartmentGateway.GetAll().Count;
            return View();
        }

    }
}