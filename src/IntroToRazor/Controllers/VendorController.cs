using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IntroToRazor.DAL;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IntroToRazor.Controllers
{
    public class VendorController : Controller
    {
        private IVendorRepository repo { get; }

        public VendorController(IVendorRepository repository)
        {
            repo = repository;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Vendors = repo.GetAllVendors();
            return View();
        }

        public IActionResult Add()
        {
            return View("Vendor");
        }

        public IActionResult Delete()
        {
            return View("Vendor");
        }

        public IActionResult Update()
        {
            return View("Vendor");
        }

        [HttpPost]
        public IActionResult Add(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                repo.AddVendor(vendor);
                return RedirectToAction("Index");
            }

            return View(vendor);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                repo.EditVendor(vendor);
                return RedirectToAction("Index");
            }

            return View(vendor);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                repo.DeleteVendor(vendor.VendorId);
                return RedirectToAction("Index");
            }

            return View(vendor);
        }

    }
}
