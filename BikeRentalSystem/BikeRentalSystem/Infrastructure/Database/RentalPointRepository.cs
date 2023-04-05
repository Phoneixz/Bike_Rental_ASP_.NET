using BikeRentalSystem.Models;
using System.Linq.Expressions;

namespace BikeRentalSystem.Infrastructure.Database
{
    public class RentalPointRepository : IRepository<RentalPoint>
    {
        private readonly Repository<RentalPoint> _rentalPointRepository;
        public RentalPointRepository(Repository<RentalPoint> rentalPointRepository)
        {
            _rentalPointRepository = rentalPointRepository;
        }

        public void Add(RentalPoint entity)
        {
            _rentalPointRepository.Add(entity);
        }

        public void Delete(int id)
        {
           _rentalPointRepository.Delete(id);
        }

        public IQueryable<RentalPoint> GetAll()
        {
           return _rentalPointRepository.GetAll();
        }

        public RentalPoint GetByID(int id, params Expression<Func<RentalPoint, object>>[] expressions)
        {
           return _rentalPointRepository.GetByID(id, e => e.vehicles);
        }

        public void Update(RentalPoint entity)
        {
            _rentalPointRepository.Update(entity);
        }
    }
}
