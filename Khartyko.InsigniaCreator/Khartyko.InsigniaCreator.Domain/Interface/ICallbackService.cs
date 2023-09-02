/** \addtogroup Domain
* @{
*/

using Khartyko.InsigniaCreator.Domain.Utility;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Interfaces;

namespace Khartyko.InsigniaCreator.Domain.Interface;

public interface ICallbackService<TEntity>
    where TEntity : IEntity
{
    void RegisterCallback(Action<Cartograph?> callback, CallbackType callbackType);
    void RegisterCallback(Action<Cartograph?> callback, params CallbackType[] callbackTypes);
    void RemoveCallback(Action<Cartograph?> callback, CallbackType callbackType);
}

/** @} */