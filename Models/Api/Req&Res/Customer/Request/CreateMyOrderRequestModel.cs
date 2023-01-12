using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;
using Models.Api.Req_Res.Common.Request;

namespace Models.Api.Req_Res.Customer.Request
{
    public class CreateMyOrderRequestModel : CommonUserRequestModel
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
        [JsonPropertyName("quantity")]
        public uint Quantity { get; set; }
    }
}
