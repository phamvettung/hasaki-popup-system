using System.Text.Json.Serialization;

namespace Intech_software.Models.WmsModel
{
    internal class LoginRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
