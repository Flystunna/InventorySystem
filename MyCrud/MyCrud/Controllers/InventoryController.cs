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
    public class InventoryController : Controller
    {
        //
        // GET: /Inventory/
        public ActionResult Index()
        {
            return View();
            
        }

        public ActionResult GetData()
        {
            using (DBInventory db = new DBInventory())
            {
                List<Inventory> empList = db.Inventories.ToList<Inventory>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]

        public ActionResult Add(int id = 0)
        {
            if (id == 0)
                return View(new Inventory());
            else
            {
                using (DBInventory db = new DBInventory())
                {
                    return View(db.Inventories.Where(x => x.InventoryID == id).FirstOrDefault<Inventory>());
                }
            }
        }
        [HttpPost]
        public ActionResult Add(Inventory emp)
        {
            using (DBInventory db = new DBInventory())
            {
                if (emp.InventoryID == 0)
                {
                    db.Inventories.Add(emp);
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
            using (DBInventory db = new DBInventory())
            {
                Inventory emp = db.Inventories.Where(x => x.InventoryID == id).FirstOrDefault<Inventory>();
                db.Inventories.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

	}
}