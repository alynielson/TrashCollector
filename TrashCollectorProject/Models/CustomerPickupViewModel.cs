using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollectorProject.Models
{
    public class CustomerPickup
    {
        public Pickup Pickup {get; set;}
        public Customer Customer { get; set; }

    }

    public class CustomerPickupViewModel
    {
        public List<CustomerPickup> CustomerPickup { get; set; }
        public string DayChosen { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateViewing { get; set; }
        public int EmployeeId { get; set; }
        public double CurrentMonthCharges { get; set; }
       
    }
}