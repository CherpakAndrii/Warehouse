using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;
using Models.DBModels.Enums;

namespace Models.Api.Admin.Response.Success
{
    public class GetUserListSuccessModel
    {
        [JsonPropertyName("userList")]
        public List<UserModel> UserList { get; set; }
    }
}
