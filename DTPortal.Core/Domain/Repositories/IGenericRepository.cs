using System.Threading.Tasks;
using System.Collections.Generic;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(int id);

        TEntity GetById(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task AddAsync(TEntity model);

        void Add(TEntity model);

        void Update(TEntity model);

        void Reload(TEntity model);

        void Remove(TEntity model);
    }
}
