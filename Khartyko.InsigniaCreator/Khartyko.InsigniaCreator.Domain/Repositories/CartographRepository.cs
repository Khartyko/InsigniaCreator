/** \addtogroup Khartyko.InsigniaCreator.Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Domain.Repositories;

/// <summary>
/// Stores Cartographs and manages their CRUD operations.
/// </summary>
public class CartographRepository : IRepository<CartographData, Cartograph>
{
    /// <summary>
	/// Used to get an IEntity instance by its id.
	/// </summary>
	/// <param name="id">The unique ulong to identify an IEntity.</param>
	/// <returns>If found, the IEntity being searched for; otherwise null.</returns>
    public async Task<Cartograph?> RetrieveAsync(ulong id)
    {
        throw new NotImplementedException();
    }

	/// <summary>
	/// Gets all of the IEntity instances stored.
	/// </summary>
	/// <returns>A collection of the IEntity instances that are currently stored.</returns>
    public async Task<IEnumerable<Cartograph>> RetrieveAllAsync()
    {
        throw new NotImplementedException();
    }

	/// <summary>
	/// Creates an IEntity instance with its respective data.
	/// </summary>
	/// <param name="data">The data of the IEntity to use</param>
	/// <returns>The newly created IEntity instance.</returns>
    public async Task<Cartograph> CreateAsync(CartographData data)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Update an IEntity instance by its id with the given data.
    /// </summary>
    /// <param name="id">The unique ulong to identify an IEntity.</param>
    /// <param name="data">The data to update for a particular IEntity instance.</param>
    /// <returns>True if the record was found and succeeded; false otherwise.</returns>
    public async Task<bool> UpdateAsync(ulong id, CartographData data)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Delete an IEntity by its unique id.
    /// </summary>
    /// <param name="id">The unique ulong to identify an IEntity.</param>
    /// <returns>True if the record was found and deleted; false otherwise.</returns>
    public async Task<bool> DeleteAsync(ulong id)
    {
        throw new NotImplementedException();
    }
}

/** @} */