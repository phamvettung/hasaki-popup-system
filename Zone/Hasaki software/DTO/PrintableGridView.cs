using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intech_software.DTO
{
    internal class PrintableGridView
    {
        public int Id { get; set; }
        public int OrderCount { get; set; }
        public int FailedOrder { get; set; }
        public int PartnerOrderId { get; set; }
        public string PartnerOrderCode { get; set; }
        public int Status { get; set; }
    }
}
