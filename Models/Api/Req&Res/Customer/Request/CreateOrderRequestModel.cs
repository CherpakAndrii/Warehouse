using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;
using Models.Api.Req_Res.Common.Request;
using Models.DBModels;

namespace Models.Api.Req_Res.Customer.Request
{
    public class CreateOrderRequestModel : CommonUserRequestModel
    {
        public ProductModel Product { get; set; }
        [JsonPropertyName("quantity")]
        public uint Quantity { get; set; }
        [JsonPropertyName("user")]
        public User User { get; set; }
    }
}
