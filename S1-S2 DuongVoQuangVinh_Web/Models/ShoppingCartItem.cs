namespace S1_S2_DuongVoQuangVinh__Web__.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public Product? Product { get; set; }

        public int Qty { get; set; }

        public string? ShoppingCartId { get; set; }
    }
}
