using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;
using Models.DBModels.Enums;

namespace Models.Api.Common.Response
{
    public abstract class ActionWithProductSuccessModel
    {
        [JsonPropertyName("productModel")]
        public ProductModel Product { get; set; }
    }
}
