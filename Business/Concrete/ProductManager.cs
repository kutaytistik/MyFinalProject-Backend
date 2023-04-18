using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;

        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //Eğer mevcut kategori sayısı 15'i geçtiyse sisteme yeni ürün eklenemez

            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                                               CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                                               CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult();
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 20)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            var result = _productDal.GetAll();
            return new SuccessDataResult<List<Product>>(result, Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            var result = _productDal.GetAll(p => p.CategoryId == id);
            return new SuccessDataResult<List<Product>>(result);
        }

        public IDataResult<Product> GetById(int productId)
        {
            var result = _productDal.Get(p => p.ProductId == productId);
            return new SuccessDataResult<Product>(result);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            var result = _productDal.GetAll(p => p.UnitPrice <= max && p.UnitPrice >= min);
            return new SuccessDataResult<List<Product>>(result);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 00)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            var result = _productDal.GetProductDetails();
            return new SuccessDataResult<List<ProductDetailDto>>(result);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }


        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            //Aynı isimde ürün eklenemez
            //bulursa true döndürür bulamazsa false 
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }


    }
}
