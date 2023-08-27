/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659

namespace Khartyko.InsigniaCreator.Library.Entity;

public class Cell
{
    public IList<Node> Nodes { get; }
    public IList<Link> Links { get; }

    public Cell(IList<Node> nodes, IList<Link> links)
    {
        AssertionHelper.NullCheck(nodes, nameof(nodes));
        AssertionHelper.NullCheck(links, nameof(links));

        AssertionHelper.MinimumCountCheck(nodes, 3, nameof(nodes));
        AssertionHelper.MinimumCountCheck(links, 3, nameof(links));

        AssertionHelper.DuplicatesCheck(nodes, nameof(nodes));
        AssertionHelper.DuplicatesCheck(links, nameof(links));
        
        Nodes = nodes;
        Links = links;
    }

    public Cell(Cell existingCell)
    {
        AssertionHelper.NullCheck(existingCell, nameof(existingCell));

        Nodes = existingCell.Nodes.Select(node => new Node(node)).ToList();
        Links = existingCell.Links.Select(link => new Link(link)).ToList();
    }

    public bool Contains(Node node) => Nodes.Contains(node);
    public bool Contains(Link link) => Links.Contains(link);

    public override bool Equals(object? obj) => obj is Cell cell
                                                && Nodes.All(cell.Contains)
                                                && Links.All(cell.Contains);
}
/** @} */