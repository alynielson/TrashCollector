using System;
using System.Collections.Generic;
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
       
    }
}