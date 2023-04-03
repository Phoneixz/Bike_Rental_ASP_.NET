using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BikeRentalSystem.Infrastructure.Database
{
    public class Repository<T> : IRepository<T> where T : class
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

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        
        public T GetByID(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
           _context.Set<T>().Update(entity);
           _context.SaveChanges();

        }
    }
}
