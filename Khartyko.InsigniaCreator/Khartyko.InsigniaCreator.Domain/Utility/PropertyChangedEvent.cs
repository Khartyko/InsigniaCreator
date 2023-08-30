/** \addtogroup Khartyko.InsigniaCreator.Domain
 * @{
 */

namespace Khartyko.InsigniaCreator.Domain.Utility;

/// <summary>
/// A delegate that allows the subscription for when a particular value is updated.
/// </summary>
/// <typeparam name="T">The type of item to watch.</typeparam>
public delegate void PropertyChangedEvent<in T>(T value);

/** @} */