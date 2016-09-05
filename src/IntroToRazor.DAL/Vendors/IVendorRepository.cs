using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroToRazor.DAL
{
    public interface IVendorRepository
    {
        IEnumerable<Vendor> GetAllVendors();

        Vendor GetVendorById(int VendorId);

        Vendor GetVendorByProduct(int productId);

        void AddVendor(Vendor vendor);

        void EditVendor(Vendor vendor);

        void DeleteVendor(int vendorId);
    }
}
