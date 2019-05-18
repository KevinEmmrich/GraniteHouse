using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraniteHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList());
        }

        //GET Create Action Method -- show new productType entry screen
        public IActionResult Create()
        {
            return View();
        }

        //POST Create Action Method -- get entered data from GET and save to SQL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Add(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  // or RedirectToAction("Index")
            }
            else
            {
                return View(productTypes);
            }

        }


        //GET Edit Action Method -- show productType entry screen
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = await _db.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        //POST Edit Action Method -- get entered data from GET and save to SQL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductTypes productTypes)
        {
            if (id != productTypes.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                // updates all fields! need more specific update later on when there are more data columns.
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  // or RedirectToAction("Index")
            }
            else
            {
                return View(productTypes);
            }

        }

        //GET Details Action Method -- show new productType entry screen
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = await _db.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }


        //GET Delete Action Method -- -- go to confirm page
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = await _db.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        //POST Delete Action Method -- get entered data from GET and save to SQL
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var productType = await _db.ProductTypes.FindAsync(id);
            _db.ProductTypes.Remove(productType);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));  // or RedirectToAction("Index")

        }




    }  // end class ProductTypesController : Controller
} // namespace GraniteHouse.Areas.Admin.Controllers