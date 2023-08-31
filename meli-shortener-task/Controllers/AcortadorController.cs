using meli.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meli.Functions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using System.IO;

namespace meli.Controllers
{
    [Route("{controller=Home}/{action=Index}/{id?}")]

    public class AcortadorController : Controller
    {
        private readonly ConnectionDbContextClass _db;
        private readonly IConfiguration _configuration;

        public AcortadorController(ConnectionDbContextClass db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        // GET: AcortadorController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<UrlConfigClass> lst = new List<UrlConfigClass>();
            
                lst =  await  (from d in _db.UrlConfigs.OrderByDescending(x => x.ID).Take(20)
                    select new UrlConfigClass
                    {
                        ID = d.ID,
                        UrlLarga = d.UrlLarga,
                        UrlCorta = d.UrlCorta,                                                      
                        FechaCreacion = d.FechaCreacion.Value,
                        Habilitado = d.Habilitado,
                        NumVisitas = d.NumVisitas,
                        FechaExpira = d.FechaExpira                           
                    }).ToListAsync();
            
            return View(lst);
        }
        public async Task<IActionResult> Excel()
        {
            var lista = await  _db.UrlConfigs.ToListAsync();
            int row = 1;
            using (var workbook = new XLWorkbook())
            {
                var hoja = workbook.Worksheets.Add("ListaUrl");
                hoja.Cell(row, 1).Value = "ID";
                hoja.Cell(row, 1).Value = "Fecha";
                hoja.Cell(row, 1).Value = "URL";
                hoja.Cell(row, 1).Value = "N° VISITAS";
                hoja.Cell(row,1).Style.Alignment.SetTextRotation(90);
                hoja.Columns("A", "H").AdjustToContents();
                foreach (var url in lista)
                {
                    row++;
                    hoja.Cell(row,1).Value = url.ID;
                    hoja.Cell(row,2).Value = url.FechaCreacion;
                    hoja.Cell(row,3).Value = url.UrlCorta;
                    hoja.Cell(row,4).Value = url.NumVisitas;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "url.xlsx");
            }
            }
            
        }
        // GET: AcortadorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AcortadorController/Create
        
        public ActionResult Create()
        {
            return PartialView();
        }
        
        [HttpPost]
        
        public JsonResult Create (UrlConfigClass model)
        //public ActionResult Create(UrlConfigClass urlConfigClass)
        {
            try
            {
                DataTable dataTable = new DataTable();
                int id_gen = 0;
                string tokenUrl = "";
                string cnnstring = _configuration.GetConnectionString("conexionActiva");
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("conexionActiva")))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand("crear_guardar_url_corta", connection);
                    adapter.SelectCommand.Parameters.Add(new SqlParameter() { ParameterName = "ID", Value = model.ID });
                    adapter.SelectCommand.Parameters.Add(new SqlParameter() { ParameterName = "UrlLarga", Value = model.UrlLarga });
                    adapter.SelectCommand.Parameters.Add(new SqlParameter() { ParameterName = "UrlCorta", Value = model.UrlCorta });
                    adapter.SelectCommand.Parameters.Add(new SqlParameter() { ParameterName = "FechaCreacion", Value = model.FechaCreacion });
                    adapter.SelectCommand.Parameters.Add(new SqlParameter() { ParameterName = "FechaExpira", Value = model.FechaExpira });
                    adapter.SelectCommand.Parameters.Add(new SqlParameter() { ParameterName = "Habilitado", Value = model.Habilitado });
                    adapter.SelectCommand.Parameters.Add(new SqlParameter() { ParameterName = "LongitudToken", Value = _configuration.GetSection("LongitudToken").Value });


                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.Fill(dataTable);
                }

                if (dataTable.Rows.Count == 0)
                    return Json(new { success = false, URL = model });


                foreach (DataRow row in dataTable.Rows)
                {
                    id_gen = Convert.ToInt32(row["ID"]);
                    tokenUrl = _configuration.GetSection("HostName").Value + row["UrlCorta"].ToString();
                    //UrlConfigClass UrlConfigClass = new UrlConfigClass();
                    model.ID = id_gen;
                    model.FechaExpira = Convert.ToDateTime(row["FechaExpira"]);// DateTime.Now;
                    model.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);// DateTime.Now;
                    model.UrlCorta = tokenUrl;

                }

                return Json(new { auto_ID = id_gen, success = true, URL = model });

            } catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1(UrlConfigClass Item)
        {
            try
            {
                _db.UrlConfigs.Add(Item);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        

        // GET: AcortadorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AcortadorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AcortadorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpDelete]
        public JsonResult Eliminar(int Id)
        {
            try
            {
                //AlumnosViewModel model = new AlumnosViewModel();
                using (_db)
                {
                    var dbTabla = _db.UrlConfigs.Find(Id);
                    if (dbTabla == null)
                    {
                        throw new Exception("No se encontró el ID: " +  Id);
                    }
                    _db.UrlConfigs.Remove(dbTabla);
                    _db.SaveChanges();
                    return Json(new { success = true, ID = Id});
                }
            } catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            
        }
        // POST: AcortadorController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
