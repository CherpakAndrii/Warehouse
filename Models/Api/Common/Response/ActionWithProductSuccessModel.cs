using System.Text.Json.Serialization;
using Models.DBModels.Enums;

namespace Models.Api.Common.Response
{
    public abstract class ActionWithProductSuccessModel
    {
        [JsonPropertyName("productId")]
        public int? ProductId { get; set; }

        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [JsonPropertyName("productQuantity")]
        public uint? ProductQuantity { get; set; }
        
        [JsonPropertyName("productPrice")]
        public float ProductPrice { get; set; }
        
        [JsonPropertyName("category")]
        public ProductCategory ProductCategory { get; set; }
    }
}
