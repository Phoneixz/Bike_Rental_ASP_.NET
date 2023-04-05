using BikeRentalSystem.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using NuGet.Protocol;
using System.Linq.Expressions;

namespace BikeRentalSystem.Infrastructure.Database
{
    public class VehicleRepository : IRepository<Vehicle>
    {
        private readonly Repository<Vehicle> _repository;
        
        public VehicleRepository(Repository<Vehicle> repository) 
        {
            _repository = repository;
        }
        public void Add(Vehicle entity)
        {
            _repository.Add(entity);
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IQueryable<Vehicle> GetAll()
        {
            return _repository.GetAll();
        }

        public Vehicle GetByID(int id, params Expression<Func<Vehicle, object>>[] expressions)
        {
            return _repository.GetByID(id, e => e.VehicleType);
        }

        public void Update(Vehicle entity)
        {
            _repository.Update(entity);
        }
    }
}
