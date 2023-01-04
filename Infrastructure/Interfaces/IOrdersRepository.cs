using Models.DBModels;

namespace Infrastructure.Interfaces
{
    public interface IOrdersRepository
    {
        void CreateOrder(Order order);
        Order GetOrder(int orderId);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
