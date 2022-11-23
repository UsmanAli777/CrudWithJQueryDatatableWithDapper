using CrudWithJQueryDatatable.Models;
using CrudWithJQueryDatatable.Models.DataTable;
using CrudWithJQueryDatatable.services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace CrudWithJQueryDatatable.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _empSer;

        public EmployeeController(IEmployeeServices empSer)
        {
            _empSer = empSer;
        }
        //JqueryDatatableCrudEntities1 db = new JqueryDatatableCrudEntities1();
        // GET: Employee
        [Authorize]
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult GetList()
        {
            //srver side parameters
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
            var result = _empSer.GetAllEmployeeDT(request);
            result.draw = request.Draw;
            result.recordsTotal = result.recordsFiltered;

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Employee());
            else
            {
                return View(_empSer.GetEmployeeById(id));
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit( Employee emp)
        {
            if (emp.EmployeeId == 0)
            {
                if(ModelState.IsValid)
                {
                    _empSer.AddEmployee(emp);
                    return Redirect("~/Employee/Index");
                }
            }
            else
            {
                _empSer.UpdateEmployee(emp);
                return Redirect("~/Employee/Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _empSer.DeleteEmployee(id);
            return Redirect("~/Employee/Index");
        }
    }
}