using BikeRentalSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BikeRentalSystem.Infrastructure.Database
{
    public class RentalPointRepository : Repository<RentalPoint>,IRepository<RentalPoint>
    {
        private readonly IRepository<RentalPoint> _rentalPointRepository;
        private readonly AppDbContext _appDbContext;
        public RentalPointRepository(IRepository<RentalPoint> rentalPointRepository, AppDbContext appDbContext) : base(appDbContext)
        {
            _rentalPointRepository = rentalPointRepository;
            _appDbContext = appDbContext;
        }
        public override RentalPoint GetByID(int id, params Expression<Func<RentalPoint, object>>[] expressions)
        {
            IQueryable<RentalPoint> query = _appDbContext.RentalPoints;

            foreach(var expression in expressions) 
            {
                query = query.Include(expression);
            }
            return query.SingleOrDefault(v => v.Id == id);
        }
    }
}
