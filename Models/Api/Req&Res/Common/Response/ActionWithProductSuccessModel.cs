using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;

namespace Models.Api.Req_Res.Common.Response
{
    public abstract class ActionWithProductSuccessModel
    {
        [JsonPropertyName("productModel")]
        public ProductModel Product { get; set; }
    }
}
