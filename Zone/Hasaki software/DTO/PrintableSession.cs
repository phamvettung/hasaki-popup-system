using Intech_software.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intech_software.DTO
{
    internal class PrintableSession
    {
        public int SessionId { get; set; }
        public string SessionCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
        public int ShipperId { get; set; }
        public int PartnerId { get; set; }
        public string PartnerCode { get; set; }

        public PrintableSession ParseOneFromDataTable(DataTable dt)
        {
            if (dt.Rows.Count == 0) { return null; }

            return new PrintableSession()
            {
                SessionId = Helper.GetIntValue(dt.Rows[0], "session_id"),
                SessionCode = Helper.GetStringValue(dt.Rows[0], "session_code"),
                Status = Helper.GetIntValue(dt.Rows[0], "status"),
                ShipperId = Helper.GetIntValue(dt.Rows[0], "shipper_id"),
            };
        }

        public List<PrintableSession> ParseFromDataTable(DataTable dt) 
        {
            List<PrintableSession> listPrinatableSession = new List<PrintableSession>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                PrintableSession printableSession = new PrintableSession
                {
                    SessionId = Helper.GetIntValue(row, "session_id"),
                    SessionCode = Helper.GetStringValue(row, "session_code"),
                    Status = Helper.GetIntValue(dt.Rows[0], "status"),
                    ShipperId = Helper.GetIntValue(dt.Rows[0], "shipper_id"),
                    PartnerCode = Helper.GetStringValue(row, "partner_code"),
                };
                

                listPrinatableSession.Add(printableSession);
            }
            return listPrinatableSession;
        }

    }
}
