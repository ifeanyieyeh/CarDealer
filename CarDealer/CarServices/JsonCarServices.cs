using CarDealer.Models;
using System.Text.Json;

namespace CarDealer.CarServices
{
    public class JsonCarServices
    {
        private readonly string _carsFile;
        private readonly string _enquiriesFile;

        public JsonCarServices(IWebHostEnvironment env)
        {
            var dataSer = Path.Combine(env.ContentRootPath, "APP_data");
            Directory.CreateDirectory(dataSer);
            _carsFile = Path.Combine(dataSer, "cars.json");
            _enquiriesFile = Path.Combine(dataSer, "enquiries.json");
        }

        private List<Cars> ReadCars() => File.Exists(_carsFile) ? JsonSerializer.Deserialize<List<Cars>>(File.ReadAllText(_carsFile)) ?? new() : new();

        private List<Enquiry> Readenquiries() => File.Exists(_enquiriesFile) ? JsonSerializer.Deserialize<List<Enquiry>>(File.ReadAllText(_enquiriesFile)) ?? new() : new();

        private void SaveCars(List<Cars> cars) => File.WriteAllText(_carsFile, JsonSerializer.Serialize(cars, new JsonSerializerOptions { WriteIndented = true }));

        private void SaveEnquires(List<Enquiry> enquiries) => File.WriteAllText(_enquiriesFile, JsonSerializer.Serialize(enquiries, new JsonSerializerOptions { WriteIndented = true }));

        public IEnumerable<Cars> GetCars() => ReadCars();
        public Cars GetCars(int id) => ReadCars().FirstOrDefault(c => c.Id == id) ?? new Cars();

        public void AddCar(Cars car)
        {
            var cars = ReadCars();
            car.Id = cars.Any() ? cars.Max(c => c.Id) + 1 : 1;
            cars.Add(car);
            SaveCars(cars);
        }

        public IEnumerable<Enquiry> GetAllEnquiries() => Readenquiries();

        public void AddEnquiry(Enquiry enquiry)
        {
            var enquires = Readenquiries();
            enquiry.Id = enquires.Any() ? enquires.Max(i => i.Id) + 1 : 1;
            enquires.Add(enquiry);
            SaveEnquires(enquires);
        }
    }


}
