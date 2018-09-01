using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollectorProject.Models;

namespace TrashCollectorProject.Controllers
{
    public class PickupController : Controller
    {
        // GET: Pickup
        public ActionResult Index()
        {
            return View();
        }

        // GET: Pickup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pickup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pickup/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pickup/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pickup/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pickup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pickup/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult RequestNew(int id)
        {
            Pickup tempPickup = new Pickup();
            tempPickup.CustomerId = id;
            tempPickup.Date = DateTime.Now;
            return View(tempPickup);
        }
        [HttpPost]
        public ActionResult RequestNew(Pickup pickup, int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            pickup.Completed = false;
            pickup.CustomerId = id;
           
            pickup.IsOneTime = true;
            var customerZip = db.Customers.Find(pickup.CustomerId).ZipCode;
            var employeeId = db.Employees.SingleOrDefault(e => e.ZipCode == customerZip).Id;
            
            pickup.EmployeeId = employeeId;
          
            db.Pickups.Add(pickup);
            db.SaveChanges();
            return RedirectToAction("Index", "Customer", new { id = pickup.CustomerId });
        }

        
    }
}
