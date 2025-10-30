using CarDealer.CarServices;
using Microsoft.AspNetCore.Mvc;
using CarDealer.Models;

namespace CarDealer.Controllers
{
    public class AdminController : Controller
    {

        private readonly JsonCarServices _services;
        private readonly IWebHostEnvironment _env;

        public AdminController(JsonCarServices services, IWebHostEnvironment env)
        {
            _services = services;
            _env = env;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(Admin model)
        {
            if(model.EmailorUsername == "admin" && model.Password == "Ifeanyi")
            {
                HttpContext.Session.SetString("Admin", "true");
                return RedirectToAction("Dashboard");
            }
            ViewBag.Error = "Invalid Credentials";
            return View(model);
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("Admin") != "true")
                return RedirectToAction("Login");
                    return View(_services.GetCars());
        }

        [HttpGet]
        public IActionResult AddCar() => View();

        [HttpPost]
        public IActionResult AddCar(Cars cars, IFormFile image)
        {
            if (image != null)
            {
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadPath);
                var fileName = Path.GetFileName(image.FileName);
                var filePath = Path.Combine(uploadPath, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                image.CopyTo(stream);
                cars.CarImagePath = "/uploads/" + fileName;
            }
            _services.AddCar(cars);
            return RedirectToAction("Dashboard");
        }
    }
}
