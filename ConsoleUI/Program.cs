using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ICategoryDal categoryDal = new EfCategoryDal();
            IProductDal productDal = new EfProductDal();
            CategoryManager categoryManager = new CategoryManager(categoryDal);
            ProductManager productManager = new ProductManager(productDal);

            //foreach (var category in categoryManager.GetAll())
            //{
            //    Console.WriteLine(category.CategoryName);
            //}

            var result = productManager.GetProductDetails();

            if (result.Success == true)
            {

                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + "/" + product.CategoryName);
                }

            }
            else
            {
                Console.WriteLine(result.Message);
            }


            
            Console.ReadLine();

        }
    }
}
