using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IRepository <TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<int> SaveChangesAcync();
    }
}
