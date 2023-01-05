using System.Text.Json.Serialization;

namespace Models.Api.Common.Response
{
    public class ErrorResponseModel
    {
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
