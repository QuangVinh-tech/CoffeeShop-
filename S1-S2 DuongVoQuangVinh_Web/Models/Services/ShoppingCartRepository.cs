using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using S1_S2_DuongVoQuangVinh__Web__.Data;
using S1_S2_DuongVoQuangVinh__Web__.Models;
using S1_S2_DuongVoQuangVinh__Web__.Models.Interfaces;

namespace S1_S2_DuongVoQuangVinh__Web__.Models.Services
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private AppDbContext dbContext;

        public ShoppingCartRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ShoppingCartItem>? ShoppingCartItems { get; set; }

        public string? ShoppingCartId { get; set; }

        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            ISession? session =
                services.GetRequiredService<IHttpContextAccessor>()
                ?.HttpContext?.Session;

            AppDbContext context =
                services.GetService<AppDbContext>()
                ?? throw new Exception("Error initializing AppDbContext");

            string cartId =
                session?.GetString("CartId")
                ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCartRepository(context)
            {
                ShoppingCartId = cartId
            };
        }

        public void AddToCart(Product product)
        {
            var shoppingCartItem =
                dbContext.ShoppingCartItems.FirstOrDefault(
                    s => s.Product != null &&
                         s.Product.Id == product.Id &&
                         s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Qty = 1
                };

                dbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Qty++;
            }

            dbContext.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems =
                dbContext.ShoppingCartItems.Where(
                    s => s.ShoppingCartId == ShoppingCartId);

            dbContext.ShoppingCartItems.RemoveRange(cartItems);

            dbContext.SaveChanges();
        }

        public List<ShoppingCartItem> GetAllShoppingCartItems()
        {
            return ShoppingCartItems ??=
                dbContext.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId)
                .Include(p => p.Product)
                .ToList();
        }

        public decimal GetShoppingCartTotal()
        {
            var totalCost =
                dbContext.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId)
                .Select(s => s.Product.Price * s.Qty)
                .Sum();

            return totalCost;
        }

        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem =
                dbContext.ShoppingCartItems.FirstOrDefault(
                    s => s.Product != null &&
                         s.Product.Id == product.Id &&
                         s.ShoppingCartId == ShoppingCartId);

            var quantity = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Qty > 1)
                {
                    shoppingCartItem.Qty--;

                    quantity = shoppingCartItem.Qty;
                }
                else
                {
                    dbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            dbContext.SaveChanges();

            return quantity;
        }
    }
}