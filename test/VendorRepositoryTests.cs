using Xunit;
using IntroToRazor.DAL;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Tests
{
    public class VendorRepositoryTests
    {
        private readonly Mock<IntroToRazorContext> context = new Mock<IntroToRazorContext>();
        Mock<DbSet<Vendor>> Vendors = new Mock<DbSet<Vendor>>();
        private IQueryable<Vendor> vendors;
        Product product1, product2, product3;
        Vendor vendor1, vendor2;
        

        public void SetUp()
        {
            vendor1 = new Vendor { VendorId = 11, Address = "Address", FirstName = "Bob", LastName = "Smith", PhoneNumber = "123-231-1232" };
            vendor2 = new Vendor { VendorId = 13, Address = "Address", FirstName = "Beth", LastName = "Thomas", PhoneNumber = "231-211-3332" };
            
            vendors = new List<Vendor> {vendor1, vendor2}.AsQueryable();

            Vendors.As<IQueryable<Vendor>>().Setup(x => x.Provider).Returns(vendors.Provider);
            Vendors.As<IQueryable<Vendor>>().Setup(x => x.Expression).Returns(vendors.Expression);
            Vendors.As<IQueryable<Vendor>>().Setup(x => x.ElementType).Returns(vendors.ElementType);
            Vendors.As<IQueryable<Vendor>>().Setup(x => x.GetEnumerator()).Returns(vendors.GetEnumerator());
            
            context.Setup(x => x.Vendors).Returns(this.Vendors.Object);
        }

        [Fact]
        public void GetVendorById_ReturnsVendor_GivenAValidId() 
        {
            SetUp();
            var repo = new VendorRepository(context.Object);
            
            //Arrange
            int id = 11; //a Vendor with this id is in mock dbset

            //Act
            var actualVendor = repo.GetVendorById(id);

            //Assert
            Assert.Equal(vendor1, actualVendor);
        }

        [Fact]
        public void GetVendorById_ReturnsNull_GivenAnInvalidId()
        {
            //Arrange
            SetUp();
            var repo = new VendorRepository(context.Object);
            int id = 2; //a Vendor with this id is NOT in mock dbset

            //Act
            var actualVendor = repo.GetVendorById(id);

            //Assert
            Assert.Null(actualVendor);
        }

        [Fact]
        public void GetProductsByVendor_ReturnsListOfProducts_GivenAValidId()
        {
            //Arrange
            SetUp();
            var repo = new ProductRepository(context.Object);
            int vendorId = 11; //Valid vendor Id

            //Act
            var actualProducts = repo.GetProductsByVendor(vendorId);

            //Assert
            Assert.Equal(Products.Object.Where(x => x.Vendor.VendorId == vendorId), actualProducts);
        }

        [Fact]
        public void GetProductsByVendor_ReturnsEmpty_GivenAnInvalidId()
        {
            //Arrange
            SetUp();
            var repo = new ProductRepository(context.Object);
            int vendorId = 133; //Invalid vendor Id

            //Act
            var actualProducts = repo.GetProductsByVendor(vendorId);

            //Assert
            Assert.Empty(actualProducts);
        }

        [Fact]
        public void AddProduct_SuccessfullyAddsProduct_GivenAValidProduct()
        {
            //Arrange
            SetUp();
            var repo = new ProductRepository(context.Object);
            var newProduct = new Product { Name = "Name4", Description = "Description4", Price = 24, Vendor = vendor2 };
            
            //Act
            repo.AddProduct(newProduct);

            //Assert
            Products.Verify(x => x.Add(It.Is<Product>(p => p.Equals(newProduct))), Times.Once());
        }

        [Fact]
        public void AddProduct_ThrowsException_GivenContextThrowsException()
        {
            //Arrange
            SetUp();
            var repo = new ProductRepository(context.Object);
            Product newProduct = null;
            Products.Setup(x => x.Add(null)).Throws(new System.Exception());

            //Act
            //Assert
            Assert.Throws<System.Exception>(delegate { repo.AddProduct(newProduct); });
        }

        [Fact]
        public void EditProduct_SuccessfullyEditsProduct_GivenAValidProduct()
        {
            //Arrange
            SetUp();
            var repo = new ProductRepository(context.Object);
            
            //Act
            //repo.EditProduct(product1);

            //Assert
            //Verify(x => x.Entry(product1), Times.Once());
        }

        [Fact]
        public void EditProduct_ThrowsException_GivenAnInvalidId()
        {
                        //Arrange
            SetUp();
            var repo = new ProductRepository(context.Object);
            context.Setup(x => x.Entry(It.IsAny<Product>())).Throws(new System.Exception());


            //Act
            //Assert
            Assert.Throws<System.Exception>(delegate { repo.EditProduct(product1); });
        }

        [Fact]
        public void DeleteProduct_SuccessfullyDeletesProduct_GivenAValidId()
        {
            //Arrange
            SetUp();
            var repo = new ProductRepository(context.Object);
            
            //Act
            repo.DeleteProduct(99);

            //Assert
            Products.Verify(x => x.Remove(It.Is<Product>(p => p.Equals(product3))), Times.Once());
        }

        [Fact]
        public void DeleteProduct_ThrowsException_GivenAnInvalidId()
        {

            //Arrange
            SetUp();
            var repo = new ProductRepository(context.Object);
            Products.Setup(x => x.Remove(null)).Throws(new Exception());
            
            //Act
            //Assert
            Assert.Throws<Exception>(delegate { repo.DeleteProduct(0); });
        }
    }
}
