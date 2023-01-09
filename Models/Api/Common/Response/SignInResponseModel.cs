using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;

namespace Models.Api.Common.Response
{
    public class SignInResponseModel
    {
        [JsonPropertyName("success")] 
        public bool Success { get; set; }
        [JsonPropertyName("user")]
        public UserModel CreatedUser { get; set; }
        [JsonPropertyName("message")] 
        public string Message { get; set; }
    }
}