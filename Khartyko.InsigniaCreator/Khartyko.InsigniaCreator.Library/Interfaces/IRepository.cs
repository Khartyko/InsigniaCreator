using Khartyko.InsigniaCreator.Library.Entity;
namespace Khartyko.InsigniaCreator.Library.Interfaces;

public interface IRepository
{
}

public interface IRepository<TType, TData> : IRepository
    where TType : IIdBearer
    where TData : IEntityData
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