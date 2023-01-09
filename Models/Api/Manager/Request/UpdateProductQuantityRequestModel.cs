using System.Text.Json.Serialization;
using Models.Api.Common.Request;

namespace Models.Api.Manager.Request
{
    public class UpdateProductQuantityRequestModel : ActionWithExistingProductRequestModel
    {
        [JsonPropertyName("productQuantityDifference")]
        public uint ProductQuantityDifference { get; set; }
    }
}