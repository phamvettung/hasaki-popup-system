using Intech_software.DAL;
using Intech_software.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace Intech_software.BUS
{
    internal class HasakiSystemBus
    {
        ConnectionFactory conn = new ConnectionFactory();

        DataTable dt = new DataTable();
        CountPackedLable _modelCountPackedLable = new CountPackedLable();

        public DataTable GetTable(string maKienHang)
        {
            //Console.WriteLine("maKienHang=============", maKienHang);
            /*
             select hsy.updated_at as ngay, 
            maKienHang, 
            isnull(hst.packed_label_zone, 0) as maZone, 
            trangThai, 
            hsy.pickup_location_id, 
            hsy.receiver_location_id 
            from HasakiSystem hsy 
            left join NowLocation nl on nl.pickup_location_id = hsy.receiver_location_id 
            left join HasakiStore hst on nl.location_id = hst.location_id and hst.zone is not null and hst.store_name like '%SHOP%' 
            where maKienHang = '100042310210161' 
            order by hst.zone desc


            select hsy.updated_at as ngay, 
            maKienHang, 
            hst.packed_label_zone as maZone, 
            trangThai, 
            hsy.pickup_location_id, 
            hsy.receiver_location_id,
			 hst.store_name
            from HasakiSystem hsy 
            left join NowLocation nl on nl.pickup_location_id = hsy.receiver_location_id 
            left join HasakiStore hst on hst.store_stock_id = nl.inside_id
            where maKienHang = '100042408040002' and hst.packed_label_zone is not null and hst.packed_label_zone != ''
            order by hst.zone desc
            */
            try
            {
                dt = conn.GetData("select hsy.updated_at as ngay, " +
                    " maKienHang, " +
                    " hst.packed_label_zone as maZone, " +
                    " trangThai, " +
                    " hsy.pickup_location_id, " +
                    " hsy.receiver_location_id, " +
                    " hst.store_name " +
                    " from HasakiSystem hsy " +
                    " left join NowLocation nl on nl.pickup_location_id = hsy.receiver_location_id " +
                    " left join HasakiStore hst on hst.store_stock_id = nl.inside_id " +
                    " where maKienHang = '" + maKienHang + "' and hst.packed_label_zone is not null and hst.packed_label_zone != '' " +
                    " order by hst.zone desc");

                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }

                dt = conn.GetData("select top (2) hsy.updated_at as ngay, " +
                    " maKienHang, " +
                    " isnull(hst.packed_label_zone, 0) as maZone, " +
                    " trangThai, " +
                    " hsy.pickup_location_id, " +
                    " hsy.receiver_location_id, " +
                    " hst.store_name " +
                    " from HasakiSystem hsy " +
                    " left join NowLocation nl on nl.pickup_location_id = hsy.receiver_location_id " +
                    " left join HasakiStore hst on nl.location_id = hst.location_id and hst.zone is not null and hst.store_name like '%SHOP%' " +
                    " where maKienHang = '" + maKienHang + "' " +
                    " order by hst.zone desc");

                if (dt != null)
                {
                    if (dt.Rows.Count > 1)
                    {
                        if (dt.Rows[0]["store_name"].ToString() == "SHOP - EVENT")
                        {
                            dt = conn.GetData("select hsy.updated_at as ngay, " +
                                " maKienHang, " +
                                " isnull(hst.packed_label_zone, 0) as maZone, " +
                                " trangThai, " +
                                " hsy.pickup_location_id, " +
                                " hsy.receiver_location_id, " +
                                " hst.store_name " +
                                " from HasakiSystem hsy " +
                                " left join NowLocation nl on nl.pickup_location_id = hsy.receiver_location_id " +
                                " left join HasakiStore hst on nl.location_id = hst.location_id and hst.zone is not null and hst.store_name like '%SHOP%'  and  hst.store_name not like '%EVENT%' " +
                                " where maKienHang = '" + maKienHang + "' " +
                                " order by hst.zone desc");
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SetTable(string ngay, string maKienHang, string maZone, string trangThai)
        {
            try
            {
                if (conn.SetData("insert into HasakiSystem(ngay, maKienHang, maZone, trangThai) values ('" + ngay + "', '" + maKienHang + "', '" + maZone + "', '" + trangThai + "')"))
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckDate(string ngay)
        {
            try
            {
                if (conn.ReadData("select top 1 * from HasakiSystem where FORMAT(created_at, 'M/d/yyyy') = '" + ngay + "'"))
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckCode(string code)
        {
            try
            {
                if (conn.ReadData("select * from HasakiSystem where maKienHang = '" + code + "'"))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CountPackedLable> CountPackedLabelByUser(int userId)
        {
            /*
             
             select isnull(hst.packed_label_zone, 0) as zone, COUNT(distinct maKienHang) as num_packed_label
                from HasakiSystem hsy 
                left join NowLocation nl on nl.pickup_location_id = hsy.receiver_location_id 
                left join HasakiStore hst on nl.location_id = hst.location_id and hst.zone is not null and hst.store_name like '%SHOP%' 
                where trangThai= 20 and shipper_id = 208
                group by hst.packed_label_zone
                order by num_packed_label desc

             */

            string query = "select isnull(hst.packed_label_zone, 0) as zone, COUNT(distinct maKienHang) as num_packed_label" +
                " from HasakiSystem hsy " +
                " left join NowLocation nl on nl.pickup_location_id = hsy.receiver_location_id " +
                " left join HasakiStore hst on nl.location_id = hst.location_id and hst.zone is not null and hst.store_name like '%SHOP%' " +
                " where trangThai= 20 and shipper_id = " + userId +
                " group by hst.packed_label_zone " +
                " order by num_packed_label desc ";

            dt = conn.GetData(query);

            List<CountPackedLable> countPackedLables = _modelCountPackedLable.ParseFromDt(dt);
            return countPackedLables;
        }
    }
}
