using System;
using System.Collections.Generic;
using TrashCollectorProject.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace TrashCollectorProject.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var customer = db.Customers.Find(id);
            return View(customer);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, string id)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                Customer customer = new Customer()
                {
                    ApplicationUserId = id,
                    Name = collection["Name"],
                    Address = collection["Address"],
                    City = collection["City"],
                    State = collection["State"],
                    PickupDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), collection["PickupDay"], true),
                    ZipCode = Convert.ToInt32(collection["ZipCode"]),
                };
                db.Customers.Add(customer);
                db.SaveChanges();
                var custId = db.Customers.Single(a => a.ApplicationUserId == id).Id;
                return RedirectToAction("Index", "Customer", new {id = custId});
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
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

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
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

        public ActionResult ChangePickupDay(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var customer = db.Customers.Find(id);
            return View(customer);
        }
        [HttpPost]
        public ActionResult ChangePickupDay(Customer customer)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var customerInDB = db.Customers.Find(customer.Id);
            customerInDB.PickupDay = customer.PickupDay;
            db.SaveChanges();
            return RedirectToAction("Index", "Customer", new { id = customer.Id });
        }

       

    }
}
