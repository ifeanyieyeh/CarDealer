using System.Diagnostics;
using CarDealer.CarServices;
using CarDealer.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly JsonCarServices _storage; 
        public HomeController(ILogger<HomeController> logger, JsonCarServices storage)
        {
            _logger = logger;
            _storage = storage;
        }

        public IActionResult Index()
        {
            return View(_storage.GetCars());
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
        public IActionResult Details(int id)
        {
            var car = _storage.GetCars(id);
            if (car == null)
                return NotFound();

            return View(car);
        }

        [HttpPost]
        public IActionResult Enquiry(int carId, string carname, string emailaddress, string message)
        {
            _storage.AddEnquiry(new Enquiry
            {
                CarId = carId,
                CarName = carname,
                EmailAddress = emailaddress,
                Message = message
            });

            ViewBag.Message = "Your enquiry was sent successfully!";
            return RedirectToAction("Details", new { id = carId });
        }
    }
}
