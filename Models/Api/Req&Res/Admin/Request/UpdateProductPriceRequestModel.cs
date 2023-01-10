using System.Text.Json.Serialization;
using Models.Api.Req_Res.Common.Request;

namespace Models.Api.Req_Res.Admin.Request
{
    public class UpdateProductPriceRequestModel : ActionWithExistingProductRequestModel
    {
        [JsonPropertyName("newProductPrice")]
        public float NewProductPrice { get; set; }
    }
}
