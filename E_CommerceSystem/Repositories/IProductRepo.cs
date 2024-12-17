using E_CommerceSystem.Models;

namespace E_CommerceSystem.Repositories
{
    public interface IProductRepo
    {
        Product AddProduct(Product p);
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int ID);
        Product UpdateProduct(Product p);
    }
}