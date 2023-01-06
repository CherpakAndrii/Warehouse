using System.Text.Json.Serialization;
using Models.DBModels.Enums;

namespace Models.Api.Common.Request
{
    public class GetProductListRequestModel
    {
        [JsonPropertyName("productCategory")]
        public ProductCategory? ProductCategory { get; set; }
    }
}