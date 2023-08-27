/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659

namespace Khartyko.InsigniaCreator.Library.Entity;

public class Node
{
    private Vector2 _position;
    public bool Activated { get; set; }

    public Vector2 Position
    {
        get => _position;
        set
        {
            AssertionHelper.NullCheck(value, nameof(value));
            
            _position = value;
        }
    }

    public Node(double x, double y)
    {
        AssertionHelper.InvalidDoubleCheck(x, nameof(x));
        AssertionHelper.InvalidDoubleCheck(y, nameof(y));

        _position = new Vector2(x, y);
    }

    public Node(Vector2 position)
    {
        AssertionHelper.NullCheck(position, nameof(position));

        _position = position;
    }

    public Node(Node existingNode)
    {
        AssertionHelper.NullCheck(existingNode, nameof(existingNode));

        _position = new Vector2(existingNode.Position);
    }

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

    public override string ToString() => $"{{ activated: {Activated}, position: {Position} }}";
}
/** @} */