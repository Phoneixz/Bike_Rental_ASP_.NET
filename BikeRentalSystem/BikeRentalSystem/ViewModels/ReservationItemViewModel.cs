using Microsoft.EntityFrameworkCore;

namespace BikeRentalSystem.ViewModels
{
    public class ReservationItemViewModel
    {
        public List<ReservationDetailViewModel> Reservations { get; set; }
    }
}
