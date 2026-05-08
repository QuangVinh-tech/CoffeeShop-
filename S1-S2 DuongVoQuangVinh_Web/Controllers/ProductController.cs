using Microsoft.AspNetCore.Mvc;
using S1_S2_DuongVoQuangVinh__Web__.Models;
using S1_S2_DuongVoQuangVinh__Web__.Models.Interfaces;

namespace S1_S2_DuongVoQuangVinh__Web__.Controllers
{

    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Shop()
        {
            var products = productRepository.GetProducts();

            return View(products);
        }
    }

}
