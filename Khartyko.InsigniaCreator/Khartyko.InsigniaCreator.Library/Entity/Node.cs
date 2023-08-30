/** \addtogroup Library
 * @{
 */

using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659

namespace Khartyko.InsigniaCreator.Library.Entity;

/// <summary>
/// Represents a point that can be activated.
/// </summary>
public class Node
{
    private Vector2 _position;
    
    /// <summary>
    /// Gets or Sets the activation state of a Node.
    /// </summary>
    public bool Activated { get; set; }

    /// <summary>
    /// Gets or Sets the position of a Node.
    /// </summary>
    /// <exception cref="ArgumentNullException">Can be thrown if 'value' is null.</exception>
    public Vector2 Position
    {
        get => _position;
        set
        {
            AssertionHelper.NullCheck(value, nameof(value));
            
            _position = value;
        }
    }

    /// <summary>
    /// Constructs a Node from an 'x' and 'y' value.
    /// </summary>
    /// <param name="x">The 'X'-value to use.</param>
    /// <param name="y">The 'Y'-value to use.</param>
    /// <exception cref="ArgumentException">Can be thrown if either 'x' or 'y' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
    public Node(double x, double y)
    {
        AssertionHelper.InvalidDoubleCheck(x, nameof(x));
        AssertionHelper.InvalidDoubleCheck(y, nameof(y));

        _position = new Vector2(x, y);
    }

    /// <summary>
    /// Constructs a Node from a Vector2 position.
    /// </summary>
    /// <param name="position">The Vector2 position to use.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'position' is null.</exception>
    public Node(Vector2 position)
    {
        AssertionHelper.NullCheck(position, nameof(position));

        _position = position;
    }

    /// <summary>
    /// Constructs a copy of an existing Node.
    /// </summary>
    /// <param name="existingNode">The existing Node to duplicate.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'existingNode' is null.</exception>
    public Node(Node existingNode)
    {
        AssertionHelper.NullCheck(existingNode, nameof(existingNode));

        _position = new Vector2(existingNode.Position);
    }

    /// <summary>
    /// An override of the default Equals method that checks if the object in question has the same data as this Node instance.
    /// </summary>
    /// <remarks>
    /// If the object is null, it'll return false.
    /// If the object is 'this', it'll return true.
    /// Otherwise, the values are compared outright.
    /// </remarks>
    /// <param name="obj">The object that is compared against this instance of an Node.</param>
    /// <returns>A boolean value as to whether 'obj' is equal to this instance of an Node.</returns>
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

        return obj is Node node
               && Position.Equals(node.Position);
    }

    /// <summary>
    /// ToString override that returns a string containing the data of 'Activated' and 'Position'.
    /// </summary>
    /// <returns>A string containing the data of 'Activated' and 'Position'.</returns>
    public override string ToString() => $"{{ activated: {Activated}, position: {Position} }}";
}

/** @} */