/** \addtogroup Domain
* @{
*/

using Khartyko.InsigniaCreator.Domain.Interface;
using Khartyko.InsigniaCreator.Library.Data;

namespace Khartyko.InsigniaCreator.Domain.Data;

/// <summary>
/// Represents data used for creating a Node.
/// </summary>
public class NodeData : IEntityData
{
    /// <summary>
    /// The position to use for a Node
    /// </summary>
    public Vector2? Position { get; set; }
}

/** @} */