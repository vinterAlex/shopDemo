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
    [Route("api/v1/product/[controller]")]
    public class AddProductController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddNewProduct([FromBody]Products productItem)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO Products(Name,Price,CreateDate,ModifyDate,InStock) " +
                                    "VALUES(@Name, @Price, GETUTCDATE(),GETUTCDATE(),1)";

                cmd.Parameters.AddWithValue("@Name", productItem.ProductName);
                cmd.Parameters.AddWithValue("@Price", productItem.Price);
                cmd.Parameters.AddWithValue("@InStock", productItem.InStock);
                

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);

                    return Ok("Product added succesfully!");
                }

            }
            catch (SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }
    }
}
