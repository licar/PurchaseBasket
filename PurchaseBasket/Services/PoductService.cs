using System.Collections.Generic;
using System.Threading.Tasks;
using PurchaseBasket.Models;
using PurchaseBasket.Utilites;

namespace PurchaseBasket.Services
{
    public sealed class ProductsService : IProductsService
    {
        private IList<ProductModel> products = new List<ProductModel>();

        public async Task<Result<IEnumerable<ProductModel>>> GetAsync()
        {
            return new Result<IEnumerable<ProductModel>> { Value = products };
        }

        public async Task<Result> AddAsync(ProductModel product)
        {
            products.Add(product);
            return new Result();
        }

        public async Task<Result<ProductModel>> GetAsync(int id)
        {
            return id < 0 || id > products.Count - 1  
                ? new Result<ProductModel> { Status = Status.Fail, Message = "Out of index" }
                : new Result<ProductModel> { Value = products[id] };
        }
    }
}
