using Microsoft.AspNetCore.Mvc;
using AspNetCoreUtilities.Models;
using System.Diagnostics;

namespace AspNeCoreUtilities.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPrice(decimal price, decimal secondPrice) 
        {
            return Ok(new { price, secondPrice });
        }

        [HttpGet]
        public IActionResult UploadFiles() => View();
       

        [HttpPost]
        public async Task<IActionResult> UploadFile(IEnumerable<IFormFile> files)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Files");

            foreach (var file in files.Where(f => f.Length > 0)) 
            {
                string filename = Path.Combine(path, file.FileName);

                using (var filestream = new FileStream(filename, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                }
            }

            return Ok( 
                new 
                { 
                    savedFilesLength = files.Sum(f => f.Length)
                });
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
