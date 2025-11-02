using CarDealer.CarServices;
using CarDealer.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly JsonCarServices _services;
        private readonly IWebHostEnvironment _env;

        public ApiController(JsonCarServices services, IWebHostEnvironment env)
        {
            _services = services;
            _env = env;
        }

        
        [HttpGet("cars")]
        public IActionResult GetCars() => Ok(_services.GetCars());

       
        [HttpPost("cars")]
        public IActionResult AddCar([FromForm] Cars cars, IFormFile image)
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
            return Ok(cars);
        }

        
        [HttpGet("enquiries")]
        public IActionResult GetAllEnquiries()
        {
            var enquiries = _services.GetAllEnquiries();
            return Ok(enquiries);
        }

        
        [HttpPost("enquiries")]
        public IActionResult AddEnquiry([FromBody] Enquiry enquiry)
        {
            if (enquiry == null)
                return BadRequest("Invalid enquiry data.");

            _services.AddEnquiry(enquiry);
            return Ok(new { success = true, message = "Enquiry saved successfully." });
        }
    }
}
