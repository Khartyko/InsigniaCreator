/** \addtogroup Khartyko.InsigniaCreator.Domain
 * @{
 */

using Khartyko.InsigniaCreator.Library.Interfaces;

namespace Khartyko.InsigniaCreator.Domain.Interfaces;

/// <summary>
/// Interface that exposes a contract of being able to generate an IEntity from an IEntityData instance.
/// </summary>
/// <typeparam name="TData">The IEntityData used in the generation.</typeparam>
/// <typeparam name="TEntity">The IEntity type to be generated.</typeparam>
public interface IGenerator<in TData, out TEntity>
	where TData : IEntityData
	where TEntity : IEntity
{
	/// <summary>
	/// Generates an IEntity from an IEntityData instance.
	/// </summary>
	/// <param name="data">The data used in the generation.</param>
	/// <returns>A newly generated IEntity instance.</returns>
	TEntity Generate(TData data);
}

/** @} */