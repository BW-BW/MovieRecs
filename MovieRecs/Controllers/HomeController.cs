using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRecs.Data;
using MovieRecs.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure;
using System.ComponentModel;

namespace MovieRecs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HttpClient _httpClient;

        private readonly MovieRecsContext _context;

        public HomeController(ILogger<HomeController> logger, MovieRecsContext context)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _context = context;
        }

        public async Task<string> CallApiGenre(string genre)
        {
            string apiUrl = "https://moviesminidatabase.p.rapidapi.com/movie/byGen/" + genre + "/";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("X-RapidAPI-Key", "87429eac32msh305b85d98fca914p1122fajsnad4deb35ba9c");
            request.Headers.Add("X-RapidAPI-Host", "moviesminidatabase.p.rapidapi.com");

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // Read first response content
                string responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("boo");
                return ("WTF");
            }
        }

        public async Task<IActionResult> CallApi(string[] selectedGenres)
        {
            string[] genrezz = ProcessGenres(selectedGenres);
            string responseBody = "";
            string json = "";
            ViewData["errors"] = "";

            if (genrezz.Length == 1)
            {
                responseBody = await CallApiGenre(genrezz[0]);
                json = "[" + responseBody + "]";
            } else if (genrezz.Length == 2)
            {
                responseBody = await CallApiGenre(genrezz[0]);
                string responseBody2 = await CallApiGenre(genrezz[1]);
                json = "[" + responseBody + "," + responseBody2 + "]";
            } else if (genrezz.Length == 3) 
            {
                responseBody = await CallApiGenre(genrezz[0]);
                string responseBody2 = await CallApiGenre(genrezz[1]);
                string responseBody3 = await CallApiGenre(genrezz[2]);
                json = "[" + responseBody + "," + responseBody2 + "," + responseBody3 + "]";
            } else if (genrezz.Length > 3)
            {
                ViewData["errors"] = "Choose Maximum 3 Category";
            }
            else
            {
                ViewData["errors"] = "Choose Atleast 1 Category";
            }

            if (responseBody != null)
            {
                return View("CallApi", json);
            }
            else
            {
                return View("CallApi");
            }

        }

        [HttpPost]
        public string[] ProcessGenres(string[] selectedGenres)
        {
            string[] chosenGenres = selectedGenres.ToArray();
            return chosenGenres;
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