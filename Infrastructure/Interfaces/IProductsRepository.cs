using Models.DBModels;

namespace Infrastructure.Interfaces
{
    public interface IProductsRepository
    {
        void CreateProduct(Product product);
        Product GetProduct(int productId);
        Product GetProduct(string productName);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
