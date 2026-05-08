
using Microsoft.EntityFrameworkCore;
using S1_S2_DuongVoQuangVinh__Web__.Data;
using S1_S2_DuongVoQuangVinh__Web__.Models;
using S1_S2_DuongVoQuangVinh__Web__.Models.Interfaces;
namespace S1_S2_DuongVoQuangVinh__Web__.Models.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public IEnumerable<Product> GetTrendingProducts()
        {
            return _context.Products.Take(5).ToList();
        }

        public Product? GetProductDetail(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
