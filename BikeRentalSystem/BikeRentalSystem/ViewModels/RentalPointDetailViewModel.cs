using BikeRentalSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeRentalSystem.ViewModels
{
    public class RentalPointDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }

    }
}
