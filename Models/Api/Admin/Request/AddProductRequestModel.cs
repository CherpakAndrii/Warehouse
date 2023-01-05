using System.Text.Json.Serialization;
using Models.DBModels;

namespace Models.Api.Admin.Request
{
    public class AddProductRequestModel
    {

        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [JsonPropertyName("productId")]
        public int? ProductId { get; set; }
        [JsonPropertyName("productPrice")]
        public float ProductPrice { get; set; }

        [JsonPropertyName("productQuantity")]
        public uint ProductQuantity { get; set; }

        public Product ConvertToProduct()
            => new()
            {
                ProductId = ProductId,
                Name = ProductName,
                Price = ProductPrice,
                Quantity = ProductQuantity
            };
    }
}
