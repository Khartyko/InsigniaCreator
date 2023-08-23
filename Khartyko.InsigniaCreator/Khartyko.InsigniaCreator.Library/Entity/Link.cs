using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659

namespace Khartyko.InsigniaCreator.Library.Entity;

public class Link
{
    private Node _head;
    private Node _tail;

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

    public Link(Node head, Node tail)
    {
        AssertionHelper.NullCheck(head, nameof(head));
        AssertionHelper.NullCheck(tail, nameof(tail));

        AssertionHelper.EqualCheck(head, tail, nameof(head), nameof(tail));
        
        _head = head;
        _tail = tail;
    }

    public Link(Link existingLink)
    {
        AssertionHelper.NullCheck(existingLink, nameof(existingLink));

        _head = new Node(existingLink.Head);
        _tail = new Node(existingLink.Tail);
    }

    public Link Reversed() => new(_tail, _head);

    public bool Contains(Node node) => Head.Equals(node) || Tail.Equals(node);

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

    public override string ToString() => $"{{ Head: {Head}, Tail: {Tail} }}";
}