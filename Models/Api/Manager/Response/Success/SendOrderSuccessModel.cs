using System.Text.Json.Serialization;
using Models.Api.Common.Response;

namespace Models.Api.Manager.Response.Success
{
    public class SendOrderSuccessModel : ActionWithOrderSuccessModel
    {
        [JsonPropertyName("orderId")]
        public int? OrderId { get; set; }
        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; }
    }
}