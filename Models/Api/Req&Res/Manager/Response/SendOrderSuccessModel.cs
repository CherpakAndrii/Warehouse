using System.Text.Json.Serialization;
using Models.Api.Req_Res.Common.Response;

namespace Models.Api.Req_Res.Manager.Response
{
    public class SendOrderSuccessModel : ActionWithOrderSuccessModel
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}