using System.Text.Json.Serialization;
using Models.Api.Req_Res.Common.Request;
using Models.DBModels.Enums;

namespace Models.Api.Req_Res.Admin.Request
{
    public class AddWorkerRequestModel : SignInRequestModel
    {
        [JsonPropertyName("role")]
        public UserRole Role { get; set; }
    }
}
