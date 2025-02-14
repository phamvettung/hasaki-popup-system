using System.Collections.Generic;

namespace Intech_software.Constants
{
    public static class Constants
    {
        public static readonly List<int> TanTheNhatIds = new List<int>(
            new List<int>
            {
                562,
                575,
                577,
                578,
                579,
                580,
                582,
                583,
                586,
                587,
                620,
                184, // SPA - GENERAL - có gì méc anh Tú


                // kho moi
                938,
                947,
                948,
                949,
                950,
                951,
                952,
                953,
                954,
                955,
                956,
                957,
                958,
                959,
                960,
                961,
                971,

            }
            );
        public static readonly string PACKAGE_STATUS_CREATED = "1"; // status pending
        public static readonly string PACKAGE_STATUS_WAITING_IT = "19"; // status pending
        public static readonly string PACKAGE_STATUS_TAKEN = "4";
        public static readonly string PACKAGE_STATUS_SHIPPING_IT = "20";
        public static readonly string PACKAGE_STATUS_SHIPPED_IT = "21";
        public static readonly string PACKAGE_STATUS_TRANSSHIPPED = "14";
        public static readonly string PACKAGE_STATUS_TEMPORARY_IMPORTED = "22";
        public static readonly string PACKAGE_STATUS_DONE = "7";
        public static readonly string PACKAGE_STATUS_CANCEL = "10";

        public static readonly string DEFAULT_DVVC_SELECTION_FILE = "./tmp/init_selection.json";
        public static readonly string DEFAULT_ZONE_SELECTION_FILE = "config_zone_selection.json";

        public static readonly int API_STATUS_SUCCESS = 1;

        public static readonly int TTN_STORE_ID = 210;

        // api phan zone
        public static string API_EMAIL { get; set; } = "ducvv@hasaki.vn";
        public static int STOCK_ID { get; set; } = 1203;
        public static int LOCATION_ID_22_TTN { get; set; } = 186;
        public static int LOCATION_ID_170_QUOC_LO { get; set; } = 398;
    }
}
