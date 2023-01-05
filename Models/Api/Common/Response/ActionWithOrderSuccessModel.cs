using System.Text.Json.Serialization;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Models.Api.Common.Response
{
    public class ActionWithOrderSuccessModel
    {
        [JsonPropertyName("orderId")]
        public int? OrderId { get; set; }

        [JsonPropertyName("orderStatus")]
        public OrderStatus Status { get; set; }

        [JsonPropertyName("productName")]
        public string ProductName { get; set; }
        
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        
        [JsonPropertyName("price")]
        public double OrderPrice { get; set; }
        
        [JsonPropertyName("customer")]
        public string CustomerName { get; set; }
    }
}
