using Models.DBModels;

namespace Infrastructure.Interfaces
{
    public interface IOrdersRepository
    {
        void CreateOrder(Order order);
        Order? GetOrder(int orderId);
        void UpdateOrder(Order order);
        IEnumerable<Order> GetOrderList(int? userId, int? productId);
        void DeleteOrder(Order order);
    }
}
