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
public interface IRepository<in TData, TTarget>
	where TTarget : IEntity
{
	/// <summary>
	/// Used to get an IEntity instance by its id.
	/// </summary>
	/// <param name="id">The unique ulong to identify an IEntity.</param>
	/// <returns>If found, the IEntity being searched for; otherwise null.</returns>
	TTarget? Retrieve(ulong id);

	/// <summary>
	/// Gets all of the IEntity instances stored.
	/// </summary>
	/// <returns>A collection of the IEntity instances that are currently stored.</returns>
	IEnumerable<TTarget> RetrieveAll();

	/// <summary>
	/// Creates an IEntity instance with its respective data.
	/// </summary>
	/// <param name="data">The data of the IEntity to use</param>
	/// <returns>The newly created IEntity instance.</returns>
	TTarget Create(TData data);

    /// <summary>
    /// Update an IEntity instance by its id with the given data.
    /// </summary>
    /// <param name="id">The unique ulong to identify an IEntity.</param>
    /// <param name="update">The value to update an existing IEntity with.</param>
    /// <returns>True if the record was found and succeeded; false otherwise.</returns>
    bool Update(ulong id, TTarget update);

    /// <summary>
    /// Delete an IEntity by its unique id.
    /// </summary>
    /// <param name="id">The unique ulong to identify an IEntity.</param>
    /// <returns>True if the record was found and deleted; false otherwise.</returns>
    bool Delete(ulong id);
}

/** @} */