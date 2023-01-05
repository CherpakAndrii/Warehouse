using System.Text.Json.Serialization;
using Models.Api.Common.Request;

namespace Models.Api.Manager.Request
{
    public class IncreaseProductQuantityRequestModel : ActionWithExistingProductRequestModel
    {
        [JsonPropertyName("productQuantityToAdd")]
        public uint ProductQuantityToAdd { get; set; }
    }
}
