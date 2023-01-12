using System.Text.Json.Serialization;
using Models.Api.Req_Res.Common.Request;

namespace Models.Api.Req_Res.Manager.Request
{
    public class UpdateProductQuantityRequestModel : ActionWithExistingProductRequestModel
    {
        [JsonPropertyName("productQuantityDifference")]
        public int ProductQuantityDifference { get; set; }
    }
}