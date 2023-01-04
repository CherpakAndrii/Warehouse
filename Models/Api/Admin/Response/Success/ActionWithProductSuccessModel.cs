using System.Text.Json.Serialization;

namespace Models.Api.Admin.Response.Success
{
    public class ActionWithProductSuccessModel
    {
        [JsonPropertyName("productId")]
        public int? ProductId { get; set; }

        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [JsonPropertyName("productQuantity")]
        public uint? ProductQuantity { get; set; }
    }
    
    public class AddProductSuccessModel : ActionWithProductSuccessModel{}
    public class UpdateProductSuccessModel : ActionWithProductSuccessModel{}
    public class DeleteProductSuccessModel : ActionWithProductSuccessModel{}
}
