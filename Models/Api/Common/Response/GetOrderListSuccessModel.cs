using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;
using Models.DBModels.Enums;

namespace Models.Api.Common.Response
{
    public class GetOrderListSuccessModel
    {
        [JsonPropertyName("productCategory")]
        public ProductCategory? Category { get; set; }

        [JsonPropertyName("orderList")]
        public List<OrderModel> OrderList { get; set; }
    }
}
