using System.Text.Json.Serialization;
using Models.Api.Common.Request;

namespace Models.Api.Manager.Request
{
    public class DecreaseProductQuantityRequestModel : ActionWithExistingProductRequestModel
    {
        [JsonPropertyName("productQuantityToAdd")]
        public uint ProductQuantityToDecrease { get; set; }
    }
}