using BikeRentalSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BikeRentalSystem.Infrastructure.Database
{
    public class VehicleRepository : Repository<Vehicle>, IRepository<Vehicle>
    {
        private readonly IRepository<Vehicle> _repository;
        private readonly AppDbContext _appDbContext;
        
        public VehicleRepository(IRepository<Vehicle> repository, AppDbContext _context) : base(_context)
        {
            _repository = repository;
            _appDbContext = _context;
        }

        public override Vehicle GetByID(int id, params Expression<Func<Vehicle, object>>[] expressions)
        {
            IQueryable<Vehicle> query = _appDbContext.Vehicles;
            foreach (var expression in expressions) 
            {
                query = query.Include(expression);
            }
            return query.SingleOrDefault(x => x.Id == id);
        }



    }
}
