using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intech_software.DTO
{
    internal class ShippingOrder
    {
        public long SoId { get; set; }
        public string SoOrderCode { get; set; }
        public string SoPackedCode { get; set; }
        public string SoCustomerCode { get; set; }
        public long SoCustomerId { get; set; }
        public string SoAddress { get; set; }
        public string SoName { get; set; }
        public string SoPhone { get; set; }
        public int SoDistrict { get; set; }
        public int SoWard { get; set; }
        public int SoCity { get; set; }
        public long SoCod { get; set; }
        public long SoFee { get; set; }
        public string SoNote { get; set; }
        public string SoItemContent { get; set; }
        public DateTime SoCTime { get; set; }
        public DateTime SoUTime { get; set; }
        public int SoShipperId { get; set; }
        public int SoStatus { get; set; }
        public int SoPriority { get; set; }
        public int SoSourceType { get; set; }
        public int SoCommissionType { get; set; }
        public int SoCommissionValue { get; set; }
        public int PickupStoreId { get; set; }
        public long PaymentReceipt { get; set; }
        public DateTime ExpectedDeliveryTime { get; set; }
        public DateTime ReceivingTime { get; set; }
        public DateTime DeliveredTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime PackedTime { get; set; }    
        public int PartnerOrderId { get; set; }
        public string Properties { get; set; }

        public List<ShippingOrder> ParseFromDataTable(DataTable dt)
        {
            List<ShippingOrder> shippingOrderList = new List<ShippingOrder>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                ShippingOrder shippingOrder = new ShippingOrder
                {
                    SoId = GetLongValue(row, "so_id"),
                    SoShipperId = GetIntValue(row, "so_shipper_id"),
                    SoPackedCode = GetStringValue(row, "so_packed_code"),
                    SoStatus = GetIntValue(row, "so_status"),
                    SoOrderCode = GetStringValue(row, "so_order_code"),
                    SoCustomerCode = GetStringValue(row, "so_customer_code"),
                    PartnerOrderId = GetIntValue(row, "partner_order_id"),
                };

                shippingOrderList.Add(shippingOrder);
            }
            return shippingOrderList;
        }

        private long GetLongValue(DataRow row, string key)
        {
            try
            {
                long result = Convert.ToInt64((row[key] == DBNull.Value) ? 0 : row[key]);
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private int GetIntValue(DataRow row, string key)
        {
            try
            {
                int result = Convert.ToInt32((row[key] == DBNull.Value) ? 0 : row[key]);
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private string GetStringValue(DataRow row, string key)
        {
            return row[key].ToString();
        }

        private float GetFloatValue(DataRow row, string key)
        {
            try
            {
                float result = Convert.ToSingle((row[key] == DBNull.Value) ? 0 : row[key]);
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }

}

