using Intech_software.DAL;
using System;
using System.Data;

namespace Intech_software.BUS
{
    internal class InBoundsBus
    {
        ConnectionFactory connectionFactory = new ConnectionFactory();

        DataTable dt = new DataTable();
        public DataTable GetTable()
        {
            try
            {
                dt = connectionFactory.GetData("select row_number() over(order by thoiGian) as STT," +
                    "ngay," +
                    "thoiGian," +
                    "maKienHang," +
                    "khoiLuong," +
                    "chieuDai," +
                    "chieuRong," +
                    "chieuCao," +
                    "maZone," +
                    "trangThai," +
                    "tenTK  from InBounds");

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SetTable(string ngay, string thoiGian, string maKienHang, string khoiLuong, string chieuDai, string chieuRong, string chieuCao, string maZone, string trangThai, string tenTK)
        {
            try
            {
                var query = "insert into InBounds (ngay,thoiGian,maKienHang,khoiLuong,chieuDai,chieuRong,chieuCao,maZone,trangThai,tenTK) " +
                    "values('" + ngay + "', '" + thoiGian + "', '" + maKienHang + "', '" + khoiLuong + "', '" + chieuDai + "', '" + chieuRong + "', '" + chieuCao + "', '" + maZone + "', '" + trangThai + "' , '" + tenTK + "')";
                Console.WriteLine(query);
                if (connectionFactory.SetData(query))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable Search(string code)
        {
            try
            {
                dt = connectionFactory.GetData("select row_number() over(order by thoiGian) as STT," +
                    " ngay," +
                    " thoiGian," +
                    " maKienHang," +
                    "khoiLuong," +
                    "chieuDai," +
                    "chieuRong," +
                    "chieuCao," +
                    "maZone," +
                    "trangThai," +
                    "tenTK from InBounds where maKienHang = '" + code + "'");
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable FindByPackageCode(string packageCode)
        {
            try
            {
                dt = connectionFactory.GetData("select row_number() over(order by thoiGian) as STT," +
                    " ngay," +
                    " thoiGian," +
                    " maKienHang," +
                    "khoiLuong," +
                    "chieuDai," +
                    "chieuRong," +
                    "chieuCao," +
                    "maZone," +
                    "trangThai," +
                    "tenTK from InBounds where trangThai = 1 and maKienHang = '" + packageCode + "'");
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetTableDate(string date)
        {
            try
            {
                dt = connectionFactory.GetData("select row_number() over(order by thoiGian) as STT," +
                    "ngay," +
                    "thoiGian," +
                    "maKienHang," +
                    "khoiLuong," +
                    "chieuDai," +
                    "chieuRong," +
                    "chieuCao," +
                    "maZone," +
                    "trangThai," +
                    "tenTK  from InBounds where ngay = '" + date + "'");

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteAllData()
        {
            try
            {
                var res = connectionFactory.SetData("truncate table InBounds");
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SetErrorCode(string packageCode, string errorMessage)
        {
            try
            {
                var res = connectionFactory.SetData("update InBounds set ErrorMessage = '" + errorMessage + "' where maKienHang = '" + packageCode + "'");
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
