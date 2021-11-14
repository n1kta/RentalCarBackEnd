using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RentalCar.Repository
{
    public interface IBaseRepositoryAsync
    {
        Task Create<T>(T entity) where T : class;
        Task Delete<T>(int id) where T : class;
        void Update<T>(T entity) where T : class;
        Task<IEnumerable<T>> GetAll<T>() where T : class;
        Task<T> GetById<T>(int id) where T : class;
        Task<IEnumerable<T>> Fetch<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<IEnumerable<T>> GetWithIncludeAsync<T>(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includeProperties) where T : class;
        Task SaveChanges();
    }
}
