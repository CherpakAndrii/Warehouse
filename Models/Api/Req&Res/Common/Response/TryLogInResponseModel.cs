using System.Text.Json.Serialization;
using Models.DBModels.Enums;

namespace Models.Api.Req_Res.Common.Response
{
    public class TryLogInResponseModel
    {
        [JsonPropertyName("success")] 
        public bool Success { get; set; }
        [JsonPropertyName("message")] 
        public string Message { get; set; }
        [JsonPropertyName("sessionId")] 
        public int SessionId { get; set; }

        [JsonPropertyName("role")] 
        public UserRole Role { get; set; }
    }
}