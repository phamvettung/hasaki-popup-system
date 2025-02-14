using System.Collections.Generic;

namespace Intech_software.Constants
{
    public static class ShippingUnit
    {
        public static Dictionary<int, string> Members = new Dictionary<int, string>()
        {
            {644, "EMS (POST)"},
            {616, "Nhất Tín Logistics (POST)"},
            {608, "Shopee Express (POST)"},
            {411, "Aha Move"},
            {67, "DHL"},
            {10, "GiaoHangNhanh"},
            {14, "GiaoHangTietKiem"},
            {71, "Lazada"},
            {534, "Lazada AhaMove"},
            {532, "Lazada B2B VN"},
            {546, "Lazada Cainiao"},
            {522, "Lazada GHN"},
            {536, "Lazada GRAB"},
            {540, "Lazada I Logic VN"},
            {542, "Lazada J&amp;T VN"},
            {524, "Lazada LEX VN"},
            {550, "Lazada LGS"},
            {538, "Lazada Logisthai VN"},
            {530, "Lazada NinjavanVN"},
            {548, "Lazada PickmeeVN"},
            {526, "Lazada Ship60"},
            {528, "Lazada SOFP_VN"},
            {544, "Lazada Vinacapital"},
            {476, "Ninja VAN"},
            {367, "Shopee BEST Express"},
            {302, "Shopee Express"},
            {461, "Shopee Express Instant"},
            {18, "Shopee GHN"},
            {304, "Shopee GHTK"},
            {413, "Shopee J&T Express"},
            {311, "Shopee Now"},
            {391, "Shopee Viettel Post"},
            {409, "Shopee VNpost Nhanh"},
            {633, "Shopee VNPost Tiết Kiệm"},
            {586, "Tiktok BEST Express"},
            {584, "Tiktok GHTK"},
            {582, "Tiktok J&T Express"},
            {361, "Viettel"},
            {407, "Shopee Ninja Van"}
        };

        // 644,616,608,411,67,10,14,71,534,532,546,522,536,540,542,524,550,538,530,548,526,528,544,476,367,302,461,18,304,413,311,391,409,633,586,584,582,361,407

        public static List<string> PrefixZone1 = new List<string>() // shopee express
        {
        };
        public static List<string> PrefixZone2 = new List<string>() // j&t
        {
            "856", // OMS_TIKTOK_JANDT_EXPRESS
            "851", // OMS_TIKTOK_JANDT_EXPRESS
        };
        public static List<string> PrefixZone3 = new List<string>() // lazada
        {
            "BES", // lazada
            "JNT", // lazada
            "LMP", // lazada
        };
        public static List<string> PrefixZone4 = new List<string>() // shopee
        {
            "811", // shopee
            "812", // shopee
            "CO9", // shopee
            "CY9", // shopee
            "CZ6", // shopee
            "G83", // shopee
            "G84", // shopee
            "G8B", // shopee
            "G8D", // shopee
            "G8K", // shopee
            "G8N", // shopee
            "G8P", // shopee
            "SPE", // shopee
            "SPX", // shopee
        };
        public static List<string> PrefixZone5 = new List<string>() // tiktok
        {
            "328", // tiktok
            "329", // tiktok
            "NJV", // unknown
        };


        public static string GetShippingUnitName(int shippingUnitId)
        {
            if (Members.ContainsKey(shippingUnitId))
            {
                return Members[shippingUnitId];
            }
            return string.Empty;
        }

        public static int GetShippingUnitId(string shippingUnitName)
        {
            foreach (var item in Members)
            {
                if (item.Value == shippingUnitName)
                {
                    return item.Key;
                }
            }
            return 0;
        }
    }

    class ShippingUnitMember
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<ShippingUnitMember> ListObjects()
        {
            List<ShippingUnitMember> shippingUnitMembers = new List<ShippingUnitMember>();
            foreach (var item in ShippingUnit.Members)
            {
                shippingUnitMembers.Add(new ShippingUnitMember { Id = item.Key, Name = item.Value });
            }
            return shippingUnitMembers;
        }
    }

}
