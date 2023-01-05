using Models.DBModels;

namespace Infrastructure.Interfaces
{
    public interface IWarehouseCustomersService : IWarehouseUserService
    {
        string GetMyOrders();
        string MakeOrder(Product product);
        string GetProductInfo();
    }
}
