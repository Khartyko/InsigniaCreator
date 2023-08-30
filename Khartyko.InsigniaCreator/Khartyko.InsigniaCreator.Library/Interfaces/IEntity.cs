/** \addtogroup Library
 * @{
 */

namespace Khartyko.InsigniaCreator.Library.Interfaces;

/// <summary>
/// Interface that is used to expose an 'Id' to any of its children.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// A value that is used to identify any children of this interface.
    /// </summary>
	long Id { get; }
}

/** @} */