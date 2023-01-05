using System.Text.Json.Serialization;
using Models.DBModels;

namespace Models.Api.Common.Request
{
    public abstract class ActionWithExistingProductRequestModel
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
    }
}