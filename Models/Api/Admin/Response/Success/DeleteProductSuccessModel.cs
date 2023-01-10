using System.Text.Json.Serialization;
using Models.Api.Common.Response;

namespace Models.Api.Admin.Response.Success;

public class DeleteProductSuccessModel : ActionWithProductSuccessModel
{
    [JsonPropertyName("ordersRejected")]
    public int OrdersRejected { get; set; }
}