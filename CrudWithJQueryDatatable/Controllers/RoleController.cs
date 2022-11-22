using CrudWithJQueryDatatable.Models;
using CrudWithJQueryDatatable.Models.DataTable;
using CrudWithJQueryDatatable.services;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CrudWithJQueryDatatable.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleServices _Role;

        public RoleController(IRoleServices Role)
        {
            _Role = Role;
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult Index(UserDetail uD)
        {
            var user = _Role.GetUserByRole(uD).ToList();
            return View(user);
            //return View();
        }

        //[HttpPost]
        //public ActionResult Index(UserDetail ud)
        //{
        //    var user = _Role.GetUserByRole(ud).ToList();
        //    return Json(new { data = user }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        public JsonResult ServerSide()
        {
            var request = new DataTableRequest();
            request.Draw = Convert.ToInt32(Request.Form["draw"]);
            request.Start = Convert.ToInt32(Request.Form["start"]);
            request.Length = Convert.ToInt32(Request.Form["length"]);
            request.Search = new DataTableSearch()
            {
                Value = Request.Form["search[value]"]
            };
            request.Order = new DataTableOrder[] {
            new DataTableOrder()
            {
                Dir = Request.Form["order[0][dir]"],
               Column = Convert.ToInt32(Request.Form["order[0][column]"])
            }};
            //var datalist = _Role.GetAllUserDT(request);

            //return Json(data);
            var result = _Role.GetAllUserDT(request);
            result.draw = request.Draw;
            result.recordsTotal = result.recordsFiltered;
            
            return Json(result, JsonRequestBehavior.AllowGet);

            //return Json(new { data = datalist, draw = Request["draw"] }, JsonRequestBehavior.AllowGet);
            //return Json();
            // return Json(_Role.GetAllUserDT(request) );
            //return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateNewRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewRole(Role role)
        {
            if (ModelState.IsValid)
            {
                _Role.AddRole(role);
                return Redirect("~/Role/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Not Added! !");
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "CEO")]
        [HttpGet]
        public ActionResult EditUser(int id)
        {
            var User = _Role.GetUserById(id).email;
            ViewBag.Email = User.ToString();

            Session["UserId"] = id;
            var role = _Role.GetAllRole(id);
            return View(role);
        }

        [HttpPost]
        public ActionResult EditUser(List<UserRoleEdit> UserRoleEdit)
        {
            int UId = (int)Session["UserId"];
            var roleChk = UserRoleEdit.Where(x => x.Checked == true);

            foreach (var item in roleChk)
            {
                if (item.Checked == true)
                {
                    _Role.RemoveUserRole(UId, item.R_Id);
                    _Role.AddUserRole(UId, item.R_Id);
                }
            };

            var roleUchk = UserRoleEdit.Where(x => x.Checked == false);
            foreach (var item in roleUchk)
            {
                _Role.RemoveUserRole(UId, item.R_Id);
            };
            return Redirect("~/Role/Index");
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "CEO")]
        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            _Role.DeleteUser(id);
            return Redirect("~/Role/Index");
        }
    }
}