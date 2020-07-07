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
    [Route("api/v1/customers/list")]
    public class GetCustomersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCustomersList()
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = "select *" +
                    "from Customers";

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);

                    List<Customers> customersList = new List<Customers>();

                    foreach (DataRow row in dt.Rows)
                    {
                        Customers customers = new Customers();


                        customers.CustomerID = Convert.ToInt32(row["CustomerID"]);
                        customers.FirstName= Convert.ToString(row["FirstName"]);
                        customers.LastName= Convert.ToString(row["LastName"]);
                        customers.Email = Convert.ToString(row["Email"]);
                        customers.Phone = Convert.ToString(row["Phone"]);
                        customers.Active = Convert.ToBoolean(row["Active"]);
                        customers.City = Convert.ToString(row["City"]);
                        customers.CountryID = Convert.ToInt32(row["CountryID"]);
                        customers.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                        customers.ModifyDate = Convert.ToDateTime(row["ModifyDate"]);

                        customersList.Add(customers);

                    }

                    return Ok(customersList);

                }

            }
            catch (SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }




        [HttpGet("{customerID}")]
        public IActionResult GetCustomerByID(int customerID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = 
                "select c.CustomerID,c.FirstName,c.LastName,c.Email,c.Phone,c.Active,c.City,cc.Name[Country],c.CreateDate,c.ModifyDate " +
                "from Customers c " +
                    "inner join Country cc " +
                        "on cc.CountryID = c.CountryID " +
                "where c.CustomerID = @customerID";

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@customerID";
                param.Value = customerID;

                cmd.Parameters.Add(param);

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);

                    List<CustomerDetailed> customersList = new List<CustomerDetailed>();

                    foreach (DataRow row in dt.Rows)
                    {
                        CustomerDetailed cd = new CustomerDetailed();


                        cd.CustomerID = Convert.ToInt32(row["CustomerID"]);
                        cd.FirstName = Convert.ToString(row["FirstName"]);
                        cd.LastName = Convert.ToString(row["LastName"]);
                        cd.Email = Convert.ToString(row["Email"]);
                        cd.Phone = Convert.ToString(row["Phone"]);
                        cd.Active = Convert.ToBoolean(row["Active"]);
                        cd.City = Convert.ToString(row["City"]);
                        cd.Country = Convert.ToString(row["Country"]);
                        cd.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                        cd.ModifyDate = Convert.ToDateTime(row["ModifyDate"]);


                        customersList.Add(cd);

                    }

                    return Ok(customersList);

                }

            }
            catch (SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }
    }
}
