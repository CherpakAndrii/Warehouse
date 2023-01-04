using System.Text.Json.Serialization;
using Models.DBModels;

namespace Models.Api.Admin.Request
{
    public class AddProductRequestModel
    {

        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [JsonPropertyName("productNumber")]
        public int? ProductId { get; set; }

        [JsonPropertyName("productQuantity")]
        public uint? ProductQuantity { get; set; }

        public Product ConvertToProduct()
            => new()
            {
                Name = ProductName,
                ProductId = ProductId,
                Quantity = ProductQuantity
            };
    }
}
