using BikeRentalSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BikeRentalSystem.Infrastructure.Database
{
    public class RentalPointRepository : IRepository<RentalPoint>
    {
        private readonly Repository<RentalPoint> _rentalPointRepository;
        private readonly AppDbContext _appDbContext;
        public RentalPointRepository(Repository<RentalPoint> rentalPointRepository, AppDbContext appDbContext)
        {
            _rentalPointRepository = rentalPointRepository;
            _appDbContext = appDbContext;
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
            IQueryable<RentalPoint> query = _appDbContext.Set<RentalPoint>();
            foreach(var expression in expressions)
            {
                query = query.Include(expression);
            }
            query = query.Include(rp => rp.vehicles).ThenInclude(v => v.VehicleType);
            return query.FirstOrDefault(rp => rp.Id == id);
        }

        public void Update(RentalPoint entity)
        {
            _rentalPointRepository.Update(entity);
        }
    }
}
