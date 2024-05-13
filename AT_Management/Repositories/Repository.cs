using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using AT_Management.Data;
using AT_Management.Repositories.IRepositories;

namespace AT_Management.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //2
        private readonly ATDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ATDbContext db)
        {
            _db = db;
            //the table = dbSet
            this.dbSet = _db.Set<T>();
            //include here
           
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked == true)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();

        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);

            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
