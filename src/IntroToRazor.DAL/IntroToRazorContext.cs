using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IntroToRazor.DAL
{
    public class IntroToRazorContext : DbContext
    {
        public IntroToRazorContext() { }
        public IntroToRazorContext(DbContextOptions<IntroToRazorContext> options)
            : base(options)
        { }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
    }
}
