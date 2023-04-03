using Microsoft.CodeAnalysis;
using System.Linq.Expressions;

namespace BikeRentalSystem.Infrastructure.Database
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetByID(int id);
        void Add (T entity);
        void Update (T entity);
        void Delete (int id);
    }
}
