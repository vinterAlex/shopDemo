using Microsoft.AspNetCore.Mvc;
using StoreManagement.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;


namespace StoreManagement.API.Controllers
{
    [ApiController]
    [Route("api/v1/orders/listdetailed")]
    public class GetOrdersDetailedController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetOrdersDetailedInfo()
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText =
                        "select o.OrderID,p.Name[ProductName],o.Quantity,os.Name[OrderStatus],s.Name[Store],o.Quantity,o.CreateDate,o.ModifyDate " +
                        "from Orders o " +
                        "    inner join Products p " +
                        "        on p.ProductID = o.ProductID " +
                        "    inner join OrderStatus os " +
                        "        on os.StatusID = o.StatusID " +
                        "    inner join Store s " +
                        "        on s.StoreID = o.StoreID " ;

                

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);

                    List<OrdersDetailed> orderInfoList = new List<OrdersDetailed>();

                    foreach (DataRow row in dt.Rows)
                    {
                        OrdersDetailed orders = new OrdersDetailed();

                        orders.OrderID = Convert.ToInt32(row["OrderID"]);
                        orders.ProductName = Convert.ToString(row["ProductName"]);
                        orders.Quantity = Convert.ToInt32(row["Quantity"]);
                        orders.OrderStatus = Convert.ToString(row["OrderStatus"]);
                        orders.Store = Convert.ToString(row["Store"]);
                        orders.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                        orders.ModifyDate = Convert.ToDateTime(row["ModifyDate"]);

                        orderInfoList.Add(orders);

                    }

                    return Ok(orderInfoList);
                }
            }
            catch (SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }





        [HttpGet("{orderID}")]
        public IActionResult GetOrdersDetailedInfo(int orderID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText =
                        "select o.OrderID,p.Name[ProductName],o.Quantity,os.Name[OrderStatus],s.Name[Store],o.CreateDate,o.ModifyDate " +
                        "from Orders o " +
                        "    inner join Products p " +
                        "        on p.ProductID = o.ProductID " +
                        "    inner join OrderStatus os " +
                        "        on os.StatusID = o.StatusID " +
                        "    inner join Store s " +
                        "        on s.StoreID = o.StoreID " +
                        "where o.OrderID = @orderID";

                cmd.Parameters.AddWithValue("@orderID", orderID);

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);

                    List<OrdersDetailed> orderInfoList = new List<OrdersDetailed>();

                    foreach (DataRow row in dt.Rows)
                    {
                        OrdersDetailed orders = new OrdersDetailed();

                        orders.OrderID = Convert.ToInt32(row["OrderID"]);
                        orders.ProductName = Convert.ToString(row["ProductName"]);
                        orders.Quantity = Convert.ToInt32(row["Quantity"]);
                        orders.OrderStatus = Convert.ToString(row["OrderStatus"]);
                        orders.Store = Convert.ToString(row["Store"]);
                        orders.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                        orders.ModifyDate = Convert.ToDateTime(row["ModifyDate"]);

                        orderInfoList.Add(orders);

                    }

                    return Ok(orderInfoList);
                }
            }
            catch (SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }
    }
}
