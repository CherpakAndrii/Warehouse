using System.Text.Json.Serialization;

namespace Models.Api.Req_Res.Common.Response
{
    public class TryLogInResponseModel
    {
        [JsonPropertyName("success")] 
        public bool Success { get; set; }
        [JsonPropertyName("message")] 
        public string Message { get; set; }
        [JsonPropertyName("sessionId")] 
        public int? SessionId { get; set; }
    }
}