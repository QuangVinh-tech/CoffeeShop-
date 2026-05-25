using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S1_S2_DuongVoQuangVinh__Web__.Models.Interfaces;

namespace S1_S2_DuongVoQuangVinh__Web__.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IShoppingCartRepository shoppingCartRepository;
        private IProductRepository productRepository;

        public ShoppingCartController(
            IShoppingCartRepository shoppingCartRepository,
            IProductRepository productRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var items = shoppingCartRepository.GetAllShoppingCartItems();
            shoppingCartRepository.ShoppingCartItems = items;

            // Bước 6: Truyền tổng tiền sang View qua ViewBag
            ViewBag.Total = shoppingCartRepository.GetShoppingCartTotal();
            ViewBag.Count = items.Count;

            // Bước 9: Cập nhật số lượng sản phẩm trong giỏ lên Session
            HttpContext.Session.SetInt32("CartCount", items.Count);

            return View(items);
        }

        public RedirectToActionResult AddToShoppingCart(int pId)
        {
            var product = productRepository.GetAllProducts()
                .FirstOrDefault(p => p.Id == pId);

            if (product != null)
            {
                shoppingCartRepository.AddToCart(product);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pId)
        {
            var product = productRepository.GetAllProducts()
                .FirstOrDefault(p => p.Id == pId);

            if (product != null)
            {
                shoppingCartRepository.RemoveFromCart(product);
            }

            return RedirectToAction("Index");
        }
    }
}
