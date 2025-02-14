using System.Text.Json.Serialization;

namespace Intech_software.DTO
{
    internal class DvvcSelection
    {
        [JsonPropertyName("dv1")] public string Dvvc1 { get; set; }
        [JsonPropertyName("dv2")] public string Dvvc2 { get; set; }
        [JsonPropertyName("dv3")] public string Dvvc3 { get; set; }
        [JsonPropertyName("dv4")] public string Dvvc4 { get; set; }
        [JsonPropertyName("dv5")] public string Dvvc5 { get; set; }
        [JsonPropertyName("dv6")] public string Dvvc6 { get; set; }
        [JsonPropertyName("dv7")] public string Dvvc7 { get; set; }
        [JsonPropertyName("dv8")] public string Dvvc8 { get; set; }
        [JsonPropertyName("dv9")] public string Dvvc9 { get; set; }
        [JsonPropertyName("dv10")] public string Dvvc10 { get; set; }

    }
}
