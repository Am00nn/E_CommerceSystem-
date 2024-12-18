using E_CommerceSystem.Models;

namespace E_CommerceSystem.Services
{
    public interface IOrderService
    {
        Order Add_Order(int U_ID, List<OrderProduct> product_order);
        Order GetOrderDetails(int Order_ID, int U_ID);
        IEnumerable<Order> GetOrdersFromSpecificUser(int U_ID);
    }
}