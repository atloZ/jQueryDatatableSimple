using jQueryDatatableMVCSimple.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;

namespace jQueryDatatableMVCSimple.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// _context adatbázis csatlakozáshoz szükséges osztály implementálása
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        private readonly DatabaseContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _context = new DatabaseContext();
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Teljes adatsor kíratásara szolgáló lap meghivása
        /// </summary>
        public IActionResult Datatable()
        {
            return View();
        }

        /// <summary>
        /// Adatbázis és Datatable összekötése
        /// </summary>
        [HttpPost]
        public IActionResult GetList()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var customerData = (from a in _context.Addresses select a);

                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.AddressId.ToString().Contains(searchValue)
                                                || m.AddressLine1.Contains(searchValue)
                                                || m.AddressLine2.Contains(searchValue)
                                                || m.City.Contains(searchValue)
                                                || m.StateProvinceId.ToString().Contains(searchValue)
                                                || m.PostalCode.Contains(searchValue)
                                                || m.Rowguid.ToString().Contains(searchValue)
                                                || m.ModifiedDate.ToString().Contains(searchValue));
                }
                recordsTotal = customerData.Count();
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}