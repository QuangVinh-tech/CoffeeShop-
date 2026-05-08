namespace S1_S2_DuongVoQuangVinh__Web__.Models.Interfaces
{
    public interface IProductRepository

    {
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetTrendingProducts();
        Product? GetProductDetail(int id);

    }
}
