using E_CommerceSystem.Models;
using E_CommerceSystem.Repositories;

namespace E_CommerceSystem.Services
{
    public class ProductService : IProductService
    {


        private readonly IProductRepo _productRepo; // Dependency

        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }


        // Add a product 
        public Product AddProduct(Product p)
        {
            // Validate product Price and  Stock before adding
            if (p.Price <= 0 || p.Stock < 0)

                throw new Exception("Invalid product Price  or  Stock .");

            return _productRepo.AddProduct(p);
        }


        // Updates an existing product by ID
        public Product UpdateProduct(int id, Product PreviousProduct)
        {
            var currentProduct = _productRepo.GetProductById(id);

            // Check if product exists
            if (currentProduct == null)

                throw new Exception("Product not found.");

            // Update product properties
            currentProduct.P_Name = PreviousProduct.P_Name;

            currentProduct.Description = PreviousProduct.Description;

            currentProduct.Price = PreviousProduct.Price;

            currentProduct.Stock = PreviousProduct.Stock;

            return _productRepo.UpdateProduct(currentProduct);
        }


        public IEnumerable<Product> GetProducts(string name, decimal? minPrice, decimal? maxPrice, int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
                throw new ArgumentException("Page and page size must be greater than zero.");

            var products = _productRepo.GetAllProducts();

            // Apply filters
            if (!string.IsNullOrEmpty(name))
                products = products.Where(p => p.P_Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (minPrice.HasValue)
                products = products.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                products = products.Where(p => p.Price <= maxPrice.Value);

            // Apply pagination
            return products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }



        // get a product by  ID
        public Product GetProductById(int id)
        {
            return _productRepo.GetProductById(id);
        }


    }
}
