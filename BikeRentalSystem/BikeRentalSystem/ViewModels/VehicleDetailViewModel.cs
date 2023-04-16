using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using BikeRentalSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeRentalSystem.ViewModels
{
    public class VehicleDetailViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Make is required.")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Make must be between 5 and 500 characters")]

        public string Make { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Invalid image URL format.")]
        public string ImgURL { get; set; }

        [Required(ErrorMessage = "Vehicle type is required.")]
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }

        [Required(ErrorMessage = "Rental point is required.")]
        public int RentalPointId { get; set; }

        public bool Availability { get; set; }

        public IEnumerable<SelectListItem> VehicleTypes { get; set; }

        public IEnumerable<SelectListItem> RentalPoints { get; set; }
    }
}
