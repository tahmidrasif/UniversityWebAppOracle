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
    public class CourseRoomController : Controller
    {
        private MyDbContext db = new MyDbContext();
        CourseRoomEnrollGateway aCourseRoomEnrollGateway=new CourseRoomEnrollGateway();
        TeacherGateway aTeacherGateway = new TeacherGateway();
        CourseGateway aCourseGateway = new CourseGateway();
        DepartmentGateway aDepartmentGateway = new DepartmentGateway();
        SemesterGateway aSemesterGateway = new SemesterGateway();
        RoomGateway aRoomGateway=new RoomGateway();

        // GET: /Admin/CourseRoom/
        public ActionResult Index()
        {
            return View(aCourseRoomEnrollGateway.GetAll().ToList());
        }

        // GET: /Admin/CourseRoom/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseRoomEnroll courseroomenroll = db.CourseRoomEnrolls.Find(id);
            if (courseroomenroll == null)
            {
                return HttpNotFound();
            }
            return View(courseroomenroll);
        }

        // GET: /Admin/CourseRoom/Create
        public ActionResult Create()
        {
            ViewBag.TeacherId = new SelectList(aTeacherGateway.GetAll(), "TeacherId", "Name");
            ViewBag.CourseId = new SelectList(aCourseGateway.GetAll(), "CourseId", "Name");
            ViewBag.RoomId = new SelectList(aRoomGateway.GetAll(), "RoomId", "RoomNumber");
            ViewBag.Departments = new SelectList(aDepartmentGateway.GetAll(), "DepartmentId", "Name");
            ViewBag.ErrorMsg = "";
            return View();
        }

        // POST: /Admin/CourseRoom/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CourseRoomEnrollId,CourseId,RoomId,TeacherId,Date,StratingTime,EndTime")] CourseRoomEnroll courseroomenroll)
        {
            var count = 0;
            var courseRoomEnroll = aCourseRoomEnrollGateway.GetByRoomId(courseroomenroll.RoomId, courseroomenroll.Date);
            foreach (var roomEnroll in courseRoomEnroll)
            {
                TimeSpan start = TimeSpan.Parse(roomEnroll.StratingTime.ToString("HH:mm:ss"));
                TimeSpan end = TimeSpan.Parse(roomEnroll.EndTime.ToString("HH:mm:ss"));
                TimeSpan nowStart = TimeSpan.Parse(courseroomenroll.StratingTime.ToString("HH:mm:ss"));
                TimeSpan nowEnd = TimeSpan.Parse(courseroomenroll.EndTime.ToString("HH:mm:ss"));
                var startTime = roomEnroll.StratingTime;
                var endtime = roomEnroll.EndTime;
                //if ((courseroomenroll.StratingTime >= startTime && courseroomenroll.EndTime <= endtime) || (courseroomenroll.StratingTime >= endtime && courseroomenroll.EndTime <= endtime))
                //{
                //    count++;
                //}
                if ((start <= nowStart && nowStart <= end) || (start <= nowEnd && nowEnd <= end) || (start >= nowStart && nowEnd >= end))
                {
                    ViewBag.TeacherId = new SelectList(aTeacherGateway.GetAll(), "TeacherId", "Name");
                    ViewBag.CourseId = new SelectList(aCourseGateway.GetAll(), "CourseId", "Name");
                    ViewBag.RoomId = new SelectList(aRoomGateway.GetAll(), "RoomId", "RoomNumber");
                    ViewBag.Departments = new SelectList(aDepartmentGateway.GetAll(), "DepartmentId", "Name");
                    ViewBag.ErrorMsg = "This time is occupied";
                    return View();
                }
            }
            if (ModelState.IsValid)
            {
                aCourseRoomEnrollGateway.Insert(courseroomenroll);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: /Admin/CourseRoom/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseRoomEnroll courseroomenroll = db.CourseRoomEnrolls.Find(id);
            if (courseroomenroll == null)
            {
                return HttpNotFound();
            }
            return View(courseroomenroll);
        }

        // POST: /Admin/CourseRoom/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CourseRoomEnrollId,CourseId,RoomId,TeacherId,Date,StratingTime,EndTime")] CourseRoomEnroll courseroomenroll)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseroomenroll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseroomenroll);
        }

        // GET: /Admin/CourseRoom/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseRoomEnroll courseroomenroll = db.CourseRoomEnrolls.Find(id);
            if (courseroomenroll == null)
            {
                return HttpNotFound();
            }
            return View(courseroomenroll);
        }

        // POST: /Admin/CourseRoom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseRoomEnroll courseroomenroll = db.CourseRoomEnrolls.Find(id);
            db.CourseRoomEnrolls.Remove(courseroomenroll);
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
