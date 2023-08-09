using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659

namespace Khartyko.InsigniaCreator.Library.Entity;

public class Cell
{
    public IList<Node> Nodes { get; }
    public IList<Link> Links { get; }

    public Cell(IList<Node> nodes, IList<Link> links)
    {
        ObjectHelper.NullCheck(nodes, nameof(nodes));
        ObjectHelper.NullCheck(links, nameof(links));

        if (!nodes.Any())
        {
            throw ExceptionHelper.GenerateArgumentException(
                GetType(), 
                nameof(nodes), 
                "'nodes' collection is empty"
            );
        }

        if (!links.Any())
        {
            throw ExceptionHelper.GenerateArgumentException(
                GetType(), 
                nameof(links), 
                "'links' collection is empty"
            );
        }

        if (nodes.Count < 3)
        {
            throw ExceptionHelper.GenerateArgumentException(
                GetType(),
                nameof(links),
                $"'nodes' collection has less than 3 Nodes; (got: {string.Join(", ", nodes)})"
            );
        }

        if (links.Count < 3)
        {
            throw ExceptionHelper.GenerateArgumentException(
                GetType(),
                nameof(links),
                $"'links' collection has less than 3 Links; (got: {string.Join(", ", links)})"
            );
        }

        if (ContainsDuplicates(nodes))
        {
            var duplicates = nodes.Where(outerNode => nodes.Count(innerNode => innerNode.Equals(outerNode)) > 1)
                .ToList();

            throw ExceptionHelper.GenerateArgumentException(
                GetType(), 
                nameof(nodes), 
                $"'nodes' collection contains duplicates (got: {string.Join(", ", duplicates)})"
            );
        }

        if (ContainsDuplicates(links))
        {
            var duplicates = links.Where(outerLink => links.Count(innerLink => innerLink.Equals(outerLink)) > 1)
                .ToList();
            
            throw ExceptionHelper.GenerateArgumentException(
                GetType(), 
                nameof(links), 
                $"'links' collection contains duplicates (got: {string.Join(", ", duplicates)})"
            );
        }
        
        Nodes = nodes;
        Links = links;
    }

    public Cell(Cell existingCell)
    {
        ObjectHelper.NullCheck(existingCell, nameof(existingCell));

        Nodes = existingCell.Nodes.Select(node => new Node(node)).ToList();
        Links = existingCell.Links.Select(link => new Link(link)).ToList();
    }

    public bool Contains(Node node) => Nodes.Contains(node);
    public bool Contains(Link link) => Links.Contains(link);

    private static bool ContainsDuplicates(IList<Node> nodes) => nodes.Any(outerNode => nodes.Count(innerNode => innerNode.Equals(outerNode)) > 1);
    private static bool ContainsDuplicates(IList<Link> links) => links.Any(outerLink => links.Count(innerLink => innerLink.Equals(outerLink)) > 1);

    public override bool Equals(object? obj) => obj is Cell cell
                                                && Nodes.All(cell.Contains)
                                                && Links.All(cell.Contains);
}