/** \addtogroup Domain
* @{
*/

namespace Khartyko.InsigniaCreator.Domain.Data;

/// <summary>
/// Represents the data for a TemplateNetwork with a triangular tessellating grid.
/// </summary>
public class TriangularNetworkData : NetworkData
{
    /// <summary>
    /// Gets or sets whether the triangles will start flipped or not.
    /// </summary>
    public bool StartFlipped { get; set; }
}

/** @} */