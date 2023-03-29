using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.DTOs;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Data Transformation Object - DTO

            ProductManager productManager = new ProductManager(new EfProductDal());
            foreach (var product in productManager.GetProductDetails())
            {
                Console.WriteLine(product.ProductName + "/" + product.CategoryName);
            }

        }
    }
}
