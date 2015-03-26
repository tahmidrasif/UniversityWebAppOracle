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
    public class CourseStudentEnrollController : Controller
    {
        private MyDbContext db = new MyDbContext();
        CourseStudentEnrollGateway aCourseStudentEnrollGateway = new CourseStudentEnrollGateway();
        TeacherGateway aTeacherGateway = new TeacherGateway();
        StudentGateway aStudentGateway=new StudentGateway();
        CourseGateway aCourseGateway = new CourseGateway();
        DepartmentGateway aDepartmentGateway = new DepartmentGateway();
        SemesterGateway aSemesterGateway = new SemesterGateway();

        // GET: /Admin/CourseStudentEnroll/
        public ActionResult Index()
        {
            //var coursestudentenrolls = db.CourseStudentEnrolls.Include(c => c.Course).Include(c => c.Student);
            var coursestudentenrollList = aCourseStudentEnrollGateway.GetAll().ToList();
            return View(coursestudentenrollList);
        }

        // GET: /Admin/CourseStudentEnroll/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseStudentEnroll coursestudentenroll = db.CourseStudentEnrolls.Find(id);
            if (coursestudentenroll == null)
            {
                return HttpNotFound();
            }
            return View(coursestudentenroll);
        }

        // GET: /Admin/CourseStudentEnroll/Create
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(aStudentGateway.GetAll(), "StudentId", "Name");
            ViewBag.CourseId = new SelectList(aCourseGateway.GetAll(), "CourseId", "Name");
            ViewBag.Departments = new SelectList(aDepartmentGateway.GetAll(), "DepartmentId", "Name");
            ViewBag.Semester = new SelectList(aSemesterGateway.GetAll());
            ViewBag.ErrorMsg = "";
            return View();
        }

        // POST: /Admin/CourseStudentEnroll/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "CourseStudentEnrollId,StudentId,CourseId,Semester,DateTime")] 
        public ActionResult Create(CourseStudentEnroll coursestudentenroll)
        {
            var alreadyEnroll =
                aCourseStudentEnrollGateway.GetAll()
                    .FirstOrDefault(
                        x => x.StudentId == coursestudentenroll.StudentId && x.CourseId == coursestudentenroll.CourseId);
            if (alreadyEnroll == null)
            {
                if (ModelState.IsValid)
                {
                    aCourseStudentEnrollGateway.Insert(coursestudentenroll);
                    return RedirectToAction("Index");
                }

            }

            ViewBag.StudentId = new SelectList(aStudentGateway.GetAll(), "StudentId", "Name");
            ViewBag.CourseId = new SelectList(aCourseGateway.GetAll(), "CourseId", "Name");
            ViewBag.Departments = new SelectList(aDepartmentGateway.GetAll(), "DepartmentId", "Name");
            ViewBag.Semester = new SelectList(aSemesterGateway.GetAll());
            ViewBag.ErrorMsg = "This Course is alreay enrolled by this student";
            return View(coursestudentenroll);
        }





        // GET: /Admin/CourseStudentEnroll/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseStudentEnroll coursestudentenroll = db.CourseStudentEnrolls.Find(id);
            if (coursestudentenroll == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", coursestudentenroll.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegistrationNo", coursestudentenroll.StudentId);
            return View(coursestudentenroll);
        }

        // POST: /Admin/CourseStudentEnroll/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CourseStudentEnrollId,StudentId,CourseId,Semester,DateTime")] CourseStudentEnroll coursestudentenroll)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coursestudentenroll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", coursestudentenroll.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegistrationNo", coursestudentenroll.StudentId);

            return View(coursestudentenroll);
        }

        // GET: /Admin/CourseStudentEnroll/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseStudentEnroll coursestudentenroll = db.CourseStudentEnrolls.Find(id);
            if (coursestudentenroll == null)
            {
                return HttpNotFound();
            }
            return View(coursestudentenroll);
        }

        // POST: /Admin/CourseStudentEnroll/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseStudentEnroll coursestudentenroll = db.CourseStudentEnrolls.Find(id);
            db.CourseStudentEnrolls.Remove(coursestudentenroll);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
