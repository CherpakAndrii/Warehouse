using System.Text.Json.Serialization;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Models.Api.ApiEntityModels;

public class ProductModel
{
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }

    [JsonPropertyName("productName")]
    public string Name { get; set; }

    [JsonPropertyName("productQuantity")]
    public uint Quantity { get; set; }
        
    [JsonPropertyName("productPrice")]
    public float Price { get; set; }
        
    [JsonPropertyName("category")]
    public ProductCategory Category { get; set; }
    private ProductModel(Product prod)
    {
        ProductId = prod.ProductId!.Value;
        Name = prod.Name;
        Quantity = prod.Quantity;
        Price = prod.Price;
        Category = prod.Category;
    }
        
    public static implicit operator ProductModel(Product p) => new (p);
    public static implicit operator Product(ProductModel pm) => new ()
    {
        ProductId = pm.ProductId,
        Name = pm.Name,
        Quantity = pm.Quantity,
        Price = pm.Price,
        Category = pm.Category
    };
}