using E_CommerceSystem.Helpers;
using E_CommerceSystem.Models;
using E_CommerceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace E_CommerceSystem.Controllers
{

    [ApiController]
    [Route("api/orders")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;


        // Constructor with dependency injection
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost]
        [Authorize]
        public IActionResult PlaceOrder([FromBody] List<OrderProduct> orderItems)
        {
            try
            {
                // Get the authenticated user's ID
                var token = Helper_Functions.ExtractTokenFromRequest(Request);
                var userId = Helper_Functions.GetUserIDFromToken(token);    

                // Validate input
                if (orderItems == null || !orderItems.Any())
                    return BadRequest("Order items cannot be empty.");

                foreach (var item in orderItems)
                {
                    if (item.PID <= 0)
                        return BadRequest("Invalid Product ID.");
                    if (item.Quantity <= 0)
                        return BadRequest("Quantity must be greater than 0.");
                }

               
                // Add the order
                var newOrder = _orderService.Add_Order(int.Parse(userId), orderItems);

                return CreatedAtAction(nameof(GetOrderDetails), new { O_ID = newOrder.Order_Id }, newOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }






        // Get all orders for a user by the authenticated user
        [HttpGet]
        public IActionResult GetUserOrders()
        {
            try
            {

                // Retrieve the U_ID from the authenticated user's claims
                var U_ID = int.Parse(User.Claims.First(c => c.Type == "NameIdentifier").Value);

               // add order using the order service
                var Userorders = _orderService.GetOrdersFromSpecificUser(U_ID);



                // 201 Created response with the order details
                return Ok(Userorders);
            }
            catch (Exception ex)
            {

                // 400 Bad Request response 

                return Unauthorized(new { Error = ex.Message });
            }
        }

        // get details of a specific order for the authenticated user.
       
        
        [HttpGet("{O_ID}")]
        public IActionResult GetOrderDetails(int O_ID)
        {
            try
            {

                var U_ID = int.Parse(User.Claims.First(c => c.Type == "NameIdentifier").Value);

                // order details using the order service

                var order_details = _orderService.GetOrderDetails(O_ID, U_ID);


                return Ok(order_details);
            }
            catch (Exception ex)
            {

                // 404 Not Found response with the error message

                return NotFound(new { Error = ex.Message });
            }
        }

    }






}
 

