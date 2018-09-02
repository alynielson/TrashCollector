using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollectorProject.Models;

namespace TrashCollectorProject.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index(int id, string dayChosen = "Today")
        {
            ApplicationDbContext db = new ApplicationDbContext();
            CustomerPickupViewModel viewModel = new CustomerPickupViewModel();
            DateTime dateChosen = ConvertDateChosen(dayChosen);
            DateTime endOfDayChosen = dateChosen.AddDays(1);
            var futurePickups = db.Pickups.Where(p => p.EmployeeId == id && p.Date >= dateChosen && p.Date < endOfDayChosen).ToList();
            var pickupsWithCustomers = futurePickups.Join(db.Customers, p => p.CustomerId, c => c.Id, (p,c) => new { p,c});
            var pickupsConverted = pickupsWithCustomers.Select(a => new CustomerPickup { Pickup = a.p, Customer = a.c}).ToList();
            viewModel.CustomerPickup = pickupsConverted;
            return View(viewModel);
        }

        private DateTime ConvertDateChosen(string dateChosen)
        {
            if (dateChosen == "Today")
            {
                return DateTime.Now.Date;
            }
            else
            {
                DayOfWeek dayChosen = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dateChosen, true);
                DateTime dateToCheck = DateTime.Now.Date;
                while (dayChosen != dateToCheck.DayOfWeek)
                {
                    dateToCheck = dateToCheck.AddDays(1);
                }
                return dateToCheck;
            }
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, string id)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                Employee employee = new Employee()
                {
                    ApplicationUserId = id,
                    Name = collection["Name"],
                    ZipCode = Convert.ToInt32(collection["ZipCode"]),
                };
                db.Employees.Add(employee);
                db.SaveChanges();
                var empId = db.Employees.Single(e => e.ApplicationUserId == id).Id;
                return RedirectToAction("Index", "Employee", new { id = empId });
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
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

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
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
    }
}
