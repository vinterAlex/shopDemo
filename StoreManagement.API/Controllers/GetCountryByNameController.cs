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
    [Route("api/v1/country/[controller]")]
    public class GetCountryByNameController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCountryByName(string countryName)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBConnection.connectionString);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandText = "select * " +
                    "from Country " +
                    "where Name = @countryName;";

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@countryName";
                param.Value = countryName;

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
