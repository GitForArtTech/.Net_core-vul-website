using System;
using Microsoft.EntityFrameworkCore;

namespace firstMVC.Models
{
    public class ProductDB : DbContext
    {
        public ProductDB(DbContextOptions<ProductDB> options) : base(options) { }
        public DbSet<Product> Product { get; set; }
 
    }
}
