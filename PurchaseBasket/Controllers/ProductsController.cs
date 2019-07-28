using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PurchaseBasket.Models;
using PurchaseBasket.Services;

namespace PurchaseBasket.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly IBasketService basketService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await productsService.GetAsync();

            if (result.Status != Utilites.Status.OK)
            {
                ViewBag.ErrorMessage = result.Message;
            }

            return View(result.Value);
        }

        public async Task<IActionResult> Create(ProductModel product)
        {
            if (string.IsNullOrWhiteSpace(product.Name) || product.Weight <= 0)
            {
                return View();
            }
            var result = await productsService.AddAsync(product);

            if (result.Status != Utilites.Status.OK)
            {
                ViewBag.ErrorMessage = result.Message;
            }
            return RedirectToAction("Index", "Products");
        }
    }
}