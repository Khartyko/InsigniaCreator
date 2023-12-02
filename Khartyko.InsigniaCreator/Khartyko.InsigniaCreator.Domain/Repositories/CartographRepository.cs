/** \addtogroup Khartyko.InsigniaCreator.Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Domain.Repositories;

/// <summary>
/// Stores Cartographs and manages their CRUD operations.
/// </summary>
public class CartographRepository : IRepository<CartographData, Cartograph>
{
    private readonly Dictionary<ulong, Cartograph> _cartographs;
    private readonly IGenerator<CartographData, Cartograph> _generator;

    public CartographRepository(IGenerator<CartographData, Cartograph> generator)
    {
        AssertionHelper.NullCheck(generator, nameof(generator));

        _generator = generator;
        _cartographs = new Dictionary<ulong, Cartograph>();
    }

    /// <summary>
	/// Used to get an IEntity instance by its id.
	/// </summary>
	/// <param name="id">The unique ulong to identify an IEntity.</param>
	/// <returns>If found, the IEntity being searched for; otherwise null.</returns>
    public Cartograph? Retrieve(ulong id)
    {
	    return !_cartographs.ContainsKey(id)
		    ? null
		    : _cartographs[id];
    }

	/// <summary>
	/// Gets all of the IEntity instances stored.
	/// </summary>
	/// <returns>A collection of the IEntity instances that are currently stored.</returns>
    public IEnumerable<Cartograph> RetrieveAll()
    {
        return _cartographs.Values.AsEnumerable();
    }

	/// <summary>
	/// Creates an IEntity instance with its respective data.
	/// </summary>
	/// <param name="data">The data of the IEntity to use</param>
	/// <returns>The newly created IEntity instance.</returns>
    public Cartograph Create(CartographData data)
    {
        AssertionHelper.NullCheck(data, nameof(data));

        Cartograph cartograph = _generator.Generate(data);

        _cartographs[cartograph.Id] = cartograph;

        return cartograph;
    }

    /// <summary>
    /// Update an IEntity instance by its id with the given data.
    /// </summary>
    /// <param name="id">The unique ulong to identify an IEntity.</param>
    /// <param name="update">The new Cartograph value to update an existing Cartograph with.</param>
    /// <returns>True if the record was found and succeeded; false otherwise.</returns>
    public bool Update(ulong id, Cartograph update)
    {
        AssertionHelper.NullCheck(update, nameof(update));

        Cartograph? cartograph = Retrieve(id);

        if (cartograph is null)
        {
            return false;
        }

        _cartographs[id] = new Cartograph(id, update);

        return true;
    }

    /// <summary>
    /// Delete an IEntity by its unique id.
    /// </summary>
    /// <param name="id">The unique ulong to identify an IEntity.</param>
    /// <returns>True if the record was found and deleted; false otherwise.</returns>
    public bool Delete(ulong id)
    {
        return _cartographs.Remove(id);
    }
}

/** @} */