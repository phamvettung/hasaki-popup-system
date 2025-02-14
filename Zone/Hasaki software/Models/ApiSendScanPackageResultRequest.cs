using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intech_software.Models
{
    public class ApiSendScanPackageResultRequest
    {
        public string PackageCode { get; set; }
        public string Email { get; set; }
        public int LocationId { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }
        public float Weight { get; set; }
    }
}
