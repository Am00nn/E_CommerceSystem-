using E_CommerceSystem.Models;
using E_CommerceSystem.Repositories;
using System.Numerics;

namespace E_CommerceSystem.Services
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepo _orderRepo;

        private readonly IProductRepo _productRepo;


        // Constructor for injecting the repositories
        public OrderService(IOrderRepo orderRepo, IProductRepo productRepo)
        {
            _orderRepo = orderRepo;

            _productRepo = productRepo;
        }


        // add a new order for a user
        public Order Add_Order(int U_ID, List<OrderProduct> product_order)
        {

            // Check if the Order_product  list is null

            if (product_order == null || !product_order.Any())

                throw new Exception("Order products cannot be null.");

            decimal Total_Amount = 0;  // Variable  total amunt 





            // Check stock and calculate total
            foreach (var p in product_order)
            {

                // product details by product ID

                var product_details = _productRepo.GetProductById(p.PID);

                if (product_details == null)


                    // error if the product does not exist

                    throw new Exception($"Product with ID {p.PID} does not exist.");

                // if Not enough stock display error 

                if (product_details.Stock < p.Quantity)

                    throw new Exception($"Not enough stock for product: {product_details.P_Name}");


                // Calculate the total amount for the order
                Total_Amount += product_details.Price * p.Quantity;



                // Decrease the ordered quantity from  stock


                product_details.Stock -= p.Quantity;

                // Update the product stock in the repository
                _productRepo.UpdateProduct(product_details);
            }

            // Create order
            var order = new Order
            {

                UserId = U_ID,

                TotalAmount = Total_Amount,

                OrderDate = DateTime.UtcNow,

                OrderItems = product_order
            };


            // Save the order in the repository and return the created order
            return _orderRepo.AddOrder(order);
        }


        // getall orders for a specific user
        public IEnumerable<Order> GetOrdersFromSpecificUser(int U_ID)
        {

            // return orders for the specified user
            return _orderRepo.GetOrdersByUserId(U_ID);
        }


        // get the details of a specific order for a user
        public Order GetOrderDetails(int Order_ID, int U_ID)
        {


            //get order details by order ID and user ID

            var User_order = _orderRepo.GetOrderById(Order_ID, U_ID);


            if (User_order == null)

                throw new Exception("Order does not exist or access is denied.");

            return User_order;  // Return the order details 
        }
    }



}

