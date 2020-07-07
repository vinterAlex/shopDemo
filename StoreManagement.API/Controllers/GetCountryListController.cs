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
    [Route("api/v1/country/list")]
    public class GetCountryListController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCountryList()
        {
            try
            {
                SqlConnection conn = new SqlConnection(StoreManagement.Classes.DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = "select *" +
                    "from Country";

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);

                    List<Country> countryList = new List<Country>();

                    foreach (DataRow row in dt.Rows)
                    {
                        Country country = new Country();

                        country.CountryID = Convert.ToInt32(row["CountryID"]);
                        country.CountryName = Convert.ToString(row["Name"]);
                        country.CreateDate = Convert.ToDateTime(row["CreateDate"]);

                        countryList.Add(country);

                    }

                    return Ok(countryList);

                }

            }
            catch(SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }



        [HttpGet("{countryID}")]
        
        public IActionResult GetCountryByID(int countryID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(StoreManagement.Classes.DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = "select * " +
                    "from Country " +
                    "where CountryID = @countryID;";

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@countryID";
                param.Value = countryID;

                cmd.Parameters.Add(param);

                conn.Open();

                DataTable dt = new DataTable();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);

                    List<Country> countryList = new List<Country>();

                    foreach (DataRow row in dt.Rows)
                    {
                        Country country = new Country();

                        country.CountryID = Convert.ToInt32(row["CountryID"]);
                        country.CountryName = Convert.ToString(row["Name"]);
                        country.CreateDate = Convert.ToDateTime(row["CreateDate"]);

                        countryList.Add(country);

                    }

                    return Ok(countryList);

                }

            }
            catch (SqlException sqlEx)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, sqlEx);
            }
        }








    }
}
