using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659

namespace Khartyko.InsigniaCreator.Library.Entity;

public class ActiveNetwork : NetworkBase
{
    public ActiveNetwork()
        : base(
            new List<Node>(),
            new List<Link>(),
            new List<Cell>()
        )
    {
    }

    // ReSharper disable once SuggestBaseTypeForParameterInConstructor
    public ActiveNetwork(ActiveNetwork existing)
        : base(existing)
    {
    }

    public bool Activate(Node node)
    {
        AssertionHelper.NullCheck(node, nameof(node));

        var result = false;

        if (Nodes.Contains(node))
        {
            return result;
        }

        Nodes.Add(node);

        node.Activated = true;

        result = true;

        return result;
    }

    public void Deactivate(Node node)
    {
        AssertionHelper.NullCheck(node, nameof(node));

        bool result = Nodes.Remove(node);

        if (result)
        {
            node.Activated = false;
        }
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

        return base.Equals(obj);
    }
}