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
    [Route("api/v1/products/[controller]")]
    public class RemoveProductController : ControllerBase
    {
        [HttpDelete("{productID}")]
        public IActionResult RemoveProduct(int productID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = " delete from Products " +
                                  " where ProductID = @productID";

                cmd.Parameters.AddWithValue("@productID", productID);

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);

                    return Ok("Product with ID [ "+productID+"] was removed successfully!");



                }



            }
            catch (SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }
    }
}
