using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroToRazor.DAL
{
    public interface IProductRepository
    {
        Product GetProductById(int productId);

        IEnumerable<Product> GetProductsByVendor(int vendorId);

        void AddProduct(Product product);

        void EditProduct(Product product);

        void DeleteProduct(int productId);
    }
}
