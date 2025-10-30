using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models
{
    public class Admin
    {
        [Required]
       public string EmailorUsername { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
