using System;

namespace Intech_software.Helper
{
    internal class SwitchZone
    {
        public int GetZoneSelection(string maKienHang)
        {
            try
            {
                BUS.HasakiSystemBus hasakiSystemBus = new BUS.HasakiSystemBus();
                System.Data.DataTable dt = hasakiSystemBus.GetTable(maKienHang);
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0]["maZone"]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
