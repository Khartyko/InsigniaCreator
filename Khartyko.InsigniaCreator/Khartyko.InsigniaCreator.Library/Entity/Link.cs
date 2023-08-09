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
            ObjectHelper.NullCheck(value, nameof(value));

            if (value.Equals(_tail))
            {
                throw ExceptionHelper.GenerateArgumentException(
                    GetType(),
                    nameof(value),
                    $"'value' cannot match 'Link.Tail' (got '{value}')"
                );
            }

            _head = value;
        }
    }

    public Node Tail
    {
        get => _tail;
        set
        {
            ObjectHelper.NullCheck(value, nameof(value));

            if (value.Equals(_head))
            {
                throw ExceptionHelper.GenerateArgumentException(
                    GetType(),
                    nameof(value),
                    $"'value' cannot match 'Link.Head' (got '{value}')"
                );
            }

            _tail = value;
        }
    }

    public Link(Node head, Node tail)
    {
        ObjectHelper.NullCheck(head, nameof(head));
        ObjectHelper.NullCheck(tail, nameof(tail));

        if (head.Equals(tail))
        {
            throw ExceptionHelper.GenerateArgumentException(
                GetType(),
                nameof(tail),
                $"'head' cannot match 'tail'; (got head: '{head}', tail: '{tail}' )"
            );
        }

        _head = head;
        _tail = tail;
    }

    public Link(Link existingLink)
    {
        ObjectHelper.NullCheck(existingLink, nameof(existingLink));

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