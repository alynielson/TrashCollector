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

        public ActionResult SendToIndex(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var customer = db.Customers.SingleOrDefault(u => u.ApplicationUserId == id);
            return RedirectToAction("Index", "Customer", new { id = customer.Id });
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
                ScheduleNormalPickups(customer,db);
                var newCustomer = db.Customers.Single(a => a.ApplicationUserId == id);
                return RedirectToAction("Index", "Customer", new {id = newCustomer.Id});
            }
            catch
            {
                return View();
            }
        }

        private DateTime FindFirstDayToSchedule(Customer customer, DateTime dayToSchedule)
        {
            while (dayToSchedule.DayOfWeek != customer.PickupDay)
            {
                dayToSchedule = dayToSchedule.AddDays(1);
            }
            return dayToSchedule;
        }

        private void CreateNewPickup(Customer customer, ApplicationDbContext db, DateTime pickupDate)
        {
            Pickup pickup = new Pickup();
            pickup.Date = pickupDate;
            pickup.CustomerId = customer.Id;
            var customerZip = db.Customers.Find(pickup.CustomerId).ZipCode;
            var employeeId = db.Employees.SingleOrDefault(e => e.ZipCode == customerZip).Id;
            pickup.EmployeeId = employeeId;
            db.Pickups.Add(pickup);
            db.SaveChanges();
        }

        public void ScheduleNormalPickups(Customer customer, ApplicationDbContext db)
        {
            DateTime pickupDate;
            if (customer.DateScheduledThrough == null)
            {
                pickupDate = FindFirstDayToSchedule(customer, DateTime.Now);
            }
            else
            {
                pickupDate = FindFirstDayToSchedule(customer, (DateTime)customer.DateScheduledThrough);
            }
            int pickupsToSchedule = 4;
            int pickupsScheduled = 0;
            while (pickupsScheduled < pickupsToSchedule)
            {
                if (pickupDate <= customer.CustomStartDate || pickupDate >= customer.CustomEndDate || customer.CustomStartDate == null)
                {
                    CreateNewPickup(customer, db, pickupDate);
                    pickupDate = pickupDate.AddDays(7);
                    pickupsScheduled++;
                }
            }
            customer.DateScheduledThrough = pickupDate;
            db.SaveChanges();
        }
        public ActionResult ViewCharges(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            CustomerPickupViewModel viewModel = new CustomerPickupViewModel();
            var unpaidPickups = db.Pickups.Where(p => p.CustomerId == id && p.Charged == true && p.Paid == false);
            var pickupsWithCustomers = unpaidPickups.Join(db.Customers, p => p.CustomerId, c => c.Id, (p, c) => new { p, c });
            var pickupsConverted = pickupsWithCustomers.Select(a => new CustomerPickup { Pickup = a.p, Customer = a.c }).ToList();

            viewModel.CustomerPickup = pickupsConverted;
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var currentMonthPickups = unpaidPickups.Where(p => p.Date >= firstDayOfMonth).ToList();
            if (currentMonthPickups.Count > 0)
            {
                viewModel.CurrentMonthCharges = currentMonthPickups.Sum(p => p.Price);
            }
            else
            {
                viewModel.CurrentMonthCharges = 0;
            }
            viewModel.OutstandingBalance = db.Customers.Find(id).MoneyOwed - viewModel.CurrentMonthCharges;
            return View(viewModel);
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
            if (customerInDB.DateScheduledThrough > DateTime.Now.Date)
            {
                var pickupsToDelete = db.Pickups.Where(p => p.CustomerId == customer.Id && p.Date > DateTime.Now && p.Completed == false && p.IsOneTime == false).ToList();
                foreach (Pickup pickup in pickupsToDelete)
                {
                    db.Pickups.Remove(pickup);
                }
                customerInDB.DateScheduledThrough = DateTime.Now;
            }
            ScheduleNormalPickups(customerInDB, db);
            db.SaveChanges();
            return RedirectToAction("Index", "Customer", new { id = customer.Id });
        }
        public ActionResult SuspendPickups(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var customer = db.Customers.Find(id);
            return View(customer);
        }
       
        [HttpPost]
        public ActionResult SuspendPickups(Customer customer)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var customerInDB = db.Customers.Find(customer.Id);
            customerInDB.CustomStartDate = customer.CustomStartDate;
            customerInDB.CustomEndDate = customer.CustomEndDate;
            if (customerInDB.DateScheduledThrough >= customerInDB.CustomStartDate)
            {
                var pickupsToDelete = db.Pickups.Where(p => p.CustomerId == customer.Id && p.Date >= customer.CustomStartDate && p.Date <= customer.CustomEndDate && p.Completed == false).ToList();
                foreach (Pickup pickup in pickupsToDelete)
                {
                    db.Pickups.Remove(pickup);
                }
                customerInDB.DateScheduledThrough = customerInDB.CustomEndDate;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Customer", new { id = customer.Id });
        }

    }
}
