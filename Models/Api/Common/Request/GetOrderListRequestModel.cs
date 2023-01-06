using System.Text.Json.Serialization;

namespace Models.Api.Common.Request
{
    public abstract class GetOrderListRequestModel
    {
        [JsonPropertyName("productId")]
        public int? ProductId { get; set; }
        [JsonPropertyName("userId")]
        public int? UserId { get; set; }
        
    }
}