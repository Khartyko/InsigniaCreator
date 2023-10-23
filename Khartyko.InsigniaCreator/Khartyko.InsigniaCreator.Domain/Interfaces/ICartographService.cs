/** \addtogroup Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Domain.Interfaces;

/// <summary>
/// Declares method to manage Cartographs.
/// </summary>
public interface ICartographService : ICallbackService<Cartograph>
{
    /// <summary>
    /// Creates a Cartograph with the given data.
    /// </summary>
    /// <param name="data">The data to use for a new Cartograph.</param>
    /// <returns>The newly created Cartograph.</returns>
    Task<Cartograph> CreateAsync(CartographData data);

    /// <summary>
    /// Gets the Cartograph by the specified id.
    /// </summary>
    /// <param name="cartographId">The id of the desired Cartograph.</param>
    /// <returns>The Cartograph instance if found; otherwise null.</returns>
    Task<Cartograph?> GetAsync(ulong cartographId);

    /// <summary>
    /// Deletes a Cartograph by the specified id.
    /// </summary>
    /// <param name="cartographId">The id of the Cartograph.</param>
    /// <returns>True if the Cartograph was found and deleted; otherwise false.</returns>
    Task<bool> DeleteByIdAsync(ulong cartographId);
}

/** @} */