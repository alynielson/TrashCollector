using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollectorProject.Models
{
    public class CustomerPickupViewModel
    {
        public Pickup Pickup {get; set;}
        public Customer Customer { get; set; }

    }
}