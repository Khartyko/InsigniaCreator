/** \addtogroup MainApp
 * @{
 */

namespace Khartyko.InsigniaCreator.Domain.Utility;

/// <summary>
/// Represents type of action that can be subscribed to.
/// </summary>
public enum CallbackType
{
    Select,
    Deselect,
    Create,
    Update,
    Delete
}

/** @} */