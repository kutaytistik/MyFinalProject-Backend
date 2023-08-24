using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class NorthwindContext : DbContext
    {
        //Entity Framework Altyapısı kullanılıyor
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //proje hangi veritabanı ile ilişkili olduğun belirteceğimiz yer
            //Connection String
            //sql case insensitive
            //postgre sql case sensitive

            //Veritabanı Bağlantısı Gerekli
        }

        //Hangi nesnem hangi tabloya karşılık geldiğini belirtiyorum
        //tablolarla nesneleri ilişkilendiriyoruz
        //entity framework altyapısını kullanarak

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
