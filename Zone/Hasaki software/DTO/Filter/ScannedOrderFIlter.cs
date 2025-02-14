using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intech_software.DTO.Filter
{
    internal class ScannedOrderFIlter
    {
        public string Code { get; set; }
        public int ShippingUnit { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public bool IsFilterDate { get; set; }
    }
}
