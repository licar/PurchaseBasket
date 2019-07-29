using System.Collections.Generic;
using System.Threading.Tasks;
using PurchaseBasket.Models;
using PurchaseBasket.Utilites;

namespace PurchaseBasket.Services
{
    public interface IBasketService
    {
        Task<Result<IList<ProductModel>>> GetListAsync();
        Task<Result> AddAsync(ProductModel product);
    }
}
