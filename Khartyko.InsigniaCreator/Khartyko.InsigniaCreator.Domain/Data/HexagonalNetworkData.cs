/** \addtogroup Domain
* @{
*/

namespace Khartyko.InsigniaCreator.Domain.Data;

/// <summary>
/// Represents the data for a TemplateNetwork with a hexagonal tessellating grid.
/// </summary>
public class HexagonalNetworkData : NetworkData
{
    /// <summary>
    /// Gets or sets whether the triangles will start with an offset hexagon or not.
    /// </summary>
    public bool StartOffset { get; set; }
}

/** @} */