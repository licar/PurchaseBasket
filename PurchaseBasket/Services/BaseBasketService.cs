using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PurchaseBasket.Models;
using PurchaseBasket.Utilites;

namespace PurchaseBasket.Services
{
    public class BaseBasketService : IBasketService
    {
        private IList<ProductModel> products = new List<ProductModel>();
        const int MAX_WEIGHT = 20;

        public async Task<Result<IList<ProductModel>>> GetListAsync()
        {
            return new Result<IList<ProductModel>> { Value = products };
        }

        public async Task<Result> AddAsync(ProductModel product)
        {
            if (string.IsNullOrWhiteSpace(product.Name) || product.Weight <= 0)
            {
                return new Result { Status = Status.Fail, Message = "Invalid product" };
            }

            var result = CheckLimit(product);
            if (result.Status != Status.OK)
            {
                return result;
            }

            //Change to SortedList
            var idx = products.ToList().FindIndex(p => p.Weight <= product.Weight);
            products.Insert(idx == -1 ? products.Count : idx, product);
            return new Result();
        }

        protected Result CheckLimit(ProductModel product)
        {
            return products.Sum(p => p.Weight) + product.Weight > MAX_WEIGHT 
                ? new Result { Status = Status.Fail, Message = "Maximum weight exceeded" }
                : new Result();
        }
        
    }
}
