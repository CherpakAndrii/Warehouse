using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;

namespace Models.Api.Common.Response
{
    public abstract class ActionWithProductSuccessModel
    {
        [JsonPropertyName("productModel")]
        public ProductModel Product { get; set; }
    }
}
