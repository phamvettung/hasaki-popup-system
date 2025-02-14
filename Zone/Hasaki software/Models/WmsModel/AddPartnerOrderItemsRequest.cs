using System.Text.Json.Serialization;

namespace Intech_software.Models.WmsModel
{
    internal class AddPartnerOrderItemsRequest
    {
        // field Id in CreatePartnerOrderRequest
        [JsonPropertyName("partner_order_id")]
        public int PartnerOrderId { get; set; }

        // so customer code
        [JsonPropertyName("code")]
        public string Code { get; set; }

    }
}
