using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UniversityWebApp.Areas.Admin.Models;
using UniversityWebApp.Areas.Teacher.Models;
using UniversityWebApp.Repository;
using UniversityWebApp.Repository.Gateway;

namespace UniversityWebApp.Areas.Teacher.Controllers
{
    public class TeacherCourseResultController : Controller
    {
        private MyDbContext db = new MyDbContext();
        StudentGateway aStudentGateway=new StudentGateway();
        CourseGateway aCourseGateway=new CourseGateway();
        CourseTeacherEnrollGateway aCourseTeacherEnrollGateway=new CourseTeacherEnrollGateway();
        CourseStudentEnrollGateway aCourseStudentEnrollGateway=new CourseStudentEnrollGateway();
        // GET: /Teacher/TeacherCourseResult/
        public ActionResult Index(int id)
        {
           return View();
        }

        // GET: /Teacher/TeacherCourseResult/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherCourseResultViewModel teachercourseresultviewmodel = db.TeacherCourseResultViewModels.Find(id);
            if (teachercourseresultviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(teachercourseresultviewmodel);
        }

        // GET: /Teacher/TeacherCourseResult/Create
        public ActionResult Create(int id)
        {
            SetStudentAndCourse(id);
            return View();
        }

        private void SetStudentAndCourse(int id)
        {
            var enrolledCourseId =
                aCourseTeacherEnrollGateway.GetAll().Where(x => x.TeacherId == id).Select(x => x.CourseId).ToList();
            var courseList = new List<Course>();
            var studentList = new List<Admin.Models.Student>();
            foreach (var i in enrolledCourseId)
            {
                var aCourse = aCourseGateway.GetAll().FirstOrDefault(x => x.CourseId == i);
                var aStudentId =
                    aCourseStudentEnrollGateway.GetAll()
                        .Where(x => x.CourseId == i)
                        .Select(x => x.StudentId)
                        .FirstOrDefault();
                var student = aStudentGateway.GetAll().FirstOrDefault(x => x.StudentId == aStudentId);
                courseList.Add(aCourse);
                studentList.Add(student);
            }
            ViewBag.CourseId = new SelectList(courseList, "CourseId", "Name");
            ViewBag.StudentId = new SelectList(studentList, "StudentId", "Name");
        }

        // POST: /Teacher/TeacherCourseResult/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeacherCourseResultViewModel teachercourseresultviewmodel)
        {
            if (teachercourseresultviewmodel.Score > 0 && teachercourseresultviewmodel.Score <= 100)
            {
                aCourseStudentEnrollGateway.EditByTeacher(teachercourseresultviewmodel);
                return RedirectToAction("Index","Home",new {area="Teacher"});
            }
 
            return RedirectToAction("Create");
        }

        // GET: /Teacher/TeacherCourseResult/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherCourseResultViewModel teachercourseresultviewmodel = db.TeacherCourseResultViewModels.Find(id);
            if (teachercourseresultviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(teachercourseresultviewmodel);
        }

        // POST: /Teacher/TeacherCourseResult/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TeacherCourseResultViewModelId,StudentId,CourseId,Score")] TeacherCourseResultViewModel teachercourseresultviewmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teachercourseresultviewmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teachercourseresultviewmodel);
        }

        // GET: /Teacher/TeacherCourseResult/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherCourseResultViewModel teachercourseresultviewmodel = db.TeacherCourseResultViewModels.Find(id);
            if (teachercourseresultviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(teachercourseresultviewmodel);
        }

        // POST: /Teacher/TeacherCourseResult/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeacherCourseResultViewModel teachercourseresultviewmodel = db.TeacherCourseResultViewModels.Find(id);
            db.TeacherCourseResultViewModels.Remove(teachercourseresultviewmodel);
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
