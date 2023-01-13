using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;
using Models.Api.Req_Res.Common.Request;
using Models.DBModels.Enums;

namespace Models.Api.Req_Res.Admin.Request
{
    public class AddWorkerRequestModel : AdditionalSecurityRequestModel
    {
        [JsonPropertyName("newUserLogin")]
        public string NewUserLogin { get; set; }
        [JsonPropertyName("newUserPassword")]
        public string NewUserPassword { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("role")]
        public UserRole Role { get; set; }
        
        public static implicit operator UserModel(AddWorkerRequestModel request)
        {
            return new UserModel
            {
                UserId = -1,
                Login = request.NewUserLogin,
                Email = request.Email,
                Name = request.Name,
                Phone = request.Phone,
                Password = request.NewUserPassword
            };
        }
    }
}
