using E_CommerceSystem.Models;

namespace E_CommerceSystem.Repositories
{
    public interface IOrderRepo
    {
        Order AddOrder(Order order);
        Order GetOrderById(int id, int userId);
        IEnumerable<Order> GetOrdersByUserId(int userId);
    }
}