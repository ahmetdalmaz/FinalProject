using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
     

        public ProductManager(IProductDal productDal, ICategoryService categoryService) //bir entity manager kendi hariç başka bir dalı enjekte edemezzz
        {
            _productDal = productDal;
            _categoryService = categoryService;
     
          
        }
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
           IResult result= BusinessRules.Run(CheckIfProductNameExists(product),
                CheckProductCountOfCategory(product.CategoryId)
                );

            if (result!=null)
            {
                return result;
            }           
          
            _productDal.Add(product);
             return new SuccessResult(Messages.ProductAdded);
              
        }



        private IResult CheckCategoryCount() 
        {
            if (_categoryService.GetAll().Data.Count>10)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        
        }


        private IResult CheckIfProductNameExists(Product product) 
        {
            var result = _productDal.GetAll(p => p.ProductName == product.ProductName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            else
            {
                return new SuccessResult();
            }

        
        }

        private IResult CheckProductCountOfCategory(int categoryId) 
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result>=10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            else
            {
                return new SuccessResult();
            }
        }

        public IDataResult<List<Product>> GetAll()
        {
            // İş kodları
           // Yetkisi var mı
            if (DateTime.Now.Hour == 3)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll());
            
            
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>( _productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>( _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult< List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>( _productDal.GetProductDetails());
        }
    }
}
