using BikeRentalSystem.Models;
using Microsoft.CodeAnalysis;
using System.Linq.Expressions;

namespace BikeRentalSystem.Infrastructure.Database
{
    public interface IRepository<T> 
    {
        IQueryable<T> GetAll();
        T GetByID(int id, params Expression<Func<T, object>>[] includes);
        void Add (T entity);
        void Update (T entity);
        void Delete (int id);
    }
}
