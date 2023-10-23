/** \addtogroup Domain
 * @{
 */

using Khartyko.InsigniaCreator.Library.Interfaces;

namespace Khartyko.InsigniaCreator.Domain.Interfaces;

/// <summary>
/// Stores and handles running CRUD operations on the IEntity type that it is registered with.
/// </summary>
/// <typeparam name="TData">The type of the data that will be used to create the IEntity instance.</typeparam>
/// <typeparam name="TTarget">The type of IEntity that will be stored and managed.</typeparam>
public interface IRepository<TData, TTarget>
	where TTarget : IEntity
{
	/// <summary>
	/// Used to get an IEntity instance by its id.
	/// </summary>
	/// <param name="id">The unique ulong to identify an IEntity.</param>
	/// <returns>If found, the IEntity being searched for; otherwise null.</returns>
	Task<TTarget?> RetrieveAsync(ulong id);

	/// <summary>
	/// Gets all of the IEntity instances stored.
	/// </summary>
	/// <returns>A collection of the IEntity instances that are currently stored.</returns>
	Task<IEnumerable<TTarget>> RetrieveAllAsync();

	/// <summary>
	/// Creates an IEntity instance with its respective data.
	/// </summary>
	/// <param name="data">The data of the IEntity to use</param>
	/// <returns>The newly created IEntity instance.</returns>
	Task<TTarget> CreateAsync(TData data);

    /// <summary>
    /// Update an IEntity instance by its id with the given data.
    /// </summary>
    /// <param name="id">The unique ulong to identify an IEntity.</param>
    /// <param name="data">The data to update for a particular IEntity instance.</param>
    /// <returns>True if the record was found and succeeded; false otherwise.</returns>
    Task<bool> UpdateAsync(ulong id, TData data);

    /// <summary>
    /// Delete an IEntity by its unique id.
    /// </summary>
    /// <param name="id">The unique ulong to identify an IEntity.</param>
    /// <returns>True if the record was found and deleted; false otherwise.</returns>
    Task<bool> DeleteAsync(ulong id);
}

/** @} */