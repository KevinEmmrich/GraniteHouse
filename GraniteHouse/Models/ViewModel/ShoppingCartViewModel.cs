using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Models.ViewModel
{
    public class ShoppingCartViewModel
    {
        public List<Products> Products { get; set; }

        // I would have called it Appointment, since there is only one.
        public Appointments Appointments { get; set; }
    }
}
