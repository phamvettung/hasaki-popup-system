using Intech_software.Interface;
using System;
using System.Text.Json;

namespace Intech_software.Models
{
    internal class Inbound : IKafkaMessage
    {
        public DateTime CreatedAt { get; set; }
        public string PackageCode { get; set; }
        public string Weight { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Zone { get; set; }
        public string Status { get; set; }
        public string UpdatedBy { get; set; }
        public int UserId { get; set; }
        public string SupportUsers { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public int ScanStatus { get; set; }
        public int KafkaStatus { get; set; } = 0;
        override public string ToString()
        {
            return JsonSerializer.Serialize(new
            {
                scanned_date = CreatedAt.ToString("yyyy-MM-dd HH:mm"),
                package_label = PackageCode,
                status_id = "1",
                note = Note,
                user_id = UserId,
                support_users = SupportUsers,
            });
        }
    }
}
