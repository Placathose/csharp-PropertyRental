using System.ComponentModel.DataAnnotations;

namespace csharp_PropertyRental.Models
{
    public class PropertyViewModel
    {
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "Landlord ID is required.")]
        public int LandlordId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Property type is required.")]
        public string? PropertyType { get; set; }

        [Required(ErrorMessage = "Number of bedrooms is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Bedrooms must be a positive number.")]
        public int Bedrooms { get; set; }

        [Required(ErrorMessage = "Number of bathrooms is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Bathrooms must be a positive number.")]
        public int Bathrooms { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Square footage must be a positive number.")]
        public int SquareFootage { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}