using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Entities;

namespace Teleperformance.Core.DataAccess.Repository
{
    public class BaseEntityRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;

        public BaseEntityRepository(DbContext context)
        {
            _context = context;
        }
        DbSet<T> Table => _context.Set<T>();
        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(List<T> entites)
        {
            await Table.AddRangeAsync(entites);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await Table.AnyAsync(filter);
        }
        public async Task DeleteAsync(T entity)
        {
            entity.Status = false;
            await Update(entity);
        }

        public async Task DeleteRange(List<T> entities)
        {
            Table.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, bool tracking = true, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Table;

            if (filter != null) { query = query.Where(filter); }
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (!tracking) query = query.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, bool tracking = true, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Table;

            if (filter != null) { query = query.Where(filter); }
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (!tracking) query = query.AsNoTracking();
            return await query.SingleOrDefaultAsync();
        }

        public async Task HardDelete(T entity)
        {
            Table.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> Query()
        {
            return Table;
        }

        public async Task Update(T entity)
        {
            Table.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
