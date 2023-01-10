using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;

namespace Models.Api.Req_Res.Common.Response;

public class GetMyProfileResponseModel
{
    [JsonPropertyName("myProfile")]
    public UserModel Profile { get; set; }
}