using BikeRentalSystem.Models;

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

        public IEnumerable<Vehicle> GetAll()
        {
            return _repository.GetAll();
        }

        public Vehicle GetByID(int id)
        {
            return _repository.GetByID(id);
        }

        public void Update(Vehicle entity)
        {
            _repository.Update(entity);
        }
    }




}
