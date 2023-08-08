using Ecommerce.Models;
using Ecommerce.Models.ViewModels;
using Ecommerce.Services;
using Ecommerce.Services.Exceptions;
using Ecommerce.Util;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace Ecommerce.Controllers {
    public class ProductsController : Controller {
        private readonly ProductService _productService;
        private readonly PromotionService _promotionService;
        private readonly Cart _cart;

        public ProductsController(ProductService productService, PromotionService promotionService, Cart cart) {
            _productService = productService;
            _promotionService = promotionService;
            _cart = cart;
        }

        public async Task<IActionResult> Index() {
            var products = await _productService.FindAllAsync();
            var finalProductsList = new List<Product>();

            foreach (var product in products) {
                product.Promotion = await _promotionService.FindByIdAsync(product.PromotionId);
                finalProductsList.Add(product);
            }

            return View(finalProductsList);
        }

        public async Task<IActionResult> Create() {
            var promotions = await _promotionService.FindAllAsync();
            var viewModel = new ProductFormViewModel { Promotions = promotions };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product) {
            if (!ModelState.IsValid) {
                List<Promotion> promotions = await _promotionService.FindAllAsync();
                ProductFormViewModel viewModel = new ProductFormViewModel() { Product = product, Promotions = promotions };
                return View(viewModel);
            }


            await _productService.InsertAsync(product);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "ID do produto não foi fornecido pelo portal" });
            }

            var product = await _productService.FindByIdAsync(id);
            if (product == null) {
                return RedirectToAction(nameof(Error), new { message = "ID do produto não foi encontrado" });
            }


            _cart.RemoveItem(product);
            HttpContext.Session.SetObject("Cart", _cart);

            await _productService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "ID do produto não foi fornecido pelo portal" });
            }

            var product = await _productService.FindByIdAsync(id.Value);
            if (product == null) {
                return RedirectToAction(nameof(Error), new { message = "ID do produto não foi encontrado" });
            }

            List<Promotion> promotions = await _promotionService.FindAllAsync();
            ProductFormViewModel viewModel = new ProductFormViewModel() { Product = product, Promotions = promotions };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product) {
            if (!ModelState.IsValid) {
                List<Promotion> promotions = await _promotionService.FindAllAsync();
                ProductFormViewModel viewModel = new ProductFormViewModel() { Product = product, Promotions = promotions };
                return View(viewModel);
            }

            if (id != product.Id) {
                return RedirectToAction(nameof(Error), new { message = "ID passado na URL não é compatível com o ID do produto para edição" });
            }

            try {
                await _productService.UpdateAsync(product);

                product.Promotion = await _promotionService.FindByIdAsync(product.PromotionId);
                _cart.UpdateItem(product);
                HttpContext.Session.SetObject("Cart", _cart);

                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });

            }
        }

        public IActionResult Error(string message) {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(viewModel);
        }
    }
}
