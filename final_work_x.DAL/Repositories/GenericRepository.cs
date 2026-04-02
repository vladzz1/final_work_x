using final_work_x.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace final_work_x.DAL.Repositories
{
    public abstract class GenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly AppDbContext _context;

        protected GenericRepository(AppDbContext context) { _context = context; }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return (await _context.SaveChangesAsync()) != 0;
        }

        public async Task<bool> CreateRangeAsync(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                await _context.Set<TEntity>().AddAsync(entity);
            }
            return (await _context.SaveChangesAsync()) != 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return (await _context.SaveChangesAsync()) != 0;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return (await _context.SaveChangesAsync()) != 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                return (await _context.SaveChangesAsync()) != 0;
            }

            return false;
        }
    }
}
