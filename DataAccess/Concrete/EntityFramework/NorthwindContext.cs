using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Contex:DB tabloları ile proje classlarını bağlamak , ilişkilendirmek için kullanılacak

    //DbContext Entity Framework ile gelen base bir sınıf

    //öncellikle benim veritabanım şurada demem lazım

    public class NorthwindContext : DbContext
    {
        //Proje ile Veritanbanı Bağlantısı Yapıldı
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true");
        }

        //Tablolar ile nesneler arası ilişkilendirilme işlemleri ,karşılık gelenleri belirleme yapılacak
        //Benim projemdeki Product nesnesi ile veritabanındaki Products tablosunu bağla

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }



    }
}
