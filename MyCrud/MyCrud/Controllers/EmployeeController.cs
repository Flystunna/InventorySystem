using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCrud.Models;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data;
using System.Data.EntityClient;

namespace MyCrud.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult GetData() 
        {
            using (DBModel db = new DBModel())
            {
                List<Employee> empList = db.Employees.ToList<Employee>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        
        }
        [HttpGet]

        public ActionResult AddorEdit(int id = 0) 
        {
            if (id == 0)
                return View(new Employee());
            else 
            {
                using (DBModel db = new DBModel()) 
                {
                    return View(db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddorEdit(Employee emp)
        {
            using (DBModel db = new DBModel())
            {
                if (emp.EmployeeID == 0)
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                Employee emp = db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
                db.Employees.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}

/*using (DBModel db = new DBModel())
          {
              try 
              {

              db.Employees.Add(emp);
              db.SaveChanges();
               
              }

              catch (DbEntityValidationException e)
              {
                    
                  foreach (var validationErrors in e.EntityValidationErrors)
                  {
                      foreach (var validationError in validationErrors.ValidationErrors) 
                      {
                          System.Console.WriteLine("Property: {0} Error : {1}", validationError.PropertyName, validationError.ErrorMessage);

                      }
                  }
              }

               return Json(new {success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
          }*/