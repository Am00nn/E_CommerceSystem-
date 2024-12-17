using E_CommerceSystem.Models;

namespace E_CommerceSystem.Services
{
    public interface IProductService
    {
        Product AddProduct(Product p);
        Product GetProductById(int id);
        IEnumerable<Product> GetProducts(string name, decimal? minPrice, decimal? maxPrice, int page, int pageSize);
        Product UpdateProduct(int id, Product PreviousProduct);
    }
}