// Repositories/Repository.cs
using Microsoft.EntityFrameworkCore;
using gestion_dette_cour_2.Data;

namespace gestion_dette_cour_2.Core.Repositories
{
    public class RepositoryEF<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryEF(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual int Insert(T entity)
        {
            _dbSet.Add(entity);
            return _context.SaveChanges();
        }

        public virtual int Update(T entity)
        {
            _dbSet.Update(entity);
            return _context.SaveChanges();
        }

        public virtual int Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return _context.SaveChanges();
            }
            return 0;
        }
    }
}