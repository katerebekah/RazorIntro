using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IntroToRazor.DAL
{
    public class IntroToRazorContext : DbContext
    {
        public IntroToRazorContext(DbContextOptions<IntroToRazorContext> options)
            : base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
    }
}
