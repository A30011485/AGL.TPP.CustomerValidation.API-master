using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Serilog;

namespace AGL.TPP.CustomerValidation.API.Storage.Services
{
    public class TableRepository<TEntity> : ITableRepository<TEntity> where TEntity : class, ITableEntity, new()
    {
        private readonly ILogger _logger;
        private readonly CloudTable _table;

        public TableRepository(IOptions<AzureCloudSettings> settings, ILogger logger)
        {
            _logger = logger;
            var storageAccount = CloudStorageAccount.Parse(settings.Value.StorageConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            var entityName = typeof(TEntity).Name;
            _table = tableClient.GetTableReference(entityName);
            _table.CreateIfNotExistsAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _table.ExecuteAsync(TableOperation.InsertOrReplace(entity));
        }

        public async Task<TEntity> GetAsync(string partitionKey, string rowKey)
        {
            var tableResult = await ExecuteAsync(TableOperation.Retrieve<TEntity>(partitionKey, rowKey));

            return tableResult.Result as TEntity;
        }

        public async Task<IEnumerable<TEntity>> GetByPartitionAsync(string partitionKey)
        {
            var filter = TableQuery.GenerateFilterCondition(nameof(ITableEntity.PartitionKey), QueryComparisons.Equal,
                partitionKey);
            return await ExecuteQueryAsync(filter).ConfigureAwait(false);
        }

        public async Task<bool> IsTableExists() => await _table.ExistsAsync();

        private async Task<TableResult> ExecuteAsync(TableOperation operation)
        {
            _logger.Debug("Running {operation} in {tableName} table", operation.OperationType, _table.Name);

            var result = await _table.ExecuteAsync(operation);

            _logger.Debug("Completed {operation} in {tableName} table", operation.OperationType, _table.Name);

            return result;
        }

        public async Task<IEnumerable<TEntity>> ExecuteQueryAsync(string filter)
        {
            var items = new List<TEntity>();
            TableContinuationToken token = null;
            do
            {
                var tblQuery = new TableQuery<TEntity>().Where(filter);
                var seg = await _table.ExecuteQuerySegmentedAsync(tblQuery, token).ConfigureAwait(false);
                token = seg.ContinuationToken;
                items.AddRange(seg);
            } while (token != null);

            return items;
        }

        public async Task BatchInsert(IEnumerable<TEntity> rows)
        {
            TableBatchOperation batchOperation = new TableBatchOperation();
            
            foreach (var entity in rows)
            {
               batchOperation.InsertOrMerge(entity);
            }

            await _table.ExecuteBatchAsync(batchOperation);
        }

        public async Task InsertOrMerge(TEntity record)
        {
            TableOperation insertOperation = TableOperation.InsertOrMerge(record);
            await _table.ExecuteAsync(insertOperation);
        }
    }
}
