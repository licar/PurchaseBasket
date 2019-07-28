using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PurchaseBasket.Models;
using PurchaseBasket.Utilites;

namespace PurchaseBasket.Services
{
    public class BaseBasketService : IBasketService
    {
        private List<ProductModel> products = new List<ProductModel>();
        const int MAX_WEIGHT = 20;

        public async Task<Result<IEnumerable<ProductModel>>> GetListAsync()
        {
            return new Result<IEnumerable<ProductModel>> { Value = products };
        }
        
        public async Task<Result> AddAsync(ProductModel product)
        {
            var result = CheckLimit(product);
            if (result.Status != Status.OK)
            {
                return result;
            }

            var idx = products.FindIndex(p => p.Weight >= product.Weight);
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