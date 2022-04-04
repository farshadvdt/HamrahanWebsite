using Contexts;
using FarshadTools;
using HamrahanTemplate.Infrastructure.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HamrahanTemplate.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly HamrahanDbContext _db;
        internal DbSet<T> dbset;

        public Repository(HamrahanDbContext db)
        {
            if (db is null)
            {
                throw new ArgumentNullException(nameof(db));
            }
            _db = db;
            dbset = _db.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            var result = dbset;
            return result.AsNoTracking().ToList();
        }
       
        public IQueryable<T> Find(Expression<Func<T, bool>> filter)
        {
            return dbset.Where(filter).AsNoTracking();
        }

        public IEnumerable<T> GetLatestItems(Expression<Func<T, object>> filter,int numberOfTakeItems)
        {
            return dbset.OrderByDescending(filter).Take(numberOfTakeItems).AsNoTracking().ToList();
        }

        public IQueryable<T> Find(string query, params object[] parameters)
        {
            return dbset.FromSqlRaw(query, parameters).AsNoTracking();
        }
        public void Insert(T entity)
        {
            dbset.Add(entity);
        }
        public void Update(T entity)
        {
           dbset.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }

        public bool HardDelete(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            dbset.Remove(entity);
            _db.Entry(entity).State = EntityState.Deleted;
            return true;

        }


    }
}
