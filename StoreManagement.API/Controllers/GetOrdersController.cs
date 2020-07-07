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
    [Route("api/v1/orders/list")]
    public class GetOrdersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetOrders()
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = "select * from Orders";

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);

                    List<Orders> ordersList = new List<Orders>();

                    foreach(DataRow row in dt.Rows)
                    {
                        Orders orders = new Orders();

                        orders.OrderID = Convert.ToInt32(row["OrderID"]);
                        orders.ProductID = Convert.ToInt32(row["ProductID"]);
                        orders.StatusID = Convert.ToInt32(row["StatusID"]);
                        orders.StoreID = Convert.ToInt32(row["StoreID"]);
                        orders.Quantity = Convert.ToInt32(row["Quantity"]);
                        orders.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                        orders.ModifyDate = Convert.ToDateTime(row["ModifyDate"]);

                        ordersList.Add(orders);
                    }

                    return Ok(ordersList);
                }

            }
            catch (SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }




        [HttpGet("{orderID}")]
        public IActionResult GetOrdersList(int orderID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = "select * from Orders where OrderID = @orderID";

                cmd.Parameters.AddWithValue("@orderID", orderID);

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);

                    List<Orders> ordersList = new List<Orders>();

                    foreach (DataRow row in dt.Rows)
                    {
                        Orders orders = new Orders();

                        orders.OrderID = Convert.ToInt32(row["OrderID"]);
                        orders.ProductID = Convert.ToInt32(row["ProductID"]);
                        orders.StatusID = Convert.ToInt32(row["StatusID"]);
                        orders.StoreID = Convert.ToInt32(row["StoreID"]);
                        orders.Quantity = Convert.ToInt32(row["Quantity"]);
                        orders.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                        orders.ModifyDate = Convert.ToDateTime(row["ModifyDate"]);

                        ordersList.Add(orders);
                    }

                    return Ok(ordersList);
                }

            }
            catch (SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }



    }
}
