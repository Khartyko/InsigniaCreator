/** \addtogroup Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Domain.Repositories;

/// <summary>
/// Stores Atlases and manages their CRUD operations.
/// </summary>
public class AtlasRepository : IRepository<AtlasData, Atlas>
{
	/// <summary>
	/// Used to get an Atlas instance by its id.
	/// </summary>
	/// <param name="id">The unique long to identify an Atlas.</param>
	/// <returns>If found, the Atlas being searched for; otherwise null.</returns>
	public async Task<Atlas?> RetrieveAsync(long id)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// Gets all of the Atlas instances stored.
	/// </summary>
	/// <returns>A collection of the Atlas instances that are currently stored.</returns>
	public async Task<IEnumerable<Atlas>> RetrieveAllAsync()
	{
		throw new NotImplementedException();
	}
	
	/// <summary>
	/// Creates an Atlas instance with its respective data.
	/// </summary>
	/// <param name="data">The data of the Atlas to use</param>
	/// <returns>The newly created Atlas instance.</returns>
	public async Task<Atlas> CreateAsync(AtlasData data)
	{
		throw new NotImplementedException();
	}
	
	/// <summary>
    /// Update an Atlas instance by its id with the given data.
    /// </summary>
    /// <param name="id">The unique long to identify an Atlas.</param>
    /// <param name="data">The data to update for a particular Atlas instance.</param>
    /// <returns>True if the record was found and succeeded; false otherwise.</returns>
	public async Task<bool> UpdateAsync(long id, AtlasData data)
	{
		throw new NotImplementedException();
	}
	
	/// <summary>
    /// Delete an Atlas by its unique id.
    /// </summary>
    /// <param name="id">The unique long to identify an Atlas.</param>
    /// <returns>True if the record was found and deleted; false otherwise.</returns>
	public async Task<bool> DeleteAsync(long id)
	{
		throw new NotImplementedException();
	}
}

/** @} */