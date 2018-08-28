using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollectorProject.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        
        public DayOfWeek PickupDay { get; set; }
        
        public DateTime CustomStartDate { get; set; }
        public DateTime CustomEndDate { get; set; }
        public double MoneyOwed { get; set; }


    }
}