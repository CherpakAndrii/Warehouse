using System.Text.Json.Serialization;
using Models.Api.Req_Res.Common.Request;

namespace Models.Api.Req_Res.Customer.Request;

public class RemoveOrderRequestModel : ActionWithExistingOrderRequestModel
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
}