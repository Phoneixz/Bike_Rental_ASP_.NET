using BikeRentalSystem.Models;
using System.Linq.Expressions;

namespace BikeRentalSystem.Infrastructure.Database
{
    public class ReservationRepository : Repository<Reservation>,IRepository<Reservation>
    {
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly AppDbContext _appDbContext;
        public ReservationRepository(IRepository<Reservation> reservationRepository, AppDbContext appDbContext) : base(appDbContext)
        {
            _reservationRepository = reservationRepository;
            _appDbContext = appDbContext;
        }
    }
}
