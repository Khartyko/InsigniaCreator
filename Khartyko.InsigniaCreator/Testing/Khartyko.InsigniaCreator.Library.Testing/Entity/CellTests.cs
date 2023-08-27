/** \addtogroup LibraryTests
 * @{
 */
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;

#pragma warning disable CS0219, CS8600, CS8601, CS8604

namespace Khartyko.InsigniaCreator.Library.Testing.Entity;

public class CellTests
{
    private static readonly IList<Node> s_nodes = new List<Node>
    {
        new(Vector2.Zero),
        new(Vector2.One),
        new(Vector2.One * 2)
    };

    private static readonly IList<Link> s_links = new List<Link>
    {
        new(s_nodes[0], s_nodes[1]),
        new(s_nodes[1], s_nodes[2]),
        new(s_nodes[2], s_nodes[0])
    };

    #region Nodes

    [Fact]
    public void Nodes_Get_Succeeds()
    {
        var cell = new Cell(s_nodes, s_links);

        Assert.Equal(s_nodes.Count, cell.Nodes.Count);

        foreach (Node node in s_nodes)
        {
            Assert.Contains(node, cell.Nodes);
        }
    }

    #endregion Nodes

    #region Links

    [Fact]
    public void Links_Get_Succeeds()
    {
        var cell = new Cell(s_nodes, s_links);

        Assert.Equal(s_links.Count, cell.Links.Count);

        foreach (Link link in s_links)
        {
            Assert.Contains(link, cell.Links);
        }
    }

    #endregion Links

    #region Constructor

    #region From Nodes and Links

    [Fact]
    public void ConstructCell_FromNodesAndLinks_Succeeds()
    {
        var cell = new Cell(s_nodes, s_links);

        Assert.NotNull(cell);
    }

    [Fact]
    public void ConstructCell_FromNodesAndLinks_EmptyNodes_Fails()
    {
        var nodes = new List<Node>();

        Assert.Throws<ArgumentException>(() => new Cell(nodes, s_links));
    }

    [Fact]
    public void ConstructCell_FromNodesAndLinks_EmptyLinks_Fails()
    {
        var links = new List<Link>();

        Assert.Throws<ArgumentException>(() => new Cell(s_nodes, links));
    }

    [Fact]
    public void ConstructCell_FromNodesAndLinks_TooFewNodes_Fails()
    {
        List<Node> nodes = s_nodes.Take(s_nodes.Count - 1).ToList();

        Assert.Throws<ArgumentException>(() => new Cell(nodes, s_links));
    }

    [Fact]
    public void ConstructCell_FromNodesAndLinks_TooFewLinks_Fails()
    {
        List<Link> links = s_links.Take(s_links.Count - 1).ToList();

        Assert.Throws<ArgumentException>(() => new Cell(s_nodes, links));
    }

    [Fact]
    public void ConstructCell_FromNodesAndLinks_DuplicateNodes_Fails()
    {
        List<Node> nodes = s_nodes.Select(node => new Node(node))
            .ToList();
        nodes.Add(new Node(s_nodes.First()));

        Assert.Throws<ArgumentException>(() => new Cell(nodes, s_links));
    }

    [Fact]
    public void ConstructCell_FromNodesAndLinks_DuplicateLinks_Fails()
    {
        List<Link> links = s_links.Select(link => new Link(link))
            .ToList();
        links.Add(new Link(s_links.First()));

        Assert.Throws<ArgumentException>(() => new Cell(s_nodes, links));
    }

    #endregion From Nodes and Links

    #region From an Existing Cell

    [Fact]
    public void ConstructCell_FromExistingCell_Succeeds()
    {
        var cell0 = new Cell(s_nodes, s_links);

        var cell1 = new Cell(s_nodes, s_links);

        Assert.True(cell0.Equals(cell1));
        Assert.True(cell1.Equals(cell0));
    }

    [Fact]
    public void ConstructCell_FromExistingCell_NullCell_Fails()
    {
        var cell = new Cell(s_nodes, s_links);
        Cell nullCell = null;

        Assert.False(cell.Equals(nullCell));
    }

    #endregion From an Existing Cell

    #endregion Constructor

    #region Contains

    [Fact]
    public void Contains_Nodes_Succeeds()
    {
        var cell = new Cell(s_nodes, s_links);

        var node = new Node(Vector2.Zero);

        Assert.True(cell.Contains(node));
    }

    [Fact]
    public void Contains_Nodes_Fails()
    {
        var cell = new Cell(s_nodes, s_links);

        Node nullNode = null;
        var testNode = new Node(Vector2.One * -1);

        Assert.False(cell.Contains(nullNode));
        Assert.False(cell.Contains(testNode));
    }

    [Fact]
    public void Contains_Links_Succeeds()
    {
        var cell = new Cell(s_nodes, s_links);

        var link = new Link(s_nodes[0], s_nodes[1]);

        Assert.True(cell.Contains(link));
    }

    [Fact]
    public void Contains_Links_Fails()
    {
        var cell = new Cell(s_nodes, s_links);

        Link nullLink = null;
        var testLink = new Link(
            new Node(Vector2.One * -1),
            new Node(Vector2.One * 2)
        );

        Assert.False(cell.Contains(nullLink));
        Assert.False(cell.Contains(testLink));
    }

    #endregion Contains

    #region Equals

    [Fact]
    public void Equals_Self_Succeeds()
    {
        var cell = new Cell(s_nodes, s_links);

        Assert.True(cell.Equals(cell));
    }

    [Fact]
    public void Equals_Similar_Succeeds()
    {
        IList<Node> nodes = new List<Node>
        {
            new(Vector2.Zero),
            new(Vector2.One),
            new(Vector2.One * 2)
        };

        IList<Link> links = new List<Link>
        {
            new(nodes[0], nodes[1]),
            new(nodes[1], nodes[2]),
            new(nodes[2], nodes[0])
        };

        var cell0 = new Cell(nodes, links);
        var cell1 = new Cell(s_nodes, s_links);

        Assert.True(cell0.Equals(cell1));
        Assert.True(cell1.Equals(cell0));
    }

    [Fact]
    public void Equals_NullCell_Fails()
    {
        Cell nullCell = null;
        var cell = new Cell(s_nodes, s_links);

        Assert.False(cell.Equals(nullCell));
    }

    [Fact]
    public void Equals_DissimilarCell_Fails()
    {
        IList<Node> nodes = new List<Node>
        {
            new(Vector2.Zero),
            new(Vector2.One * -1),
            new(Vector2.One * 5)
        };

        IList<Link> links = new List<Link>
        {
            new(nodes[0], nodes[1]),
            new(nodes[1], nodes[2]),
            new(nodes[2], nodes[0])
        };

        var cell0 = new Cell(nodes, links);
        var cell1 = new Cell(s_nodes, s_links);

        Assert.False(cell0.Equals(cell1));
        Assert.False(cell1.Equals(cell0));
    }

    #endregion Equals
}
/** @} */