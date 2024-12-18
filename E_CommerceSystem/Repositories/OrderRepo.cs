using E_CommerceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSystem.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ApplicationDbContext _context;

        public OrderRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public Order AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public IEnumerable<Order> GetOrdersByUserId(int userId)
        {
            return _context.Orders
                           .Include(o => o.OrderItems)
                           .ThenInclude(op => op.Product)
                           .Where(o => o.UserId == userId)
                           .ToList();
        }

        public Order GetOrderById(int id, int userId)
        {
            return _context.Orders
                           .Include(o => o.OrderItems)
                           .ThenInclude(op => op.Product)
                           .FirstOrDefault(o => o.Order_Id == id && o.UserId == userId);
        }

    }
}
