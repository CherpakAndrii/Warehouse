using System.Text.Json.Serialization;

namespace Models.Api.Req_Res.Common.Response
{
    public class ErrorResponseModel
    {
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
