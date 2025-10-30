using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace CarDealer.Models
{
    public class Enquiry
    {
        public int Id { get; set; }
        [Required]
        public int CarId { get; set; }
        [Required]
        public string CarName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
