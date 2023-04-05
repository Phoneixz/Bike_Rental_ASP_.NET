using BikeRentalSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BikeRentalSystem.Infrastructure.Database
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly AppDbContext _context;
        

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetByID(id);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }
        
        public T GetByID(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.FirstOrDefault(e => e.Id == id);
        }

        public void Update(T entity)
        {
           _context.Set<T>().Update(entity);
           _context.SaveChanges();

        }
    }
}
