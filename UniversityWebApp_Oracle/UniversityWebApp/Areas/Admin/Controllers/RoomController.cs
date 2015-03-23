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
    public class RoomController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: /Admin/Room/
        // GET: /Admin/Department/
        RoomGateway aRoomGateway=new RoomGateway();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(int jtStartIndex, int jtPageSize)
        {
            try
            {
                var rooms = aRoomGateway.GetAll();


                    var roomCount = rooms.Count;
                    var roomList = rooms.Distinct().Skip(jtStartIndex).Take(jtPageSize);
                    return Json(new { Result = "OK", Records = roomList, TotalRecordCount = roomCount });
                    //Higlighted text are for pagination
           



            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult Create(Room room)
        {
            try
            {
                aRoomGateway.Insert(room);
                return Json(new { Result = "OK", Record = room });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }



        [HttpPost]
        public JsonResult Delete(int RoomId)
        {
            try
            {
                aRoomGateway.Delete(RoomId);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Edit(Room room)
        {
            try
            {
                aRoomGateway.Edit(room);
                return Json(new { Result = "OK", Record = room });

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public JsonResult GetDepartmentName()
        {
            try
            {
                var aDepartmentGateway = new DepartmentGateway();
                var departments =
                    aDepartmentGateway.GetAll()
                        .Select(c => new { DisplayText = c.Code, Value = c.DepartmentId })
                        .OrderBy(s => s.DisplayText);
                return Json(new { Result = "OK", Options = departments });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
