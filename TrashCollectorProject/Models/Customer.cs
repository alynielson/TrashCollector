using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollectorProject.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        
        public DayOfWeek PickupDay { get; set; }
        
        public DateTime? CustomStartDate { get; set; }
        public DateTime? CustomEndDate { get; set; }
        public double MoneyOwed { get; set; }


    }
}