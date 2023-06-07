using BikeRentalSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace BikeRentalSystem.ViewModels
{
    [Keyless]
    public class ReservationDetailViewModel
    {
        public int Id { get; set; }
        public string CustomerId {get; set; }

        public string Status { get; set; }

        public DateTime PickupDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public double TotalCost { get; set; } 

        public List<Vehicle> Vehicles { get; set; }
        public List<string> SelectedVehicleIds { get; set; }

        public IEnumerable<SelectListItem> SelList { get; set; }
    }
}
