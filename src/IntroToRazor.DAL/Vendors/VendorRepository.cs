using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroToRazor.DAL
{
    public class VendorRepository : IVendorRepository
    {
        private IntroToRazorContext database { get; }

        public VendorRepository(IntroToRazorContext context)
        {
            database = context;
        }
        public IEnumerable<Vendor> GetAllVendors()
        {
            return database.Vendors;
        }

        public Vendor GetVendorById(int VendorId)
        {
            return database.Vendors.FirstOrDefault(Vendor => Vendor.VendorId == VendorId);
        }

        public Vendor GetVendorByProduct(int productId)
        {
            //this could go into the products repo simpler database.Products.FirstOrDefault(product => product.ProductId == productId).Vendor;
            return database.Vendors.FirstOrDefault(Vendor => Vendor.Products.Any(product => product.ProductId == productId));
        }

        public void AddVendor(Vendor vendor)
        {
            database.Vendors.Add(vendor);
            SaveChanges();
        }

        public void EditVendor(Vendor vendor)
        {
            database.Entry(vendor).State = EntityState.Modified;
            SaveChanges();
        }

        public void DeleteVendor(int vendorId)
        {
            var currentVendor = database.Vendors.FirstOrDefault(Vendor => Vendor.VendorId == vendorId);
            database.Vendors.Remove(currentVendor);
            SaveChanges();
        }

        private void SaveChanges()
        {
            database.SaveChanges();
        }
    }
}
