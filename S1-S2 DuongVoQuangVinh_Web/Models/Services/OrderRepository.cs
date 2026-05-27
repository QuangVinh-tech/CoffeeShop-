using S1_S2_DuongVoQuangVinh__Web__.Data;
using S1_S2_DuongVoQuangVinh__Web__.Models.Interfaces;

namespace S1_S2_DuongVoQuangVinh__Web__.Models.Services
{
    public class OrderRepository : IOrderRepository
    {
        private AppDbContext dbContext;
        private IShoppingCartRepository shoppingCartRepository;

            public OrderRepository(AppDbContext dbContext, IShoppingCartRepository shoppingCartRepository)
            {
            this.dbContext = dbContext;
            this.shoppingCartRepository = shoppingCartRepository;
            }
        public void PlaceOrder(Order order)
        {
            var shoppingCartItems = shoppingCartRepository.GetAllShoppingCartItems();
            order.OrderDetails = new List<OrderDetail>();
            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity= item.Qty,
                    ProductId=item.Product.Id,
                    Price = item.Product.Price
                };
                order.OrderDetails.Add(orderDetail);
            }
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = shoppingCartRepository.GetShoppingCartTotal();
            dbContext.Order.Add(order);
            dbContext.SaveChanges();
        }
    }
}
