

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Net.Http.Json;
using RaceDayMVC1.Models.ViewModels;

namespace RaceDayMVC1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Display registration page
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Send registration details to API
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Role == "Participant")
            {
                if (model.DateOfBirth.Year < 1900)
                {
                    ModelState.AddModelError(
                        "DateOfBirth",
                        "Please enter your date of birth.");
                }

                if (string.IsNullOrWhiteSpace(model.EmergencyContact))
                {
                    ModelState.AddModelError(
                        "EmergencyContact",
                        "Please enter an emergency contact.");
                }
            }
            else if (model.Role == "Organiser")
            {
                if (string.IsNullOrWhiteSpace(model.PhoneNumber))
                {
                    ModelState.AddModelError(
                        "PhoneNumber",
                        "Please enter your phone number.");
                }
            }
            else
            {
                ModelState.AddModelError(
                    "Role",
                    "Please select a valid account type.");
            }

            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("RaceDayApi");

            var request = new
            {
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.Role == "Participant"
                    ? model.DateOfBirth
                    : (DateTime?)null,
                EmergencyContact = model.EmergencyContact
            };

            try
            {
                var response = await client.PostAsJsonAsync(
                    "api/Auth/register", request);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] =
                        "Your account has been created. You can now log in.";

                    return RedirectToAction("Login");
                }

                var error = await response.Content
                    .ReadFromJsonAsync<ApiMessageResponse>();

                ModelState.AddModelError(
                    string.Empty,
                    error?.Message ?? "Registration failed. Please try again.");
            }
            catch
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Could not connect to RaceDayApi. Please make sure the API is running.");
            }

            return View(model);
        }

        // Display login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Send login details to API
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("RaceDayApi");

            try
            {
                var response = await client.PostAsJsonAsync(
                    "api/Auth/login", new
                    {
                        model.Email,
                        model.Password
                    });

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content
                        .ReadFromJsonAsync<ApiMessageResponse>();

                    ModelState.AddModelError(
                        string.Empty,
                        error?.Message ?? "Invalid email or password.");

                    return View(model);
                }

                var user = await response.Content
                    .ReadFromJsonAsync<ApiLoginResponse>();

                if (user == null)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        "The API returned an unexpected response.");

                    return View(model);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,
                        user.UserId.ToString()),

                    new Claim(ClaimTypes.Name, user.Name),

                    new Claim(ClaimTypes.Email, model.Email),

                    new Claim(ClaimTypes.Role, user.Role)
                };

                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Could not connect to RaceDayApi. Please make sure the API is running.");

                return View(model);
            }
        }

        // Log out
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        public class ApiMessageResponse
        {
            public string Message { get; set; } = string.Empty;
        }

        public class ApiLoginResponse
        {
            public int UserId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;
        }
    }
}
