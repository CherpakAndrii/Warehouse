using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;
using Models.DBModels.Enums;

namespace Models.Api.Req_Res.Common.Response
{
    public class GetProductListSuccessModel
    {
        [JsonPropertyName("productCategory")]
        public ProductCategory? Category { get; set; }

        [JsonPropertyName("productList")]
        public List<ProductModel> ProductList { get; set; }
    }
}
