using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PurchaseBasket.Models;
using PurchaseBasket.Services;

namespace PurchaseBasket.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService basketService;
        private readonly IProductsService productsService;

        public BasketController(IBasketService basketService, IProductsService productsService)
        {
            this.basketService = basketService;
            this.productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await basketService.GetListAsync();

            if (result.Status != Utilites.Status.OK)
            {
                ViewBag.ErrorMessage = result.Message;
            }

            return View(result.Value);
        }

        public async Task<IActionResult> Add(int id)
        {
            var getProductResult = await productsService.GetAsync(id);

            if (getProductResult.Status != Utilites.Status.OK)
            {
                ViewBag.ErrorMessage = getProductResult.Message;
                return View();
            }

            var result = await basketService.AddAsync(getProductResult.Value);

            if (result.Status != Utilites.Status.OK)
            {
                ViewBag.ErrorMessage = result.Message;
                return View();
            }

            return RedirectToAction("Index", "Products");
        }
    }
}