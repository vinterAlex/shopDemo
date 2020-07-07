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
    public class GetProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                SqlConnection conn = new SqlConnection(StoreManagement.Classes.DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = "select *" +
                    "from Products";

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);

                    List<Products> productList = new List<Products>();

                    foreach(DataRow row in dt.Rows)
                    {
                        Products products = new Products();

                        products.ProductID = Convert.ToInt32(row["ProductID"]);
                        products.ProductName = Convert.ToString(row["Name"]);
                        products.Price = Convert.ToString(row["Price"]);
                        products.InStock = Convert.ToBoolean(row["InStock"]);
                        products.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                        products.ModifyDate = Convert.ToDateTime(row["ModifyDate"]);

                        productList.Add(products);

                    }

                    return Ok(productList);

                }



            }
            catch (SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }
    }
}
