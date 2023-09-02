/** \addtogroup Khartyko.InsigniaCreator.Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Domain.Repositories;

public class CartographRepository : IRepository<CartographData, Cartograph>
{
    public async Task<Cartograph?> RetrieveAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Cartograph>> RetrieveAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Cartograph> CreateAsync(CartographData data)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(long id, CartographData data)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }
}

/** @} */