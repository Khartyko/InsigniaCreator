/** \addtogroup Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Interface;

namespace Khartyko.InsigniaCreator.Domain.Data;

/// <summary>
/// Represents the data used for creating a Link.
/// </summary>
public class LinkData : IEntityData
{
    /// <summary>
    /// The data for the Head Node of the Link.
    /// </summary>
    public NodeData? HeadData { get; set; }

    /// <summary>
    /// The data for the Tail Node of the link.
    /// </summary>
    public NodeData? TailData { get; set; }
}

/** @} */