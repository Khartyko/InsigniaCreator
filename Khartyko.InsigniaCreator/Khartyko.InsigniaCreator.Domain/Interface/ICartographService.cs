/** \addtogroup Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Domain.Interface;

/// <summary>
/// Declares method to manage Cartographs.
/// </summary>
public interface ICartographService : ICallbackService<Cartograph>
{
    Task<long> CreateAsync(CartographData data);

    Task<Cartograph?> GetAsync(long cartographId);

    Task<bool> DeleteByIdAsync(long cartographId);
}

/** @} */