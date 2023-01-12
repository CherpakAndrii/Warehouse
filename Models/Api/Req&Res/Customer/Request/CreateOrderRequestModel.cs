using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;
using Models.Api.Req_Res.Common.Request;
using Models.DBModels;

namespace Models.Api.Req_Res.Customer.Request
{
    public class CreateOrderRequestModel : CommonUserRequestModel
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
        [JsonPropertyName("quantity")]
        public uint Quantity { get; set; }
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
    }
}
