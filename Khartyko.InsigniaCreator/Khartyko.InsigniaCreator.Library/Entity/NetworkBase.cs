using Khartyko.InsigniaCreator.Library.Utility.Helpers;
namespace Khartyko.InsigniaCreator.Library.Entity;

public abstract class NetworkBase
{
    public IList<Node> Nodes { get; }
    public IList<Link> Links { get; }
    public IList<Cell> Cells { get; }

    protected NetworkBase(IList<Node> nodes, IList<Link> links, IList<Cell> cells)
    {
        ObjectHelper.NullCheck(nodes, nameof(nodes));
        ObjectHelper.NullCheck(links, nameof(links));
        ObjectHelper.NullCheck(cells, nameof(cells));
        
        Nodes = nodes;
        Links = links;
        Cells = cells;
    }

    protected NetworkBase(NetworkBase existing)
    {
        ObjectHelper.NullCheck(existing, nameof(existing));

        Nodes = existing.Nodes.Select(node => new Node(node)).ToList();
        Links = existing.Links.Select(link => new Link(link)).ToList();
        Cells = existing.Cells.Select(cell => new Cell(cell)).ToList();
    }
}