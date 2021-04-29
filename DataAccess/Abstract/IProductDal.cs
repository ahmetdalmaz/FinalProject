using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product>//burda çünkü tüm veritabanlrıhnda bu işlemler mevcut
    {
        List<ProductDetailDto> GetProductDetails();
    }
}
