using Microsoft.AspNetCore.Mvc;
using AspNetCoreUtilities.Models;
using System.Diagnostics;
using Microsoft.Extensions.FileProviders;

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
        public async Task<IActionResult> UploadFiles(IEnumerable<IFormFile> files)
        { 
            string path = Path.Combine(Environment.CurrentDirectory, "Files");// moga da vzema directory prez environment

            foreach (var file in files.Where(f => f.Length > 0)) 
            {
                string filename = Path.Combine(path, file.FileName);

                using (var filestream = new FileStream(filename, FileMode.Create)) //FileStream
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

        // /home/download?filename=hi.txt -> works
        public IActionResult Download(string filename) 
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Files");
            IFileProvider fileProvider = new PhysicalFileProvider(path);
            IFileInfo fileInfo = fileProvider.GetFileInfo(filename);
            var stream = fileInfo.CreateReadStream();
            var mimeType = "application/octet-stream";

            return File(stream, mimeType, filename);
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
