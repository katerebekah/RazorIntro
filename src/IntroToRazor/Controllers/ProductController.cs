using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IntroToRazor.DAL;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IntroToRazor.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repo { get; }
        private IVendorRepository vendorRepo { get; }

        public ProductController(IProductRepository repository, IVendorRepository vrepo)
        {
            repo = repository;
            vendorRepo = vrepo;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Products = repo.GetAllProducts();
            return View();
        }

        public IActionResult Add()
        {
            ViewBag.Vendors = vendorRepo.GetAllVendors();
            return View("Product");
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (ModelState.IsValid)
            {
                repo.AddProduct(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }


        public IActionResult Delete()
        {
            return View("Product");
        }

        public IActionResult Update()
        {
            return View("Product");
        }

        

        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                repo.EditProduct(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product product)
        {
            if (ModelState.IsValid)
            {
                repo.DeleteProduct(product.ProductId);
                return RedirectToAction("Index");
            }

            return View(product);
        }

    }
}
