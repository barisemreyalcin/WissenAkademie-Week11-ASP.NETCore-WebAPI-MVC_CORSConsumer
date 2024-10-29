using System.Diagnostics;
using System.Text.Json.Serialization;
using ConsumeWebAPiCORS.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConsumeWebAPiCORS.Controllers
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

        private string baseUrl = "https://localhost:7103/api/";
        private string studentListMethod = "Student/GetAllStudents"; 

		public async Task<IActionResult> Privacy()
        {
            HttpClient client = new HttpClient();
            string url = string.Concat(baseUrl, studentListMethod);
			var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            // deserialize json to list
            List<Student> studentList = JsonConvert.DeserializeObject<List<Student>>(json); // Newtonsoft package yükle
            return View(studentList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
