using System.Text.Json.Serialization;

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
    }
}
