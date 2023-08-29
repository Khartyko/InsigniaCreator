/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Utility.Helpers;
namespace Khartyko.InsigniaCreator.Library.Entity;

#pragma warning disable CS0659

/// <summary>
/// Base Network that holds a collection of Nodes, Links, and Cells.
/// </summary>
public abstract class NetworkBase
{
    /// <summary>
    /// Gets the Nodes of a Network.
    /// </summary>
    public IList<Node> Nodes { get; }
    
    /// <summary>
    /// Gets the Links of a Network.
    /// </summary>
    public IList<Link> Links { get; }
    
    /// <summary>
    /// Gets the Cells of a Network.
    /// </summary>
    public IList<Cell> Cells { get; }

    /// <summary>
    /// Constructs a Network with the provided nodes, links, and cells.
    /// </summary>
    /// <param name="nodes">The Nodes of the Network.</param>
    /// <param name="links">The Links of the Network.</param>
    /// <param name="cells">The Cells of the Network.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'nodes', 'links' or 'cells' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'nodes', 'links' or 'cells' are empty or contain duplicates.</exception>
    protected NetworkBase(IList<Node> nodes, IList<Link> links, IList<Cell> cells)
    {
        AssertionHelper.NullCheck(nodes, nameof(nodes));
        AssertionHelper.NullCheck(links, nameof(links));
        AssertionHelper.NullCheck(cells, nameof(cells));
        
        Nodes = nodes;
        Links = links;
        Cells = cells;
    }

    /// <summary>
    /// Constructs a copy of an existing Network.
    /// </summary>
    /// <param name="existing">The existing Network to duplicate.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'existing' is null.</exception>
    protected NetworkBase(NetworkBase existing)
    {
        AssertionHelper.NullCheck(existing, nameof(existing));

        Nodes = existing.Nodes.Select(node => new Node(node)).ToList();
        Links = existing.Links.Select(link => new Link(link)).ToList();
        Cells = existing.Cells.Select(cell => new Cell(cell)).ToList();
    }
}
/** @} */