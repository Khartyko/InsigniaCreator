namespace Khartyko.InsigniaCreator.Library.Interfaces;

public interface IEntityDataValidator
{
}

public interface IEntityDataValidator<in TData> : IEntityDataValidator
    where TData : IEntityData
{
    void Validate(TData data);
}