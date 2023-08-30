/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659

namespace Khartyko.InsigniaCreator.Library.Entity;

/// <summary>
/// Represents a shape, with points and sides in the form of Nodes and Links.
/// </summary>
public class Cell
{
    /// <summary>
    /// Gets the Nodes associated with a Cell.
    /// </summary>
    public IList<Node> Nodes { get; }
    
    /// <summary>
    /// Gets the Links associated with a Cell.
    /// </summary>
    public IList<Link> Links { get; }

    /// <summary>
    /// Constructs a Cell with the given Nodes and Links.
    /// </summary>
    /// <param name="nodes">The Nodes associated with the Cell.</param>
    /// <param name="links">The Links associated with the Cell.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if either 'nodes' or 'links' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if either 'nodes' or 'links' is empty or has duplicates.</exception>
    public Cell(IList<Node> nodes, IList<Link> links)
    {
        AssertionHelper.NullCheck(nodes, nameof(nodes));
        AssertionHelper.NullCheck(links, nameof(links));

        AssertionHelper.MinimumCountCheck(nodes, 3, nameof(nodes));
        AssertionHelper.MinimumCountCheck(links, 3, nameof(links));

        AssertionHelper.DuplicatesCheck(nodes, nameof(nodes));
        AssertionHelper.DuplicatesCheck(links, nameof(links));
        
        Nodes = nodes;
        Links = links;
    }

    /// <summary>
    /// Constructs a copy of an existing Cell.
    /// </summary>
    /// <param name="existingCell">The existing Cell to duplicate.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'existingCell' is null.</exception>
    public Cell(Cell existingCell)
    {
        AssertionHelper.NullCheck(existingCell, nameof(existingCell));

        Nodes = existingCell.Nodes.Select(node => new Node(node)).ToList();
        Links = existingCell.Links.Select(link => new Link(link)).ToList();
    }

    /// <summary>
    /// Checks if the specified Node is found within the Nodes of a Cell.
    /// </summary>
    /// <param name="node">The Node to search for.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'node' is null.</exception>
    /// <returns>A boolean value that denotes whether a Cell has the specified Node.</returns>
    public bool Contains(Node node)
    {
        AssertionHelper.NullCheck(node, nameof(node));
        
        return Nodes.Contains(node);
    }

    /// <summary>
    /// Checks if the specified Link is found within the Nodes of a Cell.
    /// </summary>
    /// <param name="link">The Link to search for.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'links' is null.</exception>
    /// <returns>A boolean value that denotes whether a Cell has the specified Link.</returns>
    public bool Contains(Link link)
    {
        AssertionHelper.NullCheck(link, nameof(link));
        
        return Links.Contains(link);
    }

    /// <summary>
    /// An override of the default Equals method that checks if the object in question has the same data as this Cell instance.
    /// </summary>
    /// <remarks>
    /// If the object is null, it'll return false.
    /// If the object is 'this', it'll return true.
    /// Otherwise, the values are compared outright.
    /// </remarks>
    /// <param name="obj">The object that is compared against this instance of an Cell.</param>
    /// <returns>A boolean value as to whether 'obj' is equal to this instance of an Cell.</returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }
        
        return obj is Cell cell
               && Nodes.All(cell.Contains)
               && Links.All(cell.Contains);
    }
}
/** @} */