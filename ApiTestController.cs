using Microsoft.AspNetCore.Mvc;
using RaceDayMVC1.Services;
using System.Threading.Tasks;

namespace RaceDayMVC1.Controllers
{
    public class ApiTestController : Controller
    {
        private readonly RaceDayApiService _apiService;

        public ApiTestController(RaceDayApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            bool connected = await _apiService.TestConnectionAsync();

            if (connected)
            {
                ViewBag.Message = "RaceDay API connected successfully!";
            }
            else
            {
                ViewBag.Message = "Could not connect to RaceDay API.";
            }

            return View();
        }
    }
}
