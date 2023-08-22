using meli.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace meli.Controllers
{
    //[Route("{controller=Home}/{action=Index}/{id?}")]
    //[ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        // GET api/values/5
        [HttpGet("{url}")]
        public ActionResult<string> Get(string url)
        {
            
            string result = (url.Length > 6) ? "Es url larga" : "Es url corta";
            
            //return result;
            
            try
            {
                int longitudToken = Convert.ToInt32(_configuration.GetSection("LongitudToken").Value);
                if (url.Length <= longitudToken) //si no es una url larga entra
                {
                    DataTable dataTable = new DataTable();
                
                    string urlLarga = "";
                    string cnnstring = _configuration.GetConnectionString("conexionActiva");

                    using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("conexionActiva")))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = new SqlCommand("obtener_url_larga", connection);
                        adapter.SelectCommand.Parameters.Add(new SqlParameter() { ParameterName = "tokenUrlCorta", Value = url });

                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        adapter.Fill(dataTable);
                    }

                    if (dataTable.Rows.Count == 0)
                        return Json(new { success = false, message = "No se encontró la url",URL = _configuration.GetSection("HostName").Value+url, });


                    foreach (DataRow row in dataTable.Rows)
                    {
                        urlLarga = Convert.ToString(row["UrlLarga"]);
                        
                    }
                    return Redirect(urlLarga);
                }
                else
                {
                    return Redirect("http://" + url);
                }

            }
            
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        
        
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
