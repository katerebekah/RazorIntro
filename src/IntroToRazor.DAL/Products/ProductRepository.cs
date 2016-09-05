using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroToRazor.DAL
{
    public class ProductRepository : IProductRepository
    {
        private IntroToRazorContext database { get; }

        public ProductRepository(IntroToRazorContext context)
        {
            database = context;
        }

        public Product GetProductById(int productId)
        {
            return database.Products.FirstOrDefault(product => product.ProductId == productId);
        }

        public IEnumerable<Product> GetProductsByVendor(int vendorId)
        {
            return database.Products.Where(product => product.Vendor.VendorId == vendorId);
        }

        public void AddProduct (Product product)
        {
            database.Products.Add(product);
            SaveChanges();
        }

        public void EditProduct (Product product)
        {
            database.Entry(product).State = EntityState.Modified;
            SaveChanges();
        }

        public void DeleteProduct (int productId)
        {
            var currentProduct = database.Products.FirstOrDefault(product => product.ProductId == productId);
            database.Products.Remove(currentProduct);
            SaveChanges();
        }

        private void SaveChanges()
        {
            database.SaveChanges();
        }
    }
}
