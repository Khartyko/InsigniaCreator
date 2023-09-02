/** \addtogroup Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Domain.Services;

public class CartographService
{
    /// <summary>
    /// Creates a Cartograph with the given data.
    /// </summary>
    /// <param name="data">The data to use for a new Cartograph.</param>
    /// <returns>The newly created Cartograph.</returns>
    public async Task<Cartograph> CreateAsync(CartographData data)
	{
		throw new NotImplementedException();
	}

    /// <summary>
    /// Gets the Cartograph by the specified id.
    /// </summary>
    /// <param name="cartographId">The id of the desired Cartograph.</param>
    /// <returns>The Cartograph instance if found; otherwise null.</returns>
    public async Task<Cartograph?> GetAsync(long cartographId)
	{
		throw new NotImplementedException();
	}

    /// <summary>
    /// Deletes a Cartograph by the specified id.
    /// </summary>
    /// <param name="cartographId">The id of the Cartograph.</param>
    /// <returns>True if the Cartograph was found and deleted; otherwise false.</returns>
    public async Task<bool> DeleteByIdAsync(long cartographId)
	{
		throw new NotImplementedException();
	}
}

/** @} */