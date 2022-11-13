using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class BaseRepository <TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly WarNotesContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(WarNotesContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            await Task.Run(() => _dbSet.Remove(entity));
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _dbContext.Entry(entity).State = EntityState.Modified);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<int> SaveChangesAcync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
