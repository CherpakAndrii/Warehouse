using Infrastructure.Interfaces;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Infrastructure.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly AppDbContext _context;

        public OrdersRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public Order GetOrder(int orderId)
        {
            return _context.Orders.Where(o => o.OrderId == orderId).FirstOrDefault();
        }

        public Order GetJustCreatedOrder(Order order)
        {
            return _context.Orders.LastOrDefault(o => 
                o.User == order.User && 
                o.Product == order.Product &&
                o.Quantity == order.Quantity &&
                o.OrderPrice == order.OrderPrice &&
                o.Status == OrderStatus.Created);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetOrderList(int? userId, int? productId)
        {
            IEnumerable<Order> orderList = _context.Orders;
            if (userId is not null) orderList = orderList.Where(o => o.User.UserId == userId);
            if (productId is not null) orderList = orderList.Where(o => o.Product.ProductId == productId);
            return orderList;
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}
