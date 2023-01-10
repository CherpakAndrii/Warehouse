using System.Text.Json.Serialization;
using Models.Api.Common.Request;

namespace Models.Api.Admin.Request;

public class RemoveWorkerRequestModel : CommonUserRequestModel
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
}