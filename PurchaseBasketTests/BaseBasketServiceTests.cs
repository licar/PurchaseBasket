using PurchaseBasket.Models;
using PurchaseBasket.Services;
using PurchaseBasket.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PurchaseBasketTests
{
    public class BaseBasketServiceTests
    {
        [Fact]
        public async Task SuccessfulAddToBasket()
        {
            IBasketService basketService = new BaseBasketService();
            var product = new ProductModel { Name = "Wheel", Weight = 1 };
            var addProductResult = await basketService.AddAsync(product);

            Assert.Equal(Status.OK, addProductResult.Status);

            var getProductsResult = await basketService.GetListAsync();
            Assert.Equal(Status.OK, getProductsResult.Status);

            Assert.Equal(product, getProductsResult.Value[0]);
        }

        [Fact]
        public async Task AddMaximumProducts()
        {
            IBasketService basketService = new BaseBasketService();

            var wheel = new ProductModel { Name = "Wheel", Weight = 3 };
            var bike = new ProductModel { Name = "Bike", Weight = 12 };
            var pedal = new ProductModel { Name = "Pedal", Weight = 2 };

            await basketService.AddAsync(wheel);
            await basketService.AddAsync(bike);
            var addPedalResult = await basketService.AddAsync(pedal);

            Assert.Equal(Status.OK, addPedalResult.Status);

            var getProductsResult = await basketService.GetListAsync();
            Assert.Equal(Status.OK, getProductsResult.Status);
            Assert.Equal(3, getProductsResult.Value.Count);

            var expectedProducts = new List<ProductModel>() { bike, wheel, pedal };

            Assert.Equal(expectedProducts, getProductsResult.Value);
        }

        [Fact]
        public async Task AddInvalidProducts()
        {
            IBasketService basketService = new BaseBasketService();

            var wheel = new ProductModel { Name = "", Weight = 3 };
            var bike = new ProductModel { Name = "Bike", Weight = 0 };
            var pedal = new ProductModel { Name = "Pedal", Weight = -1 };

            var addWheelResult = await basketService.AddAsync(wheel);
            Assert.Equal(Status.Fail, addWheelResult.Status);

            var addBikeResult = await basketService.AddAsync(bike);
            Assert.Equal(Status.Fail, addBikeResult.Status);

            var addPedalResult = await basketService.AddAsync(pedal);
            Assert.Equal(Status.Fail, addPedalResult.Status);

            var getProductsResult = await basketService.GetListAsync();
            Assert.Equal(Status.OK, getProductsResult.Status);
            Assert.True(!getProductsResult.Value.Any());
        }

        [Fact]
        public async Task ExceedBucketLimit()
        {
            IBasketService basketService = new BaseBasketService();

            var wheel = new ProductModel { Name = "Wheel", Weight = 20 };

            await basketService.AddAsync(wheel);
            await basketService.AddAsync(wheel);
            var addPedalResult = await basketService.AddAsync(wheel);
            Assert.Equal(Status.Fail, addPedalResult.Status);

            var getProductsResult = await basketService.GetListAsync();
            Assert.Equal(1, getProductsResult.Value.Count);
        }
    }
}
