using System.Text.Json.Serialization;

namespace Models.Api
{
    public class ErrorResponseModel
    {
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
