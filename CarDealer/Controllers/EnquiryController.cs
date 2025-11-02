using CarDealer.CarServices;
using CarDealer.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Controllers
{
    public class EnquiryController : Controller
    {
        private readonly JsonCarServices _storage;

        public EnquiryController(JsonCarServices storage)
        {
            _storage = storage;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string name, string email, string message)
        {
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(message))
            {
                ViewBag.Error = "Please fill all fields.";
                return View();
            }

            var enquiry = new Enquiry
            {
                CarId = 0,
                CarName = "General",
                EmailAddress = email,
                Message = $"From: {name}\n\n{message}"
            };

            _storage.AddEnquiry(enquiry);
            ViewBag.Message = "Your enquiry was sent successfully!";
            return View();
        }

 
        [HttpGet]
        public IActionResult Details(int id)
        {
            var car = _storage.GetCars(id);

            if (car == null || car.Id == 0)
                return NotFound();

            return View(car);
        }

        [HttpPost]
        public IActionResult Details(int carId, string carname, string emailaddress, string message)
        {
            if (string.IsNullOrWhiteSpace(emailaddress) || string.IsNullOrWhiteSpace(message))
            {
                ViewBag.Error = "Please fill all fields.";
                var car = _storage.GetCars(carId);
                return View(car);
            }

            var enquiry = new Enquiry
            {
                CarId = carId,
                CarName = carname,
                EmailAddress = emailaddress,
                Message = message
            };

            _storage.AddEnquiry(enquiry);

            ViewBag.Message = "Your enquiry has been sent successfully!";
            var carDetails = _storage.GetCars(carId);
            return View(carDetails);
        }
    }
}
