using Intech_software.DAL;
using Intech_software.DTO;
using Intech_software.DTO.Filter;
using System;
using System.Collections.Generic;

namespace Intech_software.BUS
{
    internal class ScannedOrderBus
    {
        readonly ConnectionFactory _conn = new ConnectionFactory();
        readonly ScannedOrder _scannedOrder = new ScannedOrder();

        public bool Create(ScannedOrder order)
        {
            try
            {
                string query = "insert into ScannedOrders ([gateway], [weight], [length], [width], [height], [status], [note], [scanned_at], [so_id], [not_found_code], [session_id] )" +
                    "values (" + order.Gateway + ", '" + order.Weight + "', '" + order.Length + "', '" + order.Width + "', '" + order.Height + "', " + order.StatusId + ", N'" + order.Note + "', GETDATE(), " + order.SoId + ", '" + order.NotFoundCode + "', " + order.SessionId + ")";
                return _conn.SetData(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ScannedOrder> GetTable()
        {
            try
            {
                /*
                 select scno.*, hso.so_order_code, ISNULL(hso.so_customer_code, mtc.tracking_code) as so_customer_code, hso.so_packed_code, hso.so_shipper_id 
                    from ScannedOrders scno 
                    join HasakiShippingOrder hso on scno.so_id = hso.so_id 
                    left join MapTrackingCode mtc on mtc.sales_order_number = hso.so_order_code
                    where scno.hidden != 1 order by scno.id desc
                 */

                var dt = _conn.GetData(
                    " select scno.*, hso.so_order_code, ISNULL(hso.so_customer_code, mtc.tracking_code) as so_customer_code, hso.so_packed_code, hso.so_shipper_id " +
                    " from ScannedOrders scno " +
                    " join HasakiShippingOrder hso on scno.so_id = hso.so_id " +
                    " left join MapTrackingCode mtc on mtc.sales_order_number = hso.so_order_code" +
                    " where scno.hidden != 1 order by scno.id desc");
                List<ScannedOrder> orders = _scannedOrder.ParseFromDataTable(dt);
                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool HiddenAll()
        {
            try
            {
                return _conn.SetData("update ScannedOrders set hidden = 1");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool HideOne(long id)
        {
            try
            {
                return _conn.SetData("update ScannedOrders set hidden = 1 where id = " + id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool HideInGate(int gate)
        {
            try
            {
                return _conn.SetData("update ScannedOrders set hidden = 1 where gateway = " + gate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ScannedOrder> GetTableWithFilter(ScannedOrderFIlter filter)
        {
            try
            {
                string queryStr = " select scno.*, hso.so_order_code, hso.so_customer_code, hso.so_packed_code, hso.so_shipper_id " +
                    " from ScannedOrders scno " +
                    " join HasakiShippingOrder hso on scno.so_id = hso.so_id where 1 = 1 ";
                if (!string.IsNullOrEmpty(filter.Code))
                {
                    queryStr += " and (CAST(hso.so_order_code as nvarchar) = '" + filter.Code + "' or hso.so_customer_code = '" + filter.Code + "' or (CAST(hso.so_packed_code as nvarchar) = '" + filter.Code + "')";
                }
                if (filter.ShippingUnit > 0)
                {
                    queryStr += " and hso.so_shipper_id = " + filter.ShippingUnit;
                }
                if (filter.Status > 0)
                {
                    queryStr += " and scno.status = " + filter.Status;
                }
                if (filter.IsFilterDate)
                {
                    queryStr += " and CONVERT(date, scno.scanned_at) = '" + filter.Date.ToString("yyyy-MM-dd") + "'";
                }

                Console.WriteLine(queryStr);
                var dt = _conn.GetData(queryStr);

                List<ScannedOrder> orders = _scannedOrder.ParseFromDataTable(dt);
                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateScannedOrderProperties(ScannedOrder order)
        {
            try
            {
                if (order.SoId > 0)
                {
                    var query = "UPDATE ScannedOrders " +
                        " SET weight = '" + order.Weight + "', " +
                        " width = '" + order.Width + "', " +
                        " length = '" + order.Length + "', " +
                        " height = '" + order.Height + "' " +
                        " WHERE id = (select top(1)  so.id from ScannedOrders so where so_id = " + order.SoId + " order by so.id desc)";
                    return _conn.SetData(query);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ScannedOrder> FindBySoId(long soId)
        {
            try
            {
                /*
                 select scno.*, hso.so_order_code, hso.so_customer_code, hso.so_packed_code, hso.so_shipper_id
                    from ScannedOrders scno 
                    join HasakiShippingOrder hso on scno.so_id = hso.so_id where scno.so_id = 5 order by scno.id desc
                 */

                var dt = _conn.GetData(
                    " select scno.*, hso.so_order_code, hso.so_customer_code, hso.so_packed_code, hso.so_shipper_id " +
                    " from ScannedOrders scno " +
                    " join HasakiShippingOrder hso on scno.so_id = hso.so_id where scno.so_id = " + soId + " and scno.hidden != 1 order by scno.id desc");
                List<ScannedOrder> orders = _scannedOrder.ParseFromDataTable(dt);
                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ScannedOrder> FindByCustomerCode(long soId)
        {
            try
            {
                var dt = _conn.GetData(
                    " select scno.*, hso.so_order_code, hso.so_customer_code, hso.so_packed_code, hso.so_shipper_id " +
                    " from ScannedOrders scno " +
                    " join HasakiShippingOrder hso on scno.so_id = hso.so_id where scno.id = " + soId + "order by scno.id desc");
                List<ScannedOrder> orders = _scannedOrder.ParseFromDataTable(dt);
                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ScannedOrder> GetScannedOrdersByGate(int gate)
        {
            try
            {
                /*
                 select scno.*, hso.so_order_code, ISNULL(hso.so_customer_code, mtc.tracking_code) as so_customer_code, hso.so_packed_code, hso.so_shipper_id 
                from ScannedOrders scno 
                join HasakiShippingOrder hso on scno.so_id = hso.so_id 
                left join MapTrackingCode mtc on mtc.sales_order_number = hso.so_order_code
                where scno.hidden != 1 and scno.gateway = 2 and scno.status = 1 order by scno.id desc

                -- not hidden, status true
                 */

                var dt = _conn.GetData(
                    " select scno.*, hso.so_order_code, ISNULL(hso.so_customer_code, mtc.tracking_code) as so_customer_code, hso.so_packed_code, hso.so_shipper_id " +
                    " from ScannedOrders scno " +
                    " join HasakiShippingOrder hso on scno.so_id = hso.so_id " +
                    " left join MapTrackingCode mtc on mtc.sales_order_number = hso.so_order_code " +
                    " where scno.hidden != 1 and scno.status = 1 and scno.gateway = " + gate + " order by scno.id desc");
                List<ScannedOrder> orders = _scannedOrder.ParseFromDataTable(dt);
                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ScannedOrder> GetScannedOrders()
        {
            try
            {
                /*
                 select scno.*, hso.so_order_code, ISNULL(hso.so_customer_code, mtc.tracking_code) as so_customer_code, hso.so_packed_code, hso.so_shipper_id 
                from ScannedOrders scno 
                join HasakiShippingOrder hso on scno.so_id = hso.so_id 
                left join MapTrackingCode mtc on mtc.sales_order_number = hso.so_order_code
                where scno.hidden != 1 and scno.gateway = 2 and scno.status = 1 order by scno.id desc

                -- not hidden, status true
                 */

                var dt = _conn.GetData(
                    " select scno.*, hso.so_order_code, ISNULL(hso.so_customer_code, mtc.tracking_code) as so_customer_code, hso.so_packed_code, hso.so_shipper_id " +
                    " from ScannedOrders scno " +
                    " join HasakiShippingOrder hso on scno.so_id = hso.so_id " +
                    " left join MapTrackingCode mtc on mtc.sales_order_number = hso.so_order_code " +
                    " where scno.hidden != 1 and scno.status = 1 and scno.gateway = order by scno.id desc");
                List<ScannedOrder> orders = _scannedOrder.ParseFromDataTable(dt);
                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateStatusBySoId(long soId, int status)
        {
            try
            {
                /*
                 update ScannedOrders 
                set status = 3
                where so_id = 14771350
                 */

                var res = _conn.SetData(
                    " update ScannedOrders " +
                    " set status = " + status +
                    " where so_id =  " + soId);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PrintableGridView> GetPrintableGroup()
        {

            return new List<PrintableGridView>();
        }
    }
}
