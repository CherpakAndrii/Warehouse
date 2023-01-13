using System.Text.Json.Serialization;
using Models.Api.Req_Res.Common.Request;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Models.Api.Req_Res.Admin.Request
{
    public class AddProductRequestModel : CommonUserRequestModel
    {

        [JsonPropertyName("productName")]
        public string ProductName { get; set; }
        
        [JsonPropertyName("productPrice")]
        public float ProductPrice { get; set; }

        [JsonPropertyName("productQuantity")]
        public uint ProductQuantity { get; set; }

        [JsonPropertyName("category")]
        public ProductCategory ProductCategory { get; set; }
        

        public Product ConvertToProduct()
            => new()
            {
                Name = ProductName,
                Price = ProductPrice,
                Quantity = ProductQuantity,
                AvailableAmount = (int)ProductQuantity,
                Category = ProductCategory
            };
    }
}
