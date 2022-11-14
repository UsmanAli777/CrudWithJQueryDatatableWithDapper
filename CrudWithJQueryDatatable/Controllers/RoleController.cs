using CrudWithJQueryDatatable.Models;
using CrudWithJQueryDatatable.services;
using System.Web.Mvc;

namespace CrudWithJQueryDatatable.Controllers
{
    //public class RoleController : Controller
    //{
    //    private readonly IRoleServices _role;

    //    public RoleController(IRoleServices role)
    //    {
    //        _role = role;
    //    }

    //    public ActionResult Index()
    //    {
    //        return View(_role.GetAllRole());
    //    }

    //    [HttpGet]
    //    public ActionResult CreateRole()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    public ActionResult CreateRole(Role role)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            _role.AddRole(role);
    //        }
    //        else
    //        {
    //            ModelState.AddModelError("", "Wrong Detail Add.");
    //        }
    //        return View("Index");
    //    }

    //    [HttpGet]
    //    public ActionResult EditRole(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        var emp = _role.GetRoleById(id.GetValueOrDefault());
    //        if (emp == null)

    //            return HttpNotFound();

    //        return View(emp);
    //    }

    //    [HttpPost]
    //    public ActionResult EditRole(int id, [Bind("RId,RName")] Role role)
    //    {
    //        long result = 0;
    //        int Status;
    //        string Value;

    //        if (ModelState.IsValid)
    //        {
    //            result = _role.UpdateRole(role);
    //            if (result > 0)
    //            {
    //                Status = 200;
    //                Value = Url.Content("~/Design/View/");
    //            }
    //            else
    //            {
    //                Status = 500;
    //                Value = "There is some error at server side";
    //            }
    //        }
    //        else
    //        {
    //            Status = 500;
    //            Value = "There is some error at client side";
    //        }
    //        return Json(new { status = Status, value = Value });
    //    }
    //}
}