using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intech_software.DTO
{
    internal class Order
    {
        public int Id { get; set; }
        public DateTime DateScanned { get; set; }
        public string SaleOrderNumber { get; set; }
        public string OutboundOrderNumber { get; set; }
        public int OutboundOrderId { get; set; }
        public string PackingNumber { get; set; }
        public string ShippingUnitId { get; set; }
        public string ShippingUnitName { get; set; }
        public int Gateway { get; set; }
        public float Weight { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
