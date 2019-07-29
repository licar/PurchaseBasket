using PurchaseBasket.Models;
using PurchaseBasket.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchaseBasket.Services
{
    public interface IProductsService
    {
        Task<Result<IList<ProductModel>>> GetAsync();
        Task<Result> AddAsync(ProductModel product);
        Task<Result<ProductModel>> GetAsync(int id);
    }
}
