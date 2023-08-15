using Khartyko.InsigniaCreator.Library.Entity;
namespace Khartyko.InsigniaCreator.Library.Interfaces;

public interface IService
{
}

public interface IService<TData, TType> : IService
    where TData : IEntityData
    where TType : IIdBearer
{
    Task<TType> CreateAsync(TData data);
    Task<TType> GetByIdAsync(long id);
    Task<IEnumerable<TType>> GetByDataAsync(TData data);
    Task<IEnumerable<TType>> GetByIdsAsync(IEnumerable<long> ids);
    Task<IEnumerable<TType>> GetByDataAsync(IEnumerable<TData> datas);
    Task<IEnumerable<TType>> GetAllAsync();
    Task<bool> UpdateAsync(long id, TData updates);
    Task<bool> DeleteAsync(long id);
}