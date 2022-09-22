using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGL.TPP.CustomerValidation.API.Storage.Services
{
    public interface ITableRepository<TEntity>
    {
        Task AddAsync(TEntity entity);

        Task<TEntity> GetAsync(string partitionKey, string rowKey);

        Task<IEnumerable<TEntity>> GetByPartitionAsync(string partitionKey);

        Task<bool> IsTableExists();

        Task BatchInsert(IEnumerable<TEntity> rows);

        Task InsertOrMerge(TEntity record);
    }
}
