using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Library.Interfaces;

public interface IAlchemist<TData, TType>
    where TData : IEntityData
    where TType : IIdBearer
{
    TType Alchemise(TData data);
}