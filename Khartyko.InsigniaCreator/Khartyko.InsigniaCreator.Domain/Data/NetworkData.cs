/** \addtogroup Domain
* @{
*/

using Khartyko.InsigniaCreator.Library.Data;

namespace Khartyko.InsigniaCreator.Domain.Data;

/// <summary>
/// Represents the data used to create a TemplateNetwork.
/// </summary>
public class NetworkData
{
    /// <summary>
    /// Gets or sets the width of a TemplateNetwork.
    /// </summary>
    public double Width { get; set; }

    /// <summary>
    /// Gets or sets the height of a TemplateNetwork.
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// Gets or sets whether a TemplateNetwork had Cells centered around the X-Axis.
    /// </summary>
    public bool CenterAlongXAxis { get; set; }

    /// <summary>
    /// Gets or sets whether a TemplateNetwork had Cells centered around the Y-Axis.
    /// </summary>
    public bool CenterAlongYAxis { get; set; }

    /// <summary>
    /// Gets or sets the number of Cells horizontally for a TemplateNetwork.
    /// </summary>
    public int HorizontalCellCount { get; set; }

    /// <summary>
    /// Gets or sets the number of Cells vertically for a TemplateNetwork.
    /// </summary>
    public int VerticalCellCount { get; set; }

    /// <summary>
    /// Gets or sets the transform all of the Cells will use in a TemplateNetwork.
    /// </summary>
    public Transform CellTransform { get; set; } = new();
}

/** @} */