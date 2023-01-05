using System.Text.Json.Serialization;
using Models.DBModels;

namespace Models.Api.Common.Request
{
    public abstract class ActionWithExistingOrderRequestModel
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }
    }
}