using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Extensions;
using GraniteHouse.Models;
using GraniteHouse.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GraniteHouse.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {

        private readonly ApplicationDbContext _db;

        [BindProperty]  //I missed the BindProperty!
        public ShoppingCartViewModel ShoppingCartVM { get; set; }

        public ShoppingCartController(ApplicationDbContext db)
        {
            _db = db;
            ShoppingCartVM = new ShoppingCartViewModel()
            {
                //Products = new List<Models.Products>()
                Products = new List<Products>()
                //Appointments = new Appointments()
            };

        }

        //Get Index ShoppingCart.  Get List of products and appointment data (or blank appointment)
        //public async Task<IActionResult> Index()
        public IActionResult Index()
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstShoppingCart != null  && lstShoppingCart.Count > 0)
            {
                foreach (int cartItem in lstShoppingCart)
                {
                    Products prod = _db.Products
                        .Include(p=>p.SpecialTags)
                        .Include(p=>p.ProductTypes)
                        .Where(p => p.Id == cartItem).FirstOrDefault();

                    ShoppingCartVM.Products.Add(prod);
                }
            } 
            return View(ShoppingCartVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            // Doing validation here since can't do with jquery due to the "Remove Item" button
            // I don't know why I have to rebuild ShoppingCartVM.products -- it shouldn't be null.
            if (ModelState.IsValid == false)
            {
                // need to rebuild shopping cart products.
                List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
                ShoppingCartVM = new ShoppingCartViewModel()
                {
                    Products = new List<Products>()
                };
                if (lstShoppingCart != null && lstShoppingCart.Count > 0)
                {
                    foreach (int cartItem in lstShoppingCart)
                    {
                        Products prod = _db.Products
                            .Include(p => p.SpecialTags)
                            .Include(p => p.ProductTypes)
                            .Where(p => p.Id == cartItem).FirstOrDefault();

                        ShoppingCartVM.Products.Add(prod);
                    }
                }
                return View(ShoppingCartVM);
            }


            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            ShoppingCartVM.Appointments.AppointmentDate = ShoppingCartVM.Appointments.AppointmentDate
                        .AddHours(ShoppingCartVM.Appointments.AppointmentTime.Hour)
                        .AddMinutes(ShoppingCartVM.Appointments.AppointmentTime.Minute);

            Appointments appointments = ShoppingCartVM.Appointments;
            _db.Appointments.Add(appointments);
            _db.SaveChanges();

            // now fill in ProductsSelectedForAppointment which are in the session object
            int appointmentId = appointments.Id;
            //ProductsSelectedForAppointment prods = new ProductsSelectedForAppointment(); can't have it up here -- always enters the last one!!
            foreach (var item in lstCartItems)
            {
                ProductsSelectedForAppointment prods = new ProductsSelectedForAppointment();
                prods.ProductId = item;
                prods.AppointmentId = appointmentId;
                _db.ProductsSelectedForAppointment.Add(prods);
                
            }
            _db.SaveChanges();
            // clear shopping cart in session
            lstCartItems = new List<int>();
            HttpContext.Session.Set("ssShoppingCart", lstCartItems);

            // return to main page.
            return RedirectToAction("AppointmentConfirmation", "ShoppingCart", new { Id = appointmentId });

        }

        public IActionResult Remove(int id)
        {
            //  The appointment data is cleared out if you remove item -- bad!
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstCartItems.Count>0)
            {
                if (lstCartItems.Contains(id))
                {
                    lstCartItems.Remove(id);
                }
            }
            
            // reset the session (SessionExtensions.cs)
            HttpContext.Session.Set("ssShoppingCart", lstCartItems);
            return RedirectToAction(nameof(Index));

        }


        public IActionResult AppointmentConfirmation(int id)
        {
            ShoppingCartVM.Appointments = _db.Appointments.Where(a => a.Id == id).FirstOrDefault();
            List<ProductsSelectedForAppointment> objProdList = _db.ProductsSelectedForAppointment.Where(a => a.AppointmentId == id).ToList();

            foreach (ProductsSelectedForAppointment prod in objProdList)
            {
                ShoppingCartVM.Products
                    .Add(_db.Products.Include(p => p.ProductTypes)
                    .Include(p => p.SpecialTags)
                    .Where(p => p.Id == prod.ProductId).FirstOrDefault() );
            }

            return View(ShoppingCartVM);


        }








    } // end class ShoppingCartController : Controller
} // end namespace GraniteHouse.Areas.Customer.Controllers