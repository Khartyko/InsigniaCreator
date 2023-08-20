namespace Khartyko.InsigniaCreator.Library.Interfaces;

public interface IService
{
}

public interface IService<in TData, TEntity> : IService
    where TData : IEntityData
    where TEntity : IEntity
{
    IEntityDataValidator<TData> EntityDataValidator { get; }

    Task<TEntity> CreateAsync(TData data);
    Task<TEntity> RetrieveAsync(long id);
    Task<bool> UpdateAsync(long id, TData dataUpdate);
    Task<bool> DeleteAsync(long id);
}