using System.Text.Json.Serialization;
using Models.Api.Req_Res.Common.Request;

namespace Models.Api.Req_Res.Admin.Request;

public class RemoveWorkerRequestModel : AdditionalSecurityRequestModel
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
}