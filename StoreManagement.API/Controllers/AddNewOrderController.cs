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
    [Route("api/v1/orders/[controller]")]
    public class AddNewOrderController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddOrder([FromBody] Orders order)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO Orders(ProductID,StatusID,StoreID,CreateDate,ModifyDate,Quantity) " +
                                 "VALUES(@productID, 1, @storeID, GETUTCDATE(), GETUTCDATE(),@quantity);";
                // the storeID should not be passed like a parameter.
                // should be passed by which storeID has the product from the order


                cmd.Parameters.AddWithValue("@productID", order.ProductID);
                cmd.Parameters.AddWithValue("@storeID", order.StoreID);
                cmd.Parameters.AddWithValue("@quantity",order.Quantity);

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);
                    return Ok("Order added successfully");
                }



            }
            catch (SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }
    }
}
