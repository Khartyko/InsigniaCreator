/** \addtogroup Domain
* @{
*/

using Khartyko.InsigniaCreator.Domain.Interface;

namespace Khartyko.InsigniaCreator.Domain.Data;

/// <summary>
/// Represents the data used for creating a Cell.
/// </summary>
public class CellData : IEntityData
{
    /// <summary>
    /// The data of the Nodes to use for the Cell.
    /// </summary>
    public NodeData[]? NodeDatas { get; set; }

    /// <summary>
    /// The data of the Links to use for the Cell.
    /// </summary>
    public LinkData[]? LinkDatas { get; set; }
}

/** @} */