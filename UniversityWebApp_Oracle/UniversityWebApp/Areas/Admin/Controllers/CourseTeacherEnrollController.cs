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
    [Authorize(Roles = "Admin")]
    public class CourseTeacherEnrollController : Controller
    {
        private MyDbContext db = new MyDbContext();
        CourseTeacherEnrollGateway aCourseTeacherEnrollGateway=new CourseTeacherEnrollGateway();
        TeacherGateway aTeacherGateway=new TeacherGateway();
        CourseGateway aCourseGateway=new CourseGateway();
        DepartmentGateway aDepartmentGateway=new DepartmentGateway();
        SemesterGateway aSemesterGateway=new SemesterGateway();

        // GET: /Admin/CourseTeacherEnroll/
        public ActionResult Index()
        { 
            return View(aCourseTeacherEnrollGateway.GetAll());
        }

        // GET: /Admin/CourseTeacherEnroll/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTeacherEnroll courseteacherenroll = db.CourseTeacherEnrolls.Find(id);
            if (courseteacherenroll == null)
            {
                return HttpNotFound();
            }
            return View(courseteacherenroll);
        }

        // GET: /Admin/CourseTeacherEnroll/Create
        public ActionResult Create()
        {
            ViewBag.TeacherId = new SelectList(aTeacherGateway.GetAll(), "TeacherId", "Name");
            ViewBag.CourseId = new SelectList(aCourseGateway.GetAll(), "CourseId", "Name");
            ViewBag.Departments = new SelectList(aDepartmentGateway.GetAll(),"DepartmentId","Name");
            ViewBag.Semester = new SelectList(aSemesterGateway.GetAll());
            ViewBag.ErrorMsg = "";
            return View();
        }

        // POST: /Admin/CourseTeacherEnroll/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "CourseTeacherEnrollId,TeacherId,CourseId,Semester,DateTime")]
        public ActionResult Create( CourseTeacherEnroll courseteacherenroll)
        {
            
            var courseCredit = aCourseGateway.GetById(courseteacherenroll.CourseId).Credit;
            var teacher = aTeacherGateway.GetById(courseteacherenroll.TeacherId);
            var remainingCredit = teacher.RemainingCredit;
            if (remainingCredit - courseCredit >= 0)
            {
                aCourseTeacherEnrollGateway.Insert(courseteacherenroll);
                teacher.RemainingCredit = remainingCredit - courseCredit;
                aTeacherGateway.Edit(teacher);
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMsg = "Credit Overflow";

            ViewBag.TeacherId = new SelectList(aTeacherGateway.GetAll(), "TeacherId", "Name");
            ViewBag.CourseId = new SelectList(aCourseGateway.GetAll(), "CourseId", "Name");
            ViewBag.Departments = new SelectList(aDepartmentGateway.GetAll(), "DepartmentId", "Name");
            ViewBag.Semester = new SelectList(aSemesterGateway.GetAll()); 
            return View(courseteacherenroll);
        }

        // GET: /Admin/CourseTeacherEnroll/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTeacherEnroll courseteacherenroll = db.CourseTeacherEnrolls.Find(id);
            if (courseteacherenroll == null)
            {
                return HttpNotFound();
            }
            return View(courseteacherenroll);
        }

        // POST: /Admin/CourseTeacherEnroll/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CourseTeacherEnrollId,TeacherId,CourseId,Semester,DateTime")] CourseTeacherEnroll courseteacherenroll)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseteacherenroll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseteacherenroll);
        }

        // GET: /Admin/CourseTeacherEnroll/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aCourseTeacherEnrollGateway.Delete(id);

            return RedirectToAction("Index");
        }

        //// POST: /Admin/CourseTeacherEnroll/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    CourseTeacherEnroll courseteacherenroll = db.CourseTeacherEnrolls.Find(id);
        //    db.CourseTeacherEnrolls.Remove(courseteacherenroll);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
