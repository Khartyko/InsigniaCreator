using Khartyko.InsigniaCreator.Library.Utility.Helpers;

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

    public ActiveNetwork(ActiveNetwork existing)
        : base(existing)
    {
    }

    public bool Activate(Node node)
    {
        ObjectHelper.NullCheck(node, nameof(node));

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
        ObjectHelper.NullCheck(node, nameof(node));

        var result = Nodes.Remove(node);

        if (result)
        {
            node.Activated = false;
        }
    }
}