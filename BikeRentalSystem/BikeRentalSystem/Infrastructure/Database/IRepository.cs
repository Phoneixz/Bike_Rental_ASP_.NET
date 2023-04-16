using BikeRentalSystem.Models;
using System.Linq.Expressions;

namespace BikeRentalSystem.Infrastructure.Database
{
    public interface IRepository<T> 
    {
        IQueryable<T> GetAll();
        T GetByID(int id, Expression<Func<T, object>>[] expressions);
        void Add (T entity);
        void Update (T entity);
        void Delete (int id);
    }
}
