using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Entities;

namespace Teleperformance.Core.DataAccess.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(Expression<Func<T, bool>> filter, bool tracking = true, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, bool tracking = true, params Expression<Func<T, object>>[] includes);
        IQueryable<T> Query();
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entites);
        Task Update(T entity);
        Task DeleteAsync(T entity);
        Task HardDelete(T entity);
        Task DeleteRange(List<T> entities);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
    }
}
