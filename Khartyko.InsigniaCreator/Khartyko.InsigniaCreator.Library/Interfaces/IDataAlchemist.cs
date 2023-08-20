namespace Khartyko.InsigniaCreator.Library.Interfaces;

public interface IDataAlchemist<in TData, out TEntity> : IAlchemist<TData, TEntity>
    where TData : IEntityData
    where TEntity : IEntity
{
}