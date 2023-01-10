using System.Text.Json.Serialization;
using Models.Api.Common.Request;

namespace Models.Api.Customer.Request;

public class RemoveOrderRequestModel : ActionWithExistingOrderRequestModel
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
}