using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Intech_software.DTO
{
    internal class InboundSelection
    {
        [JsonPropertyName("zone1")] public string Zone1Selection { get; set; }
        [JsonPropertyName("zone2")] public string Zone2Selection { get; set; }
        [JsonPropertyName("zone3")] public string Zone3Selection { get; set; }
        [JsonPropertyName("zone4")] public string Zone4Selection { get; set; }
        [JsonPropertyName("zone5")] public string Zone5Selection { get; set; }
        [JsonPropertyName("zone6")] public string Zone6Selection { get; set; }
        [JsonPropertyName("zone7")] public string Zone7Selection { get; set; }
        [JsonPropertyName("zone8")] public string Zone8Selection { get; set; }
        [JsonPropertyName("zone9")] public string Zone9Selection { get; set; }
        [JsonPropertyName("zone0")] public string Zone0Selection { get; set; }
        [JsonPropertyName("shop22")] public string Shop22Selection { get; set; }
        [JsonPropertyName("zone99")] public string Zone99Selection { get; set; }
        [JsonPropertyName("zone1a")] public string Zone1ASelection { get; set; }
        [JsonPropertyName("zone2a")] public string Zone2ASelection { get; set; }
        [JsonPropertyName("zone3a")] public string Zone3ASelection { get; set; }
        [JsonPropertyName("zone4a")] public string Zone4ASelection { get; set; }
        [JsonPropertyName("zone5a")] public string Zone5ASelection { get; set; }
        [JsonPropertyName("zone6a")] public string Zone6ASelection { get; set; }
        [JsonPropertyName("zone7a")] public string Zone7ASelection { get; set; }
        [JsonPropertyName("zone8a")] public string Zone8ASelection { get; set; }
        [JsonPropertyName("zone9a")] public string Zone9ASelection { get; set; }
        [JsonPropertyName("zone6b")] public string Zone6BSelection { get; set; }
        [JsonPropertyName("zonekt")] public string ZoneKTSelection { get; set; }
        [JsonPropertyName("zonegift")] public string ZoneGiftSelection { get; set; }
        // group zone, mã zone => cổng
        [JsonPropertyName("group1")] public Dictionary<string, string> Group1 { get; set; } = new Dictionary<string, string>();
        [JsonPropertyName("group2")] public Dictionary<string, string> Group2 { get; set; } = new Dictionary<string, string>();
        [JsonPropertyName("group3")] public Dictionary<string, string> Group3 { get; set; } = new Dictionary<string, string>();
        [JsonPropertyName("group4")] public Dictionary<string, string> Group4 { get; set; } = new Dictionary<string, string>();
        [JsonPropertyName("group5")] public Dictionary<string, string> Group5 { get; set; } = new Dictionary<string, string>();
        [JsonPropertyName("group6")] public Dictionary<string, string> Group6 { get; set; } = new Dictionary<string, string>();
        [JsonPropertyName("group7")] public Dictionary<string, string> Group7 { get; set; } = new Dictionary<string, string>();
        [JsonPropertyName("group8")] public Dictionary<string, string> Group8 { get; set; } = new Dictionary<string, string>();

        public void SaveSelection(string filename)
        {
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText("./tmp/" + filename, json);
        }
        public void LoadSelection(string filename)
        {
            string jsonContent = File.ReadAllText("./tmp/" + filename);
            var res = JsonSerializer.Deserialize<InboundSelection>(jsonContent);
            Zone0Selection = res.Zone0Selection;
            Zone1Selection = res.Zone1Selection;
            Zone2Selection = res.Zone2Selection;
            Zone3Selection = res.Zone3Selection;
            Zone4Selection = res.Zone4Selection;
            Zone5Selection = res.Zone5Selection;
            Zone6Selection = res.Zone6Selection;
            Zone7Selection = res.Zone7Selection;
            Zone8Selection = res.Zone8Selection;
            Zone9Selection = res.Zone9Selection;
            Shop22Selection = res.Shop22Selection;
            Zone99Selection = res.Zone99Selection;
            Zone1ASelection = res.Zone1ASelection;
            Zone2ASelection = res.Zone2ASelection;
            Zone3ASelection = res.Zone3ASelection;
            Zone4ASelection = res.Zone4ASelection;
            Zone5ASelection = res.Zone5ASelection;
            Zone6ASelection = res.Zone6ASelection;
            Zone7ASelection = res.Zone7ASelection;
            Zone8ASelection = res.Zone8ASelection;
            Zone9ASelection = res.Zone9ASelection;
            Zone6BSelection = res.Zone6BSelection;
            ZoneKTSelection = res.ZoneKTSelection;
            ZoneGiftSelection = res.ZoneGiftSelection;
            Group1 = res.Group1;
            Group2 = res.Group2;
            Group3 = res.Group3;
            Group4 = res.Group4;
            Group5 = res.Group5;
            Group6 = res.Group6;
            Group7 = res.Group7;
            Group8 = res.Group8;
        }
    }
}
