using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Models
{
    public class ProductsSelectedForAppointment
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int ProductId { get; set; }

        // associate AppointmentId and ProductId back to parent tables.
        [ForeignKey("AppointmentId")]
        public virtual Appointments Appointments { get; set; }

        [ForeignKey("ProductId")]
        public virtual Products Products { get; set; }
    }
}
