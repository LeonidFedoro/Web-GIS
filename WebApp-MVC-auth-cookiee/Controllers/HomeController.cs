using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp_MVC_auth_cookiee.Data;
using WebApp_MVC_auth_cookiee.Models;
using WebApp_MVC_auth_cookiee.Repositories;
using WebApp_MVC_auth_jwt.Models;
using static System.Console;
using static WebApp_MVC_auth_jwt.Models.Students;

namespace WebApp_MVC_auth_jwt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserRepository _userRepository;
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _userRepository = new UserRepository(context);
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="master, phd")]
        public IActionResult Master()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Phd()
        {
            return View();
        }

        [Authorize(Roles = "phd, master, bachelor")]
        public IActionResult Bachelor()
        {
            return View();
        }

        public IActionResult Denied() {
            return View();
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");
            return View();
        }

        public async Task<IActionResult>  Logout()
        {
            if (User.Identity.IsAuthenticated)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Имя пользователя и пароль обязательны.");
                return View();
            }

            // Проверка на существование пользователя
            var existingUser = await _userRepository.GetUserByCredentials(username,password);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Пользователь с таким именем уже существует.");
                return View();
            }

            // Создание нового пользователя
            var newUser = new User
            {
                Username = username,
                Password = password // Не забудьте хешировать пароль в реальном приложении
            };

            await _userRepository.Add(newUser);
            await _userRepository.SaveChangesAsync();

            return RedirectToAction("Check", "Home", new { id = username, pass = password });// Перенаправление на страницу логина после успешной регистрации
        }
        public async Task<IActionResult> Check(string? id, string pass, string? url)
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");

            // Получаем пользователя из базы данных
            User user = await _userRepository.GetUserByCredentials(id, pass);

            if (user is null)
            {
                return View("Error", model: new ErrorViewModel { RequestId = "Failed login" });
            }

            var claims = new List<Claim>
        {
           
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())// Используем роль из базы данных
        };

            var claimId = new ClaimsIdentity(claims, "Cookies");
            var claimPrincipal = new ClaimsPrincipal(claimId);
            await HttpContext.SignInAsync(claimPrincipal);

            if (string.IsNullOrEmpty(url) || url == "null") url = "/";
            return Redirect(url);
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
