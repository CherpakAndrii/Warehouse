using System.Text.Json.Serialization;

namespace Models.Api.Req_Res.Common.Request;

public class AdditionalSecurityRequestModel : CommonUserRequestModel
{
    [JsonPropertyName("currentPassword")]
    public string CurrentPassword { get; set; }
}