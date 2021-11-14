using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentalCar.Context;

namespace RentalCar.Repository
{
    public class BaseRepositoryAsync : IBaseRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create<T>(T entity) where T : class
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await SaveChanges();
        }

        public async Task Delete<T>(int id) where T : class
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            _dbContext.Set<T>().Remove(entity);
            await SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById<T>(int id) where T : class
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public async Task<IEnumerable<T>> Fetch<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWithIncludeAsync<T>(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            var query = Include(includeProperties);
            return await query.Where(condition).ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<T> Include<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            return includeProperties
                .Aggregate(_dbContext.Set<T>().AsQueryable(), (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
