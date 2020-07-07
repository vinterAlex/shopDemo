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
    [Route("api/v1/store/list")]
    public class GetStoreController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStore()
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = "select *" +
                    "from Store";

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);

                    List<StoreInformation> storeList = new List<StoreInformation>();

                    foreach(DataRow row in dt.Rows)
                    {
                        StoreInformation storeInfo = new StoreInformation();

                        storeInfo.StoreID = Convert.ToInt32(row["StoreID"]);
                        storeInfo.Name = Convert.ToString(row["Name"]);
                        storeInfo.Phone = Convert.ToString(row["Phone"]);
                        storeInfo.Street = Convert.ToString(row["Street"]);
                        storeInfo.City = Convert.ToString(row["City"]);
                        storeInfo.CountryID = Convert.ToInt32(row["CountryID"]);
                        storeInfo.Active = Convert.ToBoolean(row["Active"]);
                        storeInfo.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                        storeInfo.ModifyDate = Convert.ToDateTime(row["ModifyDate"]);

                        storeList.Add(storeInfo);                           
                    }

                    return Ok(storeList);


                }

            }
            catch (SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }



        [HttpGet("{storeID}")]
        public IActionResult GetStore(int storeID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = "select s.StoreID,s.Name[StoreName],s.Phone,s.Street,s.City,c.Name[Country],s.Active,s.CreateDate,s.ModifyDate " +
                    "from Store s " +
                    "   inner join Country c " +
                    "       on c.CountryID = s.CountryID " +
                    "where s.StoreID = @storeID";

                cmd.Parameters.AddWithValue("@storeID", storeID);
                

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);

                    List<StoreInformationDetailed> storeList = new List<StoreInformationDetailed>();

                    foreach (DataRow row in dt.Rows)
                    {
                        StoreInformationDetailed storeInfo = new StoreInformationDetailed();

                        storeInfo.StoreID = Convert.ToInt32(row["StoreID"]);
                        storeInfo.StoreName = Convert.ToString(row["StoreName"]);
                        storeInfo.Phone = Convert.ToString(row["Phone"]);
                        storeInfo.Street = Convert.ToString(row["Street"]);
                        storeInfo.City = Convert.ToString(row["City"]);
                        storeInfo.Country = Convert.ToString(row["Country"]);
                        storeInfo.Active = Convert.ToBoolean(row["Active"]);
                        storeInfo.CreateDate = Convert.ToDateTime(row["CreateDate"]);
                        storeInfo.ModifyDate = Convert.ToDateTime(row["ModifyDate"]);

                        storeList.Add(storeInfo);
                    }

                    return Ok(storeList);


                }

            }
            catch (SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }

    }
}
