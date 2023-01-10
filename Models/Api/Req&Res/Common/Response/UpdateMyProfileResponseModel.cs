using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;

namespace Models.Api.Req_Res.Common.Response;

public class UpdateMyProfileResponseModel
{
    [JsonPropertyName("myProfile")]
    public UserModel Profile { get; set; }
    
    [JsonPropertyName("changesCounter")]
    public int ChangesCounter { get; set; }
}