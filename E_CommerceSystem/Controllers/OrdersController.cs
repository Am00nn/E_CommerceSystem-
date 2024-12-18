using E_CommerceSystem.Models;
using E_CommerceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        // add a new order for the authenticated user 
        [HttpPost]
        public IActionResult AddOrder([FromBody] List<OrderProduct> order_product )
        {
            try
            {
                // Extract the U_ID from the authenticated user's claims
                var U_ID = int.Parse(User.Claims.First(c => c.Type == "NameIdentifier").Value);


                // add order using the order service
                var order = _orderService.Add_Order(U_ID, order_product);


                // Created response with the order details

                return CreatedAtAction(nameof(GetOrderDetails), new { ID = order.Order_Id }, order);
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
 

