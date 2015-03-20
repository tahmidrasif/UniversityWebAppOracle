using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityWebApp.Areas.Student.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Student/Home/
        //[Authorize(Roles = "Student")]
        public ActionResult Index()
        {
            return View();
        }
	}
}