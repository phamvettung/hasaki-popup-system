using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Intech_software.DTO
{
    internal class ZoneConfig
    {
        [JsonPropertyName("zone1")] public string Zone1 { get; set; }
        [JsonPropertyName("zone2")] public string Zone2 { get; set; }
        [JsonPropertyName("zone3")] public string Zone3 { get; set; }
        [JsonPropertyName("zone4")] public string Zone4 { get; set; }
        [JsonPropertyName("zone5")] public string Zone5 { get; set; }
        [JsonPropertyName("zone6")] public string Zone6 { get; set; }
        [JsonPropertyName("zone7")] public string Zone7 { get; set; }
        [JsonPropertyName("zone8")] public string Zone8 { get; set; }
        [JsonPropertyName("zone9")] public string Zone9 { get; set; }
        [JsonPropertyName("zone0")] public string Zone0 { get; set; }
        [JsonPropertyName("shop22")] public string Shop22 { get; set; }
        [JsonPropertyName("zone99")] public string Zone99 { get; set; }

        public void SaveConfig(string filename)
        {
            string json = JsonSerializer.Serialize(this);
            Directory.CreateDirectory("./tmp");
            File.WriteAllText("./tmp/" + filename, json);
        }

        public ZoneConfig LoadConfig(string filename)
        {
            string json = File.ReadAllText("./tmp/" + filename);
            return JsonSerializer.Deserialize<ZoneConfig>(json);
        }
    }
}
