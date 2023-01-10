using System.Text.Json.Serialization;

namespace Models.Api.Req_Res.Common.Request
{
    public abstract class ActionWithExistingProductRequestModel : CommonUserRequestModel
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
    }
}