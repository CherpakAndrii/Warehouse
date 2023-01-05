using Models.DBModels;

namespace Infrastructure.Interfaces
{
    public interface IWarehouseCustomersService : IWarehouseUserService
    {
        string GetProducts(string category = null);
        string GetMyOrders();
        string MakeOrder(Product product);
        string GetProductInfo();
    }
}
