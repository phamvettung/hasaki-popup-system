using System.Text.Json.Serialization;

namespace Intech_software.Models.WmsModel
{
    internal class CreatePartnerOrderRequest
    {
        [JsonPropertyName("store_id")]
        public int StoreId { get; set; }
        [JsonPropertyName("type")]
        public int Type { get; set; }
    }
}
