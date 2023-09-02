/** \addtogroup Domain
 * @{
 */

using Khartyko.InsigniaCreator.Library.Interfaces;

namespace Khartyko.InsigniaCreator.Domain.Interface;

/// <summary>
/// Interface that declares that a child is associated with the given IEntity type.
/// </summary>
/// <typeparam name="TEntity">The type of IEntity to associate the child with.</typeparam>
public interface IEntityData<TEntity>
    where TEntity : IEntity
{
}

/** @} */