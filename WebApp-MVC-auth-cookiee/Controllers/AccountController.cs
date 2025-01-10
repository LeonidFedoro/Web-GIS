using Microsoft.AspNetCore.Mvc;
using System;
using WebApp_MVC_auth_cookiee.Data;
using WebApp_MVC_auth_cookiee.Models;
using WebApp_MVC_auth_cookiee.Repositories;

namespace WebApp_MVC_auth_cookiee.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;

        public AccountController(AppDbContext context)
        {
            _userRepository = new UserRepository(context);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Здесь вы можете добавить логику для проверки существования пользователя
                // и хеширования пароля перед сохранением в базу данных

                await _userRepository.Add(user); // Предполагается, что у вас есть метод добавления пользователя
                await _userRepository.SaveChangesAsync(); // Сохраните изменения в базе данных

                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");
            return View();
        }

        // Добавьте метод для обработки логина, если он еще не добавлен
    }
}
