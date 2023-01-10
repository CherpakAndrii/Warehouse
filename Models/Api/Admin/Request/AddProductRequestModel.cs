using System.Text.Json.Serialization;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Models.Api.Admin.Request
{
    public class AddProductRequestModel : Models.Api.Common.Request.CommonUserRequestModel
    {

        [JsonPropertyName("productName")]
        public string ProductName { get; set; }
        
        [JsonPropertyName("productPrice")]
        public float ProductPrice { get; set; }

        [JsonPropertyName("productQuantity")]
        public uint ProductQuantity { get; set; }
        [JsonPropertyName("availableAmount")]
        public int AvailableAmount { get; set; }
        
        [JsonPropertyName("category")]
        public ProductCategory ProductCategory { get; set; }
        

        public Product ConvertToProduct()
            => new()
            {
                Name = ProductName,
                Price = ProductPrice,
                Quantity = ProductQuantity,
                AvailableAmount = AvailableAmount,
                Category = ProductCategory
            };
    }
}
