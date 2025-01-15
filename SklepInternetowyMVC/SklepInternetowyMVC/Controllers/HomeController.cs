using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SklepInternetowyMVC.Data;
using SklepInternetowyMVC.Models;
using System.Diagnostics;

namespace SklepInternetowyMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Authorize(Roles = "Admin")]  // Tylko admin mo¿e mieæ dostêp do tego kontrolera
        public class BooksAdminController : Controller
        {
            private readonly ApplicationDbContext _context;

            public BooksAdminController(ApplicationDbContext context)
            {
                _context = context;
            }

            // Akcja, która wyœwietli stronê z formularzem dodawania ksi¹¿ek
            public IActionResult ManageBooks()
            {
                return View();  // Zwróci widok Index.cshtml
            }

            // Inne akcje jak Create, Edit, Delete...
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
