using Intech_software.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intech_software.DTO
{
    internal class ScannedOrder
    {
        public long Id { get; set; }
        public DateTime DateScanned { get; set; }
        public string SoOrderCode { get; set; }
        public string SoPackedCode { get; set; }
        public string SoCustomerCode { get; set; }
        public int ShippingUnitId { get; set; }
        public string ShippingUnitName { get; set; }
        public int Gateway { get; set; }
        public string Weight { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string Note { get; set; }
        public long SoId { get; set; }
        public int SessionId { get; set; }

        // custom properties
        public string Date { get; set; }
        public string Time { get; set; }
        public int STT { get; set; }
        public string NotFoundCode { get; set; }

        public List<ScannedOrder> ParseFromDataTable(DataTable dt)
        {
            List<ScannedOrder> scannedOrderList = new List<ScannedOrder>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                ScannedOrder scannedOrder = new ScannedOrder
                {
                    STT = dt.Rows.Count - i,
                    SoId = Helper.GetLongValue(row, "so_id"),
                    Id = Helper.GetLongValue(row, "id"),
                    ShippingUnitId = Helper.GetIntValue(row, "so_shipper_id"),
                    Gateway = Helper.GetIntValue(row, "gateway"),
                    Weight = Helper.GetStringValue(row, "weight"),
                    Length = Helper.GetStringValue(row, "length"),
                    Width = Helper.GetStringValue(row, "width"),
                    Height = Helper.GetStringValue(row, "height"),
                    StatusId = Helper.GetIntValue(row, "status"),
                    Note = Helper.GetStringValue(row, "note"),
                    SoOrderCode = Helper.GetStringValue(row, "so_order_code"),
                    SoPackedCode = Helper.GetStringValue(row, "so_packed_code"),
                    SoCustomerCode = Helper.GetStringValue(row, "so_customer_code"),
                    DateScanned = Helper.GetDateTimeValue(row, "scanned_at")
                };
                string nfCode = Helper.GetStringValue(row, "not_found_code");
                if ((string.IsNullOrEmpty(scannedOrder.SoCustomerCode) || scannedOrder.SoCustomerCode == "N/A") && !string.IsNullOrEmpty(nfCode))
                {
                    scannedOrder.SoCustomerCode = nfCode;
                }
                scannedOrder.ShippingUnitName = ShippingUnit.GetShippingUnitName(scannedOrder.ShippingUnitId);
                scannedOrder.Date = scannedOrder.DateScanned.ToString("yyyy-MM-dd");
                scannedOrder.Time = scannedOrder.DateScanned.ToString("HH:mm:ss");
                switch (scannedOrder.StatusId)
                {
                    case 1:
                        scannedOrder.StatusName = "True";
                        break;
                    case 2:
                        scannedOrder.StatusName = "False";
                        break;
                    case 3:
                        scannedOrder.StatusName = "Failed Add";
                        break;
                    case 4:
                        scannedOrder.StatusName = "Success add";
                        break;
                    default:
                        scannedOrder.StatusName = "False";
                        break;
                }
                //scannedOrder.StatusName = scannedOrder.StatusId == 1 ? "True" : "False";

                scannedOrderList.Add(scannedOrder);
            }
            return scannedOrderList;
        }
    }
}
