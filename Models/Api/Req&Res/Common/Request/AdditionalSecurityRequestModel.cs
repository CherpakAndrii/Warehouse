using System.Text.Json.Serialization;

namespace Models.Api.Req_Res.Common.Request;

public abstract class AdditionalSecurityRequestModel : CommonUserRequestModel
{
    [JsonPropertyName("currentPassword")]
    public string CurrentPassword { get; set; }
}