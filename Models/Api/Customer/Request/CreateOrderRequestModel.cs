using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;
using Models.DBModels;

namespace Models.Api.Customer.Request
{
    public class CreateOrderRequestModel : Models.Api.Common.Request.CommonUserRequestModel
    {
        public ProductModel Product { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("user")]
        public User User { get; set; }
    }
}
