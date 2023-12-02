/** \addtogroup Library
 * @{
 */

using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659

namespace Khartyko.InsigniaCreator.Library.Entity;

/// <summary>
/// Represents a collection of Nodes, Links, and Cells that are currently activated
/// </summary>
public class ActiveNetwork : NetworkBase
{
    /// <summary>
    /// Constructs an ActiveNetwork with no activated Nodes, Links, or Cells.
    /// </summary>
    public ActiveNetwork()
        : base(
            new List<Node>(),
            new List<Link>(),
            new List<Cell>()
        )
    {
    }

    /// <summary>
    /// Constructs an ActiveNetwork from an existing ActiveNetwork.
    /// </summary>
    /// <param name="existing">An existing ActiveNetwork instance.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'existing' is null.</exception>
    public ActiveNetwork(ActiveNetwork existing)
        : base(existing)
    {
    }

    /// <summary>
    /// Activates a Node, if it isn't already activated.
    /// </summary>
    /// <param name="node">The node to be activated</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'node' is null.</exception>
    /// <returns>A boolean value that denotes if the Node was successfully activated</returns>
    public bool Activate(Node node)
    {
        AssertionHelper.NullCheck(node, nameof(node));

        if (Nodes.Contains(node))
        {
            return false;
        }

        Nodes.Add(node);

        node.Activated = true;

        return true;
    }

    /// <summary>
    /// Deactivates a node, if is was previously activated.
    /// </summary>
    /// <param name="node">The node to be deactivated</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'node' is null.</exception>
    public void Deactivate(Node node)
    {
        AssertionHelper.NullCheck(node, nameof(node));

        bool result = Nodes.Remove(node);

        if (result)
        {
            node.Activated = false;
        }
    }
    
    /// <summary>
    /// An override of the default Equals method that checks if the object in question has the same data as this ActiveNetwork instance.
    /// </summary>
    /// <remarks>
    /// If the object is null, it'll return false.
    /// If the object is 'this', it'll return true.
    /// Otherwise, the values are compared outright.
    /// </remarks>
    /// <param name="obj">The object that is compared against this instance of an ActiveNetwork.</param>
    /// <returns>A boolean value as to whether 'obj' is equal to this instance of an ActiveNetwork.</returns>
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

        return obj is ActiveNetwork activeNetwork
               && Nodes.All(activeNetwork.Nodes.Contains)
               && Links.All(activeNetwork.Links.Contains)
               && Cells.All(activeNetwork.Cells.Contains);
    }
}

/** @} */