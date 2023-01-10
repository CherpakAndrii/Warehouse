using System.Text.Json.Serialization;
using Models.Api.Req_Res.Common.Response;

namespace Models.Api.Req_Res.Admin.Response;

public class DeleteProductSuccessModel : ActionWithProductSuccessModel
{
    [JsonPropertyName("ordersRejected")]
    public int OrdersRejected { get; set; }
}