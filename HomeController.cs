
using Microsoft.AspNetCore.Mvc;

namespace RaceDayMVC1.Controllers
{
    public class HomeController : Controller
    {
        // Displays the homepage
        public IActionResult Index()
        {
            return View();
        }

        // Displays the events page
        public IActionResult Events()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            if (id == 1)
            {
                ViewBag.EventName = "City Fun Run";
                ViewBag.Date = "15 March 2027";
                ViewBag.Location = "Johannesburg";
                ViewBag.Category = "Running";
                ViewBag.Description = "Enjoy a fun-filled running experience with fellow running enthusiasts. Whether you are a beginner or an experienced runner, this event is a great opportunity to challenge yourself.";
            }
            else if (id == 2)
            {
                ViewBag.EventName = "Green Mile Challenge";
                ViewBag.Date = "22 March 2027";
                ViewBag.Location = "Pretoria";
                ViewBag.Category = "Marathon";
                ViewBag.Description = "Take on a new challenge and test your endurance at this exciting running event.";
            }
            else if (id == 3)
            {
                ViewBag.EventName = "Sunrise Run";
                ViewBag.Date = "5 April 2027";
                ViewBag.Location = "Soweto";
                ViewBag.Category = "Fun Run";
                ViewBag.Description = "Start your morning with movement, motivation and a great atmosphere.";
            }
            else
            {
                return NotFound();
            }

            return View();
        }

    }
}


