﻿using System;
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
    [Authorize(Roles = "Teacher")]
    public class TeacherCourseResultController : Controller
    {
        private MyDbContext db = new MyDbContext();
        StudentGateway aStudentGateway=new StudentGateway();
        CourseGateway aCourseGateway=new CourseGateway();
        CourseTeacherEnrollGateway aCourseTeacherEnrollGateway=new CourseTeacherEnrollGateway();
        CourseStudentEnrollGateway aCourseStudentEnrollGateway=new CourseStudentEnrollGateway();
        private List<double> totalGpa = new List<double>();
        private List<double> totalCredit = new List<double>();
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
                var aCourse = aCourseGateway.GetById(i);
                var studentsId =
                    aCourseStudentEnrollGateway.GetAll()
                        .Where(x => x.CourseId == i)
                        .Select(x => x.StudentId).ToList();

                studentList.AddRange(studentsId.Select(aStudentId => aStudentGateway.GetById(aStudentId)));
                courseList.Add(aCourse);

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
                var enrolls = aCourseStudentEnrollGateway.GetAll().Where(x => x.StudentId == teachercourseresultviewmodel.StudentId);
                foreach (var aEnroll in enrolls)
                {
                    var credit = aCourseGateway.GetById(aEnroll.CourseId).Credit;
                    double number = aEnroll.Score;
                    double grade = GetGpa(number);
                    totalCredit.Add(credit);
                    var gpa = credit * grade;
                    totalGpa.Add(gpa);

                }
                var cgpa = totalGpa.Sum() / totalCredit.Sum();
                aStudentGateway.EditByTeacher(cgpa,teachercourseresultviewmodel.StudentId);

                return RedirectToAction("Index","Home",new {area="Teacher"});

            }
 
            return RedirectToAction("Create");
        }

        private double GetGpa(double number)
        {
            if (number >= 80)
                return 4;
            if (number >= 70 && number < 80)
                return 3.5;
            if (number >= 60 && number < 70)
                return 3.0;
            if (number >= 50 && number < 60)
                return 2.5;
            if (number >= 40 && number < 50)
                return 2.0;
            if (number >= 0 && number < 40)
                return 0;
            return 0;
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
