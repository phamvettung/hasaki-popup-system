using Intech_software.DAL;
using Intech_software.DTO;
using System;
using System.Collections.Generic;

namespace Intech_software.BUS
{
    internal class PrintableSessionBus
    {
        readonly ConnectionFactory _conn = new ConnectionFactory();
        readonly PrintableSession _printableSession = new PrintableSession();

        public PrintableSession GetTodayMaxSession(int shipperId)
        {
            string query = "select top 1 * from PrintableSession where created_date = cast(GETDATE() as date) and shipper_id = " + shipperId ?? 0 + " and status = 1 order by session_id desc";
            var dt = _conn.GetData(query);
            if (dt.Rows.Count > 0)
            {
                return _printableSession.ParseOneFromDataTable(dt);
            }

            return null;
        }

        public string GetTodayMaxSessionCode(int shipperId)
        {
            string query = "select top 1 * from PrintableSession where created_date = cast(GETDATE() as date) and shipper_id = " + shipperId ?? 0 + " order by session_id desc";
            var dt = _conn.GetData(query);
            if (dt.Rows.Count > 0)
            {
                return DTO.Helper.GetStringValue(dt.Rows[0], "session_code");
            }

            return null;
        }

        public PrintableSession CreateNewSession(int shipperId)
        {
            try
            {
                var oldSession = GetTodayMaxSession(shipperId);
                string newSessionCode = string.Empty;
                if (oldSession == null)
                {
                    newSessionCode = string.Format("{0}{1}0001", DateTime.Now.ToString("yyMMdd"), shipperId.ToString().PadLeft(3, '0'));

                }
                else
                {
                    long.TryParse(oldSession.SessionCode, out long numOldSession);
                    numOldSession += 1;
                    newSessionCode = numOldSession.ToString();
                }
                string query = "insert into PrintableSession (session_code, created_date, status, shipper_id) values ('" + newSessionCode + "', GETDATE(), 1," + shipperId + ")";
                _ = _conn.SetData(query);
                var res = GetByCode(newSessionCode);

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PrintableSession GetByCode(string code)
        {
            try
            {
                var dt = _conn.GetData("select * from PrintableSession where session_code = '" + code + "'");
                var res = _printableSession.ParseOneFromDataTable(dt);
                return res;
            }
            catch
            {
                return new PrintableSession() { SessionId = 0 };
            }
        }

        public bool CompleteAllSession()
        {
            try
            {
                string query = "update PrintableSession set status = 2 where status = 1)";
                return _conn.SetData(query);
            }
            catch
            {
                return false;
            }
        }

        public List<PrintableSession> GetTable()
        {
            /*
             select  ps.session_id, ps.session_code, count(distinct sco.so_id) as total_order, count(case when sco.status = 3 then 1 end) as fail_order, 
            ps.status, ps.partner_code, ps.partner_id , ps.shipper_id
            from PrintableSession ps join ScannedOrders sco on ps.session_id = sco.session_id 
            group by ps.session_id, ps.session_code, ps.status, ps.partner_code, ps.partner_id, ps.shipper_id

             */

            try
            {
                string query = "select  ps.session_id, ps.session_code, count(distinct sco.so_id) as total_order, count(case when sco.status = 3 then 1 end) as fail_order, " +
                    " ps.status, ps.partner_code, ps.partner_id , ps.shipper_id " +
                    " from PrintableSession ps join ScannedOrders sco on ps.session_id = sco.session_id " +
                    " group by ps.session_id, ps.session_code, ps.status, ps.partner_code, ps.partner_id, ps.shipper_id ";
                var dt = _conn.GetData(query);
                return _printableSession.ParseFromDataTable(dt);
            }
            catch
            {
                return null;
            }
        }
    }
}
