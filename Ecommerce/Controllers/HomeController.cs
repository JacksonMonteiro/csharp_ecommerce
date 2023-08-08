using Ecommerce.Models;
using Ecommerce.Models.ViewModels;
using Ecommerce.Services;
using Ecommerce.Util;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecommerce.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        private readonly ProductService _productService;
        private readonly PromotionService _promotionService;
        private readonly Cart _cart;

        public HomeController(ILogger<HomeController> logger, ProductService productService, Cart cart, PromotionService promotionService) {
            _logger = logger;
            _productService = productService;
            _cart = cart;
            _promotionService = promotionService;
        }

        public async Task<IActionResult> Index() {
            // Cart
            var cartFromSession = HttpContext.Session.GetObject<Cart>("Cart");
            var cart = new Cart();

            if (cartFromSession != null) {
                cart.Items = cartFromSession.Items;
            }

            // Products with promotion
            var products = await _productService.FindAllAsync();
            var finalProductsList = new List<Product>();
            foreach (var product in products) {
                product.Promotion = await _promotionService.FindByIdAsync(product.PromotionId);
                finalProductsList.Add(product);
            }

            HomeViewModel viewModel = new HomeViewModel { Products = finalProductsList, Cart = cart };
            return View(viewModel);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id) {
            var product = await _productService.FindByIdAsync(id);
            product.Promotion = await _promotionService.FindByIdAsync(product.PromotionId);

            if (product != null) {
                _cart.AddProduct(product);
                HttpContext.Session.SetObject("Cart", _cart);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id) {
            var product = await _productService.FindByIdAsync(id);
            if (product != null) {
                _cart.RemoveItem(product);
                HttpContext.Session.SetObject("Cart", _cart);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> IncreaseQuantity(int id) {
            var product = await _productService.FindByIdAsync(id);

            if (product != null) {
                _cart.IncreaseQuantity(product);
                HttpContext.Session.SetObject("Cart", _cart);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseQuantity(int id) {
            var product = await _productService.FindByIdAsync(id);

            if (product != null) {
                _cart.DecreaseQuantity(product);
                HttpContext.Session.SetObject("Cart", _cart);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

