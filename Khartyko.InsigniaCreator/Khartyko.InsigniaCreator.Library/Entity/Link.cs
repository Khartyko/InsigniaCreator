/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659

namespace Khartyko.InsigniaCreator.Library.Entity;

/// <summary>
/// Represents a line with a start and end, with both being a Node.
/// </summary>
public class Link
{
    private Node _head;
    private Node _tail;

    /// <summary>
    /// Gets or sets the starting point of a Link.
    /// </summary>
    /// <exception cref="ArgumentNullException">Can be thrown if 'value' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'value' is equal to 'Tail'.</exception>
    public Node Head
    {
        get => _head;
        set
        {
            AssertionHelper.NullCheck(value, nameof(value));
            AssertionHelper.EqualCheck(value, Tail, nameof(value), nameof(Tail));
            
            _head = value;
        }
    }

    /// <summary>
    /// Gets or sets the ending point of a Link.
    /// </summary>
    /// <exception cref="ArgumentNullException">Can be thrown if 'value' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'value' is equal to 'Head'.</exception>
    public Node Tail
    {
        get => _tail;
        set
        {
            AssertionHelper.NullCheck(value, nameof(value));
            AssertionHelper.EqualCheck(value, Head, nameof(value), nameof(Head));

            _tail = value;
        }
    }

    /// <summary>
    /// Constructs a Link with the given Head and Tail.
    /// </summary>
    /// <param name="head">The starting Node of the Link.</param>
    /// <param name="tail">The ending Node of the Link.</param>
    /// <exception cref="NullReferenceException">Can be thrown if either 'head' or 'tail' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'head' and 'tail' are the same Nodes.</exception>
    public Link(Node head, Node tail)
    {
        AssertionHelper.NullCheck(head, nameof(head));
        AssertionHelper.NullCheck(tail, nameof(tail));

        AssertionHelper.EqualCheck(head, tail, nameof(head), nameof(tail));
        
        _head = head;
        _tail = tail;
    }

    /// <summary>
    /// Constructs a copy of an existing Link.
    /// </summary>
    /// <param name="existingLink">The existing Link to duplicate.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'existingLink' is null.</exception>
    public Link(Link existingLink)
    {
        AssertionHelper.NullCheck(existingLink, nameof(existingLink));

        _head = new Node(existingLink.Head);
        _tail = new Node(existingLink.Tail);
    }

    /// <summary>
    /// Creates a reversed Link, with Head and Tail swapped.
    /// </summary>
    /// <returns>A Link that's the reverse of a Link.</returns>
    public Link Reversed() => new(_tail, _head);

    /// <summary>
    /// Checks if a Node is either the Head or Tail.
    /// </summary>
    /// <param name="node"></param>
    /// <returns>A boolean value denoting if a Link contains a Node.</returns>
    public bool Contains(Node node) => Head.Equals(node) || Tail.Equals(node);

    /// <summary>
    /// An override of the default Equals method that checks if the object in question has the same data as this Link instance.
    /// </summary>
    /// <remarks>
    /// If the object is null, it'll return false.
    /// If the object is 'this', it'll return true.
    /// Otherwise, the values are compared outright.
    /// </remarks>
    /// <param name="obj">The object that is compared against this instance of an Link.</param>
    /// <returns>A boolean value as to whether 'obj' is equal to this instance of an Link.</returns>
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

        return obj is Link link
               && Contains(link.Head)
               && Contains(link.Tail);
    }

    /// <summary>
    /// ToString override that returns a string containing the data of 'Head' and 'Tail'.
    /// </summary>
    /// <returns>A string containing the data of 'Head' and 'Tail'.</returns>
    public override string ToString() => $"{{ Head: {Head}, Tail: {Tail} }}";
}
/** @} */