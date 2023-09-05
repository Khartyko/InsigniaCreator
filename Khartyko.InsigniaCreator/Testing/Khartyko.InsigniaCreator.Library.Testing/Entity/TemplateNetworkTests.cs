/** \addtogroup LibraryTests
 * @{
 */

using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Testing.Utility;
using Khartyko.InsigniaCreator.TestingLibrary;

#pragma warning disable CS0219, CS8600, CS8601, CS8604

namespace Khartyko.InsigniaCreator.Library.Testing.Entity;

public class TemplateNetworkTests
{
	private static readonly IList<Node> s_nodes = new List<Node>
	{
		new(new Vector2(1, 1)),
		new(new Vector2(-1, 1)),
		new(new Vector2(-1, -1)),
		new(new Vector2(1, -1))
	};

	private static readonly IList<Link> s_links = new List<Link>
	{
		new(s_nodes[0], s_nodes[1]),
		new(s_nodes[1], s_nodes[2]),
		new(s_nodes[2], s_nodes[3]),
		new(s_nodes[3], s_nodes[0])
	};

	private static readonly IList<Cell> s_cells = new List<Cell>
	{
		new(s_nodes, s_links)
	};

    #region Constructor
    
    [Fact]
    public void Construct_FromILists_Succeeds()
    {
	    var templateNetwork = new TemplateNetwork(s_nodes, s_links, s_cells);
	    
	    Assert.Equal(s_nodes, templateNetwork.Nodes);
	    Assert.Equal(s_links, templateNetwork.Links);
	    Assert.Equal(s_cells, templateNetwork.Cells);
    }

    [Fact]
    public void Construct_FromILists_NullLists_Fails()
    {
	    IList<Node> nullNodes = null;
	    IList<Link> nullLinks = null;
	    IList<Cell> nullCells = null;
	    
	    Assert.Throws<ArgumentNullException>(() => new TemplateNetwork(nullNodes, s_links, s_cells));
	    Assert.Throws<ArgumentNullException>(() => new TemplateNetwork(s_nodes, nullLinks, s_cells));
	    Assert.Throws<ArgumentNullException>(() => new TemplateNetwork(s_nodes, s_links, nullCells));
    }

    [Fact]
    public void Construct_FromILists_EmptyLists_Fails()
    {
	    var nodes = new List<Node>();
	    var links = new List<Link>();
	    var cells = new List<Cell>();

	    Assert.Throws<ArgumentException>(() => new TemplateNetwork(nodes, s_links, s_cells));
	    Assert.Throws<ArgumentException>(() => new TemplateNetwork(s_nodes, links, s_cells));
	    Assert.Throws<ArgumentException>(() => new TemplateNetwork(s_nodes, s_links, cells));
    }

    [Fact]
    public void Construct_FromExisting_Succeeds()
    {
	    var initialTemplateNetwork = new TemplateNetwork(s_nodes, s_links, s_cells);

	    var duplicateTemplateNetwork = new TemplateNetwork(initialTemplateNetwork);
	    
	    Assert.Equal(initialTemplateNetwork.Nodes, duplicateTemplateNetwork.Nodes);
	    Assert.Equal(initialTemplateNetwork.Links, duplicateTemplateNetwork.Links);
	    Assert.Equal(initialTemplateNetwork.Cells, duplicateTemplateNetwork.Cells);
    }

    [Fact]
    public void Construct_FromExisting_Fails()
    {
	    TemplateNetwork nullTemplateNetwork = null;

	    Assert.Throws<ArgumentNullException>(() => new TemplateNetwork(nullTemplateNetwork));
    }
    
    #endregion Constructor

    #region GetNode

    [Fact]
    public void GetNode_Succeeds()
    {
	    var templateNetwork = new TemplateNetwork(s_nodes, s_links, s_cells);

	    int subjectIndex = DataGenerator.GenerateRandomInt(0, s_nodes.Count - 1);
	    Vector2 subjectPosition = s_nodes[subjectIndex].Position;

	    Node? node = templateNetwork.GetNode(subjectPosition);
	    
	    Assert.NotNull(node);
	    Assert.Equal(subjectPosition, node.Position);
	    Assert.Equal(s_nodes[subjectIndex], node);
    }

    [Fact]
    public void GetNode_NullVector_Fails()
    {
	    var templateNetwork = new TemplateNetwork(s_nodes, s_links, s_cells);
	    Vector2 nullVector = null;
	    
	    Assert.Throws<ArgumentNullException>(() => templateNetwork.GetNode(nullVector));
    }

    [Fact]
    public void GetNode_DissimilarVector_Fails()
    {
	    var templateNetwork = new TemplateNetwork(s_nodes, s_links, s_cells);

	    Node? node = templateNetwork.GetNode(Vector2.Zero);
	    
	    Assert.Null(node);
    }

    #endregion GetNode

    #region Equals

    [Fact]
    public void Equals_Succeeds()
    {
	    var initialTemplateNetwork = new TemplateNetwork(s_nodes, s_links, s_cells);

	    var similarTemplateNetwork = new TemplateNetwork(s_nodes, s_links, s_cells);

	    Assert.True(initialTemplateNetwork.Equals(similarTemplateNetwork));
	    
	    // ReSharper disable EqualExpressionComparison
	    Assert.True(initialTemplateNetwork.Equals(initialTemplateNetwork));
	    Assert.True(similarTemplateNetwork.Equals(similarTemplateNetwork));
	    // ReSharper restore EqualExpressionComparison
    }

    [Fact]
    public void Equals_NullTemplateNetwork_Fails()
    {
	    var initialTemplateNetwork = new TemplateNetwork(s_nodes, s_links, s_cells);
	    TemplateNetwork nullTemplateNetwork = null;
	    
	    Assert.False(initialTemplateNetwork.Equals(nullTemplateNetwork));
    }

    [Fact]
    public void Equals_DissimilarTemplateNetwork_Fails()
    {
	    var initialTemplateNetwork = new TemplateNetwork(s_nodes, s_links, s_cells);

	    var nodes = new List<Node>
	    {
		    new(Vector2.Zero),
		    new(new Vector2(-5, 5)),
		    new(new Vector2(-5, -5))
	    };

	    var links = new List<Link>
	    {
		    new(s_nodes[0], s_nodes[1]),
		    new(s_nodes[1], s_nodes[2]),
		    new(s_nodes[2], s_nodes[0])
	    };

	    var cells = new List<Cell>
	    {
			new(nodes, links)
	    };

	    var dissimilarTemplateNetwork = new TemplateNetwork(nodes, links, cells);

	    Assert.False(initialTemplateNetwork.Equals(dissimilarTemplateNetwork));
    }

    #endregion Equals
}

/** @} */