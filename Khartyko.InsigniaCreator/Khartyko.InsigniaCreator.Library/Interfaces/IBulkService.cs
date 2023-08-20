namespace Khartyko.InsigniaCreator.Library.Interfaces;

public interface IBulkService<in TData, TEntity> : IService
    where TData : IEntityData
    where TEntity : IEntity
{
    IEntityDataValidator<TData> EntityDataValidator { get; }
    
    Task<IEnumerable<TEntity>> CreateBulkAsync(IEnumerable<TData> data);
    Task<IEnumerable<TEntity>> RetrieveBulkAsync(IEnumerable<long> id);
    Task<bool> UpdateBulkAsync(IEnumerable<long> id, IEnumerable<TData> dataUpdate);
    Task<bool> DeleteBulkAsync(IEnumerable<long> id);
}