using Models.DBModels;
using Models.DBModels.Enums;

namespace Infrastructure.Interfaces
{
    public interface IProductsRepository
    {
        void CreateProduct(Product product);
        Product? GetProduct(int productId);
        Product? GetProduct(string productName);
        IEnumerable<Product> GetProductList(ProductCategory? category);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
