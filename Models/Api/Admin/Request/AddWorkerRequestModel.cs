using System.Text.Json.Serialization;
using Models.Api.Common.Request;
using Models.DBModels;
using Models.DBModels.Enums;

namespace Models.Api.Admin.Request
{
    public class AddWorkerRequestModel : SignInRequestModel
    {
        [JsonPropertyName("role")]
        public UserRole Role { get; set; }
    }
}
