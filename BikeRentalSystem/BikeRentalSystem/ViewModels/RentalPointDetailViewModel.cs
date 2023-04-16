using BikeRentalSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeRentalSystem.ViewModels
{
    public class RentalPointDetailViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Address should be between 5 and 100 characters")]
        public string Address { get; set; }
        [Display(Name = "Vehicles")]
        public IEnumerable<Vehicle> Vehicles { get; set; }
    }
}
