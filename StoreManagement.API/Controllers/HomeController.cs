using Microsoft.AspNetCore.Mvc;


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StoreManagement.API.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Home()
        {

            return Ok("Welcome to Home Page");

        }
    }
}
