using CarDealer.CarServices;
using CarDealer.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Controllers
{
    public class HomeController : Controller
    {
        private readonly JsonCarServices _storage;

        public HomeController(JsonCarServices storage)
        {
            _storage = storage;
        }

        
        public IActionResult Index()
        {
            var cars = _storage.GetCars();
            return View(cars);
        }

        public IActionResult Details(int id)
        {
            var car = _storage.GetCars(id);

            if (car == null || car.Id == 0)
                return NotFound();

            return View(car);
        }
    }
}
