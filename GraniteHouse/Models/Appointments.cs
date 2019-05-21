using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Models
{
    public class Appointments
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [Display(Name = "Appointment Time")]
        [NotMapped]
        public DateTime AppointmentTime { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string CustomerPhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Appointment Confirmed:")]
        public bool IsConfirmed { get; set; }
    }
}
