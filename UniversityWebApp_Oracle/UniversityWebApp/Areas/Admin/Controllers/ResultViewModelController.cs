using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Rotativa;
using UniversityWebApp.Areas.Admin.ViewModel;
using UniversityWebApp.Repository;
using UniversityWebApp.Repository.Gateway;

namespace UniversityWebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ResultViewModelController : Controller
    {
        private MyDbContext db = new MyDbContext();
        StudentGateway aStudentGateway=new StudentGateway();
        DepartmentGateway aDepartmentGateway=new DepartmentGateway();
        Result result = new Result();

        // GET: /Admin/ResultViewModel/
        public ActionResult Index()
        {
            ViewBag.Departments=new SelectList(aDepartmentGateway.GetAll(),"DepartmentId","Code");
            ViewBag.Students = new SelectList(aStudentGateway.GetAll(), "StudentId", "Name");
          
            return View(new Result());
        }

        public ActionResult FilteredSection(int id)
        {
            ViewBag.Departments = new SelectList(aDepartmentGateway.GetAll(), "DepartmentId", "Code");
            ViewBag.Students = new SelectList(aStudentGateway.GetAll(), "StudentId", "Name");
          
            var student = aStudentGateway.GetById(id);
           
                Result aResult = new Result();
                aResult.Name = student.Name;
                aResult.RegistraitionNumber = student.RegistrationNo;
                aResult.CGPA = student.Cgpa;
                aResult.DepartmentName = aDepartmentGateway.GetById(student.DepartmentId).Name;
                return PartialView("_ResultPartial", aResult);


        }
        public ActionResult IndexNew(Result aResult)
        {
            return new Rotativa.ActionAsPdf("OverallResultPrintVersion",aResult);
        }

        public ActionResult OverallResultPrintVersion(Result aResult)
        {
            //Result aResult=new Result();
            //var hell = "jhhkjk";
            //return View("Result",hell);
            return PartialView("_ResultPartial",aResult);
        }
        
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Result result = db.Results.Find(id);
        //    if (result == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(result);
        //}

        //// GET: /Admin/ResultViewModel/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: /Admin/ResultViewModel/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ResultId,Name,DepartmentName,RegistraitionNumber,CGPA")] Result result)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Results.Add(result);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(result);
        //}

        //// GET: /Admin/ResultViewModel/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Result result = db.Results.Find(id);
        //    if (result == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(result);
        //}

        //// POST: /Admin/ResultViewModel/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ResultId,Name,DepartmentName,RegistraitionNumber,CGPA")] Result result)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(result).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(result);
        //}

        //// GET: /Admin/ResultViewModel/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Result result = db.Results.Find(id);
        //    if (result == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(result);
        //}

        //// POST: /Admin/ResultViewModel/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Result result = db.Results.Find(id);
        //    db.Results.Remove(result);
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
