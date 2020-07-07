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
    [Route("api/v1/store/[controller]/{storeID}")]
    public class UpdateStoreActivityController : ControllerBase
    {
        [HttpPut]
        public IActionResult UpdateStore(int storeID,[FromBody] StoreInformation storeItem)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = "update Store " +
                    "set Active = @trueFalse " +
                    "where StoreID = @storeID;";

                cmd.Parameters.AddWithValue("@storeID", storeID);
                cmd.Parameters.AddWithValue("@trueFalse", storeItem.Active);

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);

                    
                    
                    return Ok("Store with ID ["+storeID+"] availability was changed!");
                }

            }
            catch (SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }
    }
}
