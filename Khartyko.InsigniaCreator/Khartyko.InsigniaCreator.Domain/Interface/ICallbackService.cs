/** \addtogroup Domain
* @{
*/

using Khartyko.InsigniaCreator.Domain.Utility;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Interfaces;

namespace Khartyko.InsigniaCreator.Domain.Interface;

/// <summary>
/// Declares methods to be used for Registering and Unregistering callbacks.
/// </summary>
/// <typeparam name="TEntity">The IEntity type that is to be passed when it's been updated.</typeparam>
public interface ICallbackService<TEntity>
    where TEntity : IEntity
{
    /// <summary>
    /// Registers the particular callback with the associate CallbackType.
    /// </summary>
    /// <param name="callback">The callback to register and call when the particular CallbackType is met.</param>
    /// <param name="callbackType"></param>
    void RegisterCallback(Action<TEntity?> callback, CallbackType callbackType);
    
    /// <summary>
    /// Registers the particular callback with the associate CallbackTypes.
    /// </summary>
    /// <param name="callback">The callback to register and call when the particular CallbackType is met.</param>
    /// <param name="callbackTypes"></param>
    void RegisterCallback(Action<TEntity?> callback, params CallbackType[] callbackTypes);
    
    /// <summary>
    /// Removes the callback with the associated CallbackType.
    /// </summary>
    /// <param name="callback">The callback to remove.</param>
    /// <param name="callbackType">The CallbackType that's associated with the callback.</param>
    void RemoveCallback(Action<TEntity?> callback, CallbackType callbackType);
}

/** @} */