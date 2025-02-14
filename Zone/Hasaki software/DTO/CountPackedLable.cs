using System.Collections.Generic;
using System.Data;

namespace Intech_software.DTO
{
    internal class CountPackedLable
    {
        public string Zone { get; set; }
        public int NumPackedLabel { get; set; }


        public List<CountPackedLable> ParseFromDt(DataTable dt)
        {
            List<CountPackedLable> countPackedLables = new List<CountPackedLable>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                CountPackedLable shippingOrder = new CountPackedLable
                {
                    Zone = Helper.GetStringValue(row, "zone"),
                    NumPackedLabel = Helper.GetIntValue(row, "num_packed_label"),
                };

                countPackedLables.Add(shippingOrder);
            }

            return countPackedLables;
        }
    }

}
