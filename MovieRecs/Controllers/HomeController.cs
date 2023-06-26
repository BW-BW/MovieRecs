using Microsoft.AspNetCore.Mvc;
using MovieRecs.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieRecs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> CallApi()
        {
            string apiUrl = "https://moviesminidatabase.p.rapidapi.com/movie/byGen/Comedy/";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("X-RapidAPI-Key", "87429eac32msh305b85d98fca914p1122fajsnad4deb35ba9c");
            request.Headers.Add("X-RapidAPI-Host", "moviesminidatabase.p.rapidapi.com");

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return View("CallApi", responseBody);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("boo");
                return StatusCode((int)response.StatusCode);
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