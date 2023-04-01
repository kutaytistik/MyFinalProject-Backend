using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            //business codes

            if (product.ProductName.Length < 2)
            {
                //magic strings
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult();
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour==20)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            var result=_productDal.GetAll();
            return new SuccessDataResult<List<Product>>(result,Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            var result=_productDal.GetAll(p => p.CategoryId == id);
            return new SuccessDataResult<List<Product>>(result);
        }

        public IDataResult<Product> GetById(int productId)
        {
            var result=_productDal.Get(p => p.ProductId == productId);
            return new SuccessDataResult<Product>(result);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            var result=_productDal.GetAll(p => p.UnitPrice <= max && p.UnitPrice >= min);
            return new SuccessDataResult<List<Product>>(result);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 00)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            var result=_productDal.GetProductDetails();
            return new SuccessDataResult<List<ProductDetailDto>>(result);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
