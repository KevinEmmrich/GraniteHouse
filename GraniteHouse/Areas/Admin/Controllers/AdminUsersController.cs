using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Models;
using GraniteHouse.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraniteHouse.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]
    public class AdminUsersController : Controller
    {

        private readonly ApplicationDbContext _db;
       

        public AdminUsersController(ApplicationDbContext db)
        {
            _db = db;
        }


      
        public IActionResult Index()
        {
            return View(_db.ApplicationUser.ToList() );
        }


        // get Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || id.Trim().Length==0)
            {
                return NotFound();  // NotFound() base controller method that returns a 404 html error.
            }

            var userFromDb = await _db.ApplicationUser.FindAsync(id);
           
            if (userFromDb == null)
            {
                return NotFound();
            }

            return View(userFromDb);
        }

        //post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (string id, ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ApplicationUser userFromDb = _db.ApplicationUser.Where(u => u.Id == id).FirstOrDefault();
                userFromDb.Name = applicationUser.Name;
                userFromDb.PhoneNumber = applicationUser.PhoneNumber;
                //what about changing email?
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }

            return View(applicationUser);
        }

        //GET Delete Action Method -- -- go to confirm page
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser userFromDb = _db.ApplicationUser.Where(u => u.Id == id).FirstOrDefault();
            if (userFromDb == null)
            {
                return NotFound();
            }

            return View(userFromDb);
        }

        //POST Delete Action Method -- get entered data from GET and save to SQL
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {

            ApplicationUser userFromDb = _db.ApplicationUser.Where(u => u.Id == id).FirstOrDefault();
            userFromDb.LockoutEnd = DateTime.Now.AddYears(+100);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));  // or RedirectToAction("Index")

        }

    } // end class AdminUsersController : Controller
}  // end namespace GraniteHouse.Areas.Admin.Controllers