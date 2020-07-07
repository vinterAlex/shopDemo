using Microsoft.AspNetCore.Mvc;
using StoreManagement.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text.RegularExpressions;

namespace StoreManagement.API.Controllers
{
    [ApiController]
    [Route("api/v1/product/[controller]/{productID}")]
    public class UpdateProductController : ControllerBase
    {
        [HttpPut]
        public IActionResult UpdateProduct(int productID,[FromBody]Products productItem)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = "update Products " +
                        "set Name = @Name, Price = @Price, ModifyDate = GETUTCDATE(),InStock = @InStock " +
                        "where ProductID = @productID";


                cmd.Parameters.AddWithValue("@productID", productID);

                cmd.Parameters.AddWithValue("@Name", productItem.ProductName);
                cmd.Parameters.AddWithValue("@Price", productItem.Price);
                cmd.Parameters.AddWithValue("@InStock", productItem.InStock);

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);

                    return Ok("Product with ID ["+productID+"] updated successfully");
                }




            }
            catch (SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }
    }
}
