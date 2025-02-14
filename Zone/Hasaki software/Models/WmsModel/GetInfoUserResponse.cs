using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Intech_software.Models.WmsModel
{
    internal class GetInfoUserResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("user")]
        public User User { get; set; }
        [JsonPropertyName("permissions")]
        public List<string> Permissions { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class User
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
