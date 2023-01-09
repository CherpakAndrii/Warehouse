using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;

namespace Models.Api.Customer.Request
{
    public class CreateOrderRequestModel : Models.Api.Common.Request.CommonUserRequestModel
    {

        [JsonPropertyName("order")]
        public OrderModel Order { get; set; }
    }
}
