/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659

namespace Khartyko.InsigniaCreator.Library.Entity;

/// <summary>
/// Represents the possible Nodes, Links, and Cells that can be activated
/// </summary>
public class TemplateNetwork : NetworkBase
{
    /// <summary>
    /// Constructs a Template Network with the given Nodes, Links, and Cells.
    /// </summary>
    /// <param name="nodes">The Nodes to include in the TemplateNetwork.</param>
    /// <param name="links">The Links to include in the TemplateNetwork.</param>
    /// <param name="cells">The Cells to include in the TemplateNetwork.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'nodes', 'links', or 'cells' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'nodes', 'links', or 'cells' is empty or contains duplicates.</exception>
    public TemplateNetwork(IList<Node> nodes, IList<Link> links, IList<Cell> cells)
        : base(
            Validate(nodes, nameof(nodes)),
            Validate(links, nameof(links)),
            Validate(cells, nameof(cells))
        )
    {
    }
    
    /// <summary>
    /// Constructs a copy of an existing TemplateNetwork.
    /// </summary>
    /// <param name="existing">The existing TemplateNetwork to duplicate.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'existing' is null.</exception>
    public TemplateNetwork(TemplateNetwork existing)
        : base(existing)
    {
    }

    /// <summary>
    /// Gets a Node at the specified position.
    /// </summary>
    /// <param name="position">The position to search a Node for.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'position' is null.</exception>
    /// <returns>A Node if it is found, otherwise null.</returns>
    public Node? GetNode(Vector2 position)
    {
        AssertionHelper.NullCheck(position, nameof(position));

        return Nodes.SingleOrDefault(node => position.Equals(node.Position));
    }

    /// <summary>
    /// An override of the default Equals method that checks if the object in question has the same data as this TemplateNetwork instance.
    /// </summary>
    /// <remarks>
    /// If the object is null, it'll return false.
    /// If the object is 'this', it'll return true.
    /// Otherwise, the values are compared outright.
    /// </remarks>
    /// <param name="obj">The object that is compared against this instance of an TemplateNetwork.</param>
    /// <returns>A boolean value as to whether 'obj' is equal to this instance of an TemplateNetwork.</returns>
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

        return base.Equals(obj);
    }

    /// <summary>
    /// Validates a collection, checking if a list is empty or not.
    /// </summary>
    /// <param name="list">The list of items to check.</param>
    /// <param name="descriptor">The name of the descriptor (for logging purposes).</param>
    /// <typeparam name="T">The type of the list.</typeparam>
    /// <exception cref="ArgumentNullException">Can be thrown if either 'list' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'list' is empty, 'list' has duplicates.</exception>
    /// <returns>The list passed as an argument.</returns>
    private static IList<T> Validate<T>(IList<T> list, string descriptor)
    {
        AssertionHelper.EmptyCheck(list, descriptor);
        AssertionHelper.DuplicatesCheck(list, nameof(descriptor));
        
        return list;
    }
}
/** @} */