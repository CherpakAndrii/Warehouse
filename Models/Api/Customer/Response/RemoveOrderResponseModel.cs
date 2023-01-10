using System.Text.Json.Serialization;
using Models.Api.Common.Response;

namespace Models.Api.Customer.Response;

public class RemoveOrderResponseModel : ActionWithOrderSuccessModel
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("message")]
    public string Message { get; set; }
}