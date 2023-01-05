using System.Text.Json.Serialization;
using Models.DBModels;

namespace Models.Api.Admin.Request
{
    public class DeleteProductRequestModel
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
    }
}