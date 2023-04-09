using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeRentalSystem.ViewModels
{
    public class RentalPointDetailViewModel
    {
        public RentalPointDetailViewModel()
        {
            Vehicles = new List<VehicleDetailViewModel>();
        }
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }

        public List<VehicleDetailViewModel> Vehicles { get; set; }
        public IEnumerable<SelectListItem> VehicleList { get; set; }
    }
}
