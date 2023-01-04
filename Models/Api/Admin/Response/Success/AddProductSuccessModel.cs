using System.Text.Json.Serialization;

namespace Models.Api.Admin.Response.Success
{
    public class AddProductSuccessModel
    {
        [JsonPropertyName("productId")]
        public int? ProductId { get; set; }

        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [JsonPropertyName("productQuantity")]
        public uint? ProductQuantity { get; set; }
    }
}
