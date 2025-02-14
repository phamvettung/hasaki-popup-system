using System.Text.Json.Serialization;

namespace Intech_software.Models.WmsModel
{
    internal class AddPartnerOrderItemsResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }
    }
}
