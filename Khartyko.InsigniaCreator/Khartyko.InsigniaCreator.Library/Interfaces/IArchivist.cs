namespace Khartyko.InsigniaCreator.Library.Interfaces;

public interface IArchivist
{
    IConfiguration? Config { get; }

    void RegisterArchivist<T>(IJournal<T> archivist);
    
    void RegisterValidator<TData>(IEntityDataValidator<TData> validator)
        where TData : IEntityData;

    void RegisterDataAlchemist<TData, TEntity>(IDataAlchemist<TData, TEntity> dataAlchemist)
        where TData : IEntityData
        where TEntity : IEntity;

    void RegisterService<TData, TEntity>(IService<TData, TEntity> service)
        where TData : IEntityData
        where TEntity : IEntity;

    IJournal<T> GetJournal<T>();

    IEntityDataValidator<TData> GetValidator<TData>()
        where TData : IEntityData;
    
    IDataAlchemist<TData, TEntity> GetDataAlchemist<TData, TEntity>()
        where TData : IEntityData
        where TEntity : IEntity;
    
    IService<TData, TEntity> GetService<TData, TEntity>()
        where TData : IEntityData
        where TEntity : IEntity;
}