using Intech_software.DAL;
using Intech_software.DTO;
using System;
using System.Collections.Generic;

namespace Intech_software.BUS
{
    internal class ShippingOrderBus
    {
        readonly ConnectionFactory _conn = new ConnectionFactory();
        readonly ShippingOrder _shippingOrder = new ShippingOrder();

        public List<ShippingOrder> GetBySocode(string soCode)
        {
            try
            {
                /*
                    select hso.so_id, hso.so_order_code, hso.so_packed_code, isnull(hso.so_customer_code, mtc.tracking_code) as so_customer_code, hso.so_status, hso.so_shipper_id, hso.partner_order_id
                    from HasakiShippingOrder hso 
                    left join MapTrackingCode mtc on hso.so_order_code = mtc.sales_order_number
                    where hso.so_customer_code = '1KKQ6RUY' or mtc.tracking_code = '1KKQ6RUY' order by hso.so_id desc
                 */
                string query = "select hso.so_id, hso.so_order_code, hso.so_packed_code, isnull(hso.so_customer_code, mtc.tracking_code) as so_customer_code, hso.so_status, hso.so_shipper_id, hso.partner_order_id" +
                    " from HasakiShippingOrder hso  " +
                    " left join MapTrackingCode mtc on hso.so_order_code = mtc.sales_order_number " +
                    " where hso.so_customer_code = '" + soCode + "' or mtc.tracking_code = '" + soCode + "'  order by hso.so_id desc";
                Console.WriteLine(query);
                var dt = _conn.GetData(query);
                List<ShippingOrder> orders = _shippingOrder.ParseFromDataTable(dt);
                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
