using E_CommerceSystem.Models;

namespace E_CommerceSystem.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationDbContext _context;


        // Constructor to inject ApplicationDbContext
        public ProductRepo(ApplicationDbContext context)
        {
            _context = context;
        }


        // Adds a new product to the database
        public Product AddProduct(Product p)
        {
            _context.Products.Add(p);
            _context.SaveChanges();
            return p;

        }


        //update product 
        public Product UpdateProduct(Product p)
        {
            _context.Products.Update(p);
            _context.SaveChanges();
            return p;
        }

        // get product by ID
        public Product GetProductById(int ID)
        {


            return _context.Products.Find(ID);

        }


        // get all products from the database
        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }






    }
}
