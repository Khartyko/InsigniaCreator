/** \addtogroup Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Domain.Repositories;

/// <summary>
/// Stores Atlases and manages their CRUD operations.
/// </summary>
public class AtlasRepository : IRepository<AtlasData, Atlas>
{
	private readonly Dictionary<ulong, Atlas> _atlases;
	private readonly IGenerator<AtlasData, Atlas> _generator;
	
	public AtlasRepository(IGenerator<AtlasData, Atlas> generator)
	{
		AssertionHelper.NullCheck(generator, nameof(generator));
		
		_generator = generator;
		_atlases = new Dictionary<ulong, Atlas>();
	}
	
	/// <summary>
	/// Used to get an Atlas instance by its id.
	/// </summary>
	/// <param name="id">The unique ulong to identify an Atlas.</param>
	/// <returns>If found, the Atlas being searched for; otherwise null.</returns>
	public Atlas? Retrieve(ulong id)
	{
		return !_atlases.ContainsKey(id)
			? null
			: _atlases[id];
	}

	/// <summary>
	/// Gets all of the Atlas instances stored.
	/// </summary>
	/// <returns>A collection of the Atlas instances that are currently stored.</returns>
	public IEnumerable<Atlas> RetrieveAll()
	{
		return _atlases.Values.AsEnumerable();
	}
	
	/// <summary>
	/// Creates an Atlas instance with its respective data.
	/// </summary>
	/// <param name="data">The data of the Atlas to use</param>
	/// <returns>The newly created Atlas instance.</returns>
	public Atlas Create(AtlasData data)
	{
		AssertionHelper.NullCheck(data, nameof(data));

		Atlas atlas = _generator.Generate(data);

		_atlases[atlas.Id] = atlas;

		return atlas;
	}
	
	/// <summary>
    /// Update an Atlas instance by its id with the given data.
    /// </summary>
    /// <param name="id">The unique ulong to identify an Atlas.</param>
    /// <param name="update">The new Atlas to update the existing with</param>
    /// <returns>True if the record was found and succeeded; false otherwise.</returns>
	public bool Update(ulong id, Atlas update)
	{
		AssertionHelper.NullCheck(update, nameof(update));

		Atlas? atlas = Retrieve(id);

		if (atlas is null)
		{
			return false;
		}

		_atlases[id] = new Atlas(id, update);

		return true;
	}
	
	/// <summary>
    /// Delete an Atlas by its unique id.
    /// </summary>
    /// <param name="id">The unique ulong to identify an Atlas.</param>
    /// <returns>True if the record was found and deleted; false otherwise.</returns>
	public bool Delete(ulong id)
	{
		return _atlases.Remove(id);
	}
}

/** @} */