using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;

namespace Models.Api.Req_Res.Admin.Response
{
    public class GetUserListSuccessModel
    {
        [JsonPropertyName("userList")]
        public List<UserModel> UserList { get; set; }
    }
}
