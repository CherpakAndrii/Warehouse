using Infrastructure.Interfaces;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Infrastructure.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly AppDbContext _context;

        public ProductsRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public Product GetProduct(int productId)
        {
            return _context.Products.Where(c => c.ProductId == productId).FirstOrDefault();
        }

        public Product GetProduct(string name)
        {
            return _context.Products.Where(c => c.Name == name).LastOrDefault();
        }

        public IEnumerable<Product> GetProductList(ProductCategory? category)
        {
            if (category is null) return _context.Products;
            return _context.Products.Where(c => c.Category == category);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
