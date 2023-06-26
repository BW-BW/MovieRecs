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

        /*public async Task<IActionResult> GetCurrentUser(String  username)
        {
            if (username == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Username == username);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }*/

        public async Task<List<User>> GetCurrentUser(string username)
        {
            if (username == null || _context.User == null)
            {
                return null;
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Username == username);
            if (user == null)
            {
                return null;
            }

            return new List<User> { user };
        }

        public async Task<string> CallApiGen1()
        {
            var userdata = await GetCurrentUser("Brian");
            System.Diagnostics.Debug.WriteLine("kontoolll");
            System.Diagnostics.Debug.WriteLine(userdata);
            string genre = userdata[0].Genre1;

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

        public async Task<string> CallApiGen2()
        {
            var userdata = await GetCurrentUser("Brian");
            System.Diagnostics.Debug.WriteLine("kontoolll");
            System.Diagnostics.Debug.WriteLine(userdata);
            string genre = userdata[0].Genre2;

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

        public async Task<IActionResult> CallApi()
        {
            string responseBody = await CallApiGen2();
            string responseBody2 = await CallApiGen1();

            string json = "[" + responseBody + "," + responseBody2 + "]";

            if (responseBody != null)
            {
                return View("CallApi", json);
            }
            else
            {
                return View("CallApi");
            }

        }

        /*public async Task<IActionResult> CallApi()
        {
            var userdata = await GetCurrentUser("Brian");
            System.Diagnostics.Debug.WriteLine("kontoolll");
            System.Diagnostics.Debug.WriteLine(userdata);
            string genre = userdata[0].Genre2;

            string apiUrl = "https://moviesminidatabase.p.rapidapi.com/movie/byGen/" + genre + "/";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("X-RapidAPI-Key", "87429eac32msh305b85d98fca914p1122fajsnad4deb35ba9c");
            request.Headers.Add("X-RapidAPI-Host", "moviesminidatabase.p.rapidapi.com");

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // Read first response content
                string responseBody = await response.Content.ReadAsStringAsync();

                return View("CallApi", responseBody);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("boo");
                return StatusCode((int)response.StatusCode);
            }
        }*/

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