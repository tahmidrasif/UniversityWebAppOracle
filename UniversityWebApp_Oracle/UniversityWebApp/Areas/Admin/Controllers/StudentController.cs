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
    public class StudentController : Controller
    {
        private StudentGateway aStudentGateway=new StudentGateway();
        private UserGateway aUserGateway=new UserGateway();
        private DepartmentGateway aDepartmentGateway=new DepartmentGateway();
        // GET: /Admin/Student/
        public ActionResult Index()
        {
            return View(aStudentGateway.GetAll());
        }

        // GET: /Admin/Student/Details/5


        // GET: /Admin/Student/Create
        public ActionResult Create()
        {
            var students = aUserGateway.GetAll().Where(x => x.UserType == "Student");
            ViewBag.UserId = new SelectList(students, "UserId", "UserName");
            ViewBag.DepartmentId = new SelectList(aDepartmentGateway.GetAll(), "DepartmentId", "Name");
            return View();
        }

        // POST: /Admin/Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Student student)
        {
            var students = aUserGateway.GetAll().Where(x => x.UserType == "Student");
            if (ModelState.IsValid)
            {
                aStudentGateway.Insert(student);
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(students, "UserId", "UserName", student.User.UserName);
            ViewBag.DepartmentId = new SelectList(aDepartmentGateway.GetAll(), "DepartmentId", "Name",student.Departments.Name); 
            return View(student);
        }

        // GET: /Admin/Student/Edit/5
        public ActionResult Edit(int? id)
        {
            var students = aUserGateway.GetAll().Where(x => x.UserType == "Student");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniversityWebApp.Areas.Admin.Models.Student student = aStudentGateway.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            ViewBag.UserId = new SelectList(students, "UserId", "UserName");
            ViewBag.DepartmentId = new SelectList(aDepartmentGateway.GetAll(), "DepartmentId", "Name"); 
            return View(student);
        }

        // POST: /Admin/Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UniversityWebApp.Areas.Admin.Models.Student student)
        {
            var students = aUserGateway.GetAll().Where(x => x.UserType == "Student");
            if (ModelState.IsValid)
            {
                aStudentGateway.Edit(student);
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(students, "UserId", "UserName");
            ViewBag.DepartmentId = new SelectList(aDepartmentGateway.GetAll(), "DepartmentId", "Name"); 
            return View(student);
        }

        // GET: /Admin/Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniversityWebApp.Areas.Admin.Models.Student student = aStudentGateway.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: /Admin/Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            aStudentGateway.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
