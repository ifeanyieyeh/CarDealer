using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models
{
    public class Cars
    {
        public int Id { get; set; }
        [Required]
        public string CarName { get; set; }
        [Required]
        public string CarMake { get; set; }
        [Required]
        public string CarModel {  get; set; }
        [Required]
        public int YearOfProduction {  get; set; }
        [Required]
        public decimal CarPrice { get; set; }
        [Required]
        public string CarDescription { get; set; } = string.Empty;
        [Required]
        public string CarImagePath { get; set; } = string.Empty;

    }
}
