using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp_MVC_auth_cookiee.Models;
using WebApp_MVC_auth_cookiee.Repositories;

namespace WebApp_MVC_auth_cookiee.Controllers
{
    public class PlaceController : Controller
    {
        private readonly IPlaceRepository _placeRepository;

        public PlaceController(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        // GET: Place/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Place/Create
        [HttpPost]
        public async Task<IActionResult> Create(Place place)
        {
            if (ModelState.IsValid)
            {
                // Установите UserId из формы
                place.UserId = int.Parse(Request.Form["User Id"]);

                await _placeRepository.AddPlaceAsync(place);
                await _placeRepository.SaveChangesAsync();
                return RedirectToAction("Index"); // Перенаправление на страницу со списком мест
            }
            return View(place); // Если есть ошибки, возвращаем ту же форму
        }


        // GET: Place/Index
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var places = await _placeRepository.GetAllPlacesAsync();
            return View(places);
        }
        // GET: Place/Redirect

        public IActionResult Redirect()
        {
            return View();
        }

    }
}