using CrudWithJQueryDatatable.Models;
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
        //JqueryDatatableCrudEntities1 db = new JqueryDatatableCrudEntities1();
        // GET: Employee
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult GetData()
        //{
        //    using(DBModel db = new DBModel())
        //    {
        //        List<Employee> empList = db.Employees.ToList<Employee>();
        //        return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpPost]
        public ActionResult GetList()
        {
            //srver side parameters
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<Employee> empList = new List<Employee>();
            using (EmployeeDb db = new EmployeeDb())
            {
                empList = db.Employees.ToList<Employee>();
                int totalrows = empList.Count;
                if (!string.IsNullOrEmpty(searchValue))//filter
                {
                    empList = empList.
                        Where(x => x.Name.ToLower().Contains(searchValue.ToLower()) || x.Position.ToLower().Contains(searchValue.ToLower()) || x.Office.ToLower().Contains(searchValue.ToLower()) || x.Age.ToString().Contains(searchValue.ToLower()) || x.Salary.ToString().Contains(searchValue.ToLower())).ToList<Employee>();
                }
                int totalrowsafterfiltering = empList.Count;
                //sorting
                empList = empList.OrderBy(sortColumnName + " " + sortDirection).ToList<Employee>();

                //paging
                empList = empList.Skip(start).Take(length).ToList<Employee>();

                return Json(new { data = empList, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Employee());
            else
            {
                using (EmployeeDb db = new EmployeeDb())
                {
                    return View(db.Employees.Where(x => x.EmployeeId == id).FirstOrDefault<Employee>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Employee emp)
        {
            using (EmployeeDb db = new EmployeeDb())
            {
                if (emp.EmployeeId == 0)
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Employee");
                    //return Json(new { success = true, message = "Save Successfully" }, JsonRequestBehavior.AllowGet);
                    //return ('<script>  </script>');
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Employee");
                    //return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (EmployeeDb db = new EmployeeDb())
            {
                Employee emp = db.Employees.Where(x => x.EmployeeId == id).FirstOrDefault<Employee>();
                db.Employees.Remove(emp);
                db.SaveChanges();
                return RedirectToAction("Index", "Employee");
                //return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
                // return RedirectToAction("Index", "Employee");
            }
        }
    }
}