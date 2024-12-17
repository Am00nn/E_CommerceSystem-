using E_CommerceSystem.Models;
using E_CommerceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSystem.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize] // All endpoints require authorization
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        // Constructor to inject ProductService
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        /// Add a new product (Admin only).
    
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddProduct(
            [FromQuery] string p_Name,
            [FromQuery] decimal price,
            [FromQuery] int stock,
            [FromQuery] string description
           )
        {
            if (string.IsNullOrEmpty(p_Name) || price <= 0 || stock < 0)
                return BadRequest("Invalid product details. Price must be greater than zero and stock cannot be negative.");

            try
            {
                var newProduct = new Product
                {
                    P_Name = p_Name,
                    Price = price,
                    Stock = stock,
                    Description = description
                };

                var addedProduct = _productService.AddProduct(newProduct);
                return CreatedAtAction(nameof(GetProductById), new { id = addedProduct.P_Id }, addedProduct);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding product: {ex.Message}");
            }
        }

        /// Update an existing product (Admin only).
  
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateProduct(
            [FromQuery] int id,
            [FromQuery] string p_Name,
            [FromQuery] decimal price,
            [FromQuery] int stock,
            [FromQuery] string description
            )
        {
            if (id <= 0)
                return BadRequest("Product ID must be greater than zero.");

            if (string.IsNullOrEmpty(p_Name) || price <= 0 || stock < 0)
                return BadRequest("Invalid product details. Price must be greater than zero and stock cannot be negative.");

            try
            {
                var updatedProduct = new Product
                {
                    P_Name = p_Name,
                    Price = price,
                    Stock = stock,
                    Description = description
                   
                };

                var result = _productService.UpdateProduct(id, updatedProduct);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound($"Error updating product: {ex.Message}");
            }
        }

     
        /// Get a list of products with filtering and pagination.
    
        [HttpGet]
        public IActionResult GetProducts(
            [FromQuery] string name,
            [FromQuery] decimal? minPrice,

            [FromQuery] decimal? maxPrice,

            [FromQuery] int page = 1,

            [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page number and page size must be greater than zero.");

            try
            {
                var products = _productService.GetProducts(name, minPrice, maxPrice, page, pageSize);
                if (!products.Any())
                    return NotFound("No products found matching the criteria.");

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving products: {ex.Message}");
            }
        }

 
        /// Get product details by ID.
        
        [HttpGet("{id}")]
        public IActionResult GetProductById([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("Product ID must be greater than zero.");

            try
            {
                var product = _productService.GetProductById(id);
                if (product == null)
                    return NotFound($"Product with ID {id} not found.");

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving product: {ex.Message}");
            }
        }
    }






}
