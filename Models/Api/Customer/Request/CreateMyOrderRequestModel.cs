using System.Text.Json.Serialization;
using Models.Api.Common.Response;
using Models.DBModels.Enums;

namespace Models.Api.Customer.Request
{
    public class CreateMyOrderRequestModel : Models.Api.Common.Request.CommonUserRequestModel
    {
        [JsonPropertyName("product")]
        public ProductModel Product { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("price")]
        public double OrderPrice { get; set; }
    }
}
