using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intech_software.DTO
{
    public static class Helper
    {
        public static long GetLongValue(DataRow row, string key)
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

        public static int GetIntValue(DataRow row, string key)
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

        public static string GetStringValue(DataRow row, string key)
        {
            return row[key].ToString();
        }

        public static float GetFloatValue(DataRow row, string key)
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

        public static DateTime GetDateTimeValue(DataRow row, string key)
        {
            try
            {
                return Convert.ToDateTime(row[key]);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
    }
}
