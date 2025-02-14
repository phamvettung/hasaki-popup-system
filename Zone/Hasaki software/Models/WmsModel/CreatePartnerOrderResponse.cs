using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Intech_software.Models.WmsModel
{
    public class IntToStringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
                return reader.TryGetInt64(out long l) ?
                l.ToString() :
                reader.GetDouble().ToString();

            return reader.GetString();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    internal class CreatePartnerOrderResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("data")]
        public PartnerOrder Data { get; set; }
    }

    class PartnerOrder
    {
        [JsonPropertyName("partner_id")]
        public int PartnerId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("partner_code")]
        [JsonConverter(typeof(IntToStringConverter))]
        public string PartnerCode { get; set; }
    }
}
