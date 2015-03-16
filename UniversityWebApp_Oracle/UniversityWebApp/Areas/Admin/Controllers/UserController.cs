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
    public class UserController : Controller
    {
        private MyDbContext db = new MyDbContext();
        UserGateway aUserGateway=new UserGateway();
        private List<string> userTypes = new List<string>()
            {
                "Admin","DeptHead","Student","Teacher"
            };

        // GET: /Admin/User/
        public ActionResult Index()
        {
            var users = aUserGateway.GetAll();
            Session["UserCount"] = users.Count;
            return View(users);
        }

        // GET: /Admin/User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                User user = aUserGateway.GetById(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /Admin/User/Create
        public ActionResult Create()
        {
            
            ViewBag.UserType = new SelectList(userTypes);
            return View();
        }

        // POST: /Admin/User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UserId,UserName,Password,Email,UserType")] User user)
        {
            if (ModelState.IsValid)
            {
                aUserGateway.Insert(user);
                return RedirectToAction("Index");
            }
            ViewBag.UserType = new SelectList(userTypes);
            return View(user);
        }

        // GET: /Admin/User/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.UserType = new SelectList(userTypes);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = aUserGateway.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Admin/User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UserId,UserName,Password,Email,UserType")] User user)
        {
            if (ModelState.IsValid)
            {
                aUserGateway.Edit(user);
                return RedirectToAction("Index");
            }
            ViewBag.UserType = new SelectList(userTypes);
            return View(user);
        }

        // GET: /Admin/User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = aUserGateway.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            aUserGateway.Delete(id);
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
