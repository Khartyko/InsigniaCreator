using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Testing.Utility;
namespace Khartyko.InsigniaCreator.Library.Testing.Entity;

#pragma warning disable CS0219, CS8600, CS8601, CS8604

public class ActiveNetworkTests
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

	private static readonly TemplateNetwork s_templateNetwork = new(s_nodes, s_links, s_cells);

	private static int GetRandomTemplateNetworkNodeIndex()
	{
		return DataGenerator.GenerateRandomInt(0, s_nodes.Count - 1);
	}
	
	#region Constructor

	[Fact]
	public void Construct_NoParameters_Succeeds()
	{
		var activeNetwork = new ActiveNetwork();
		
		Assert.Empty(activeNetwork.Nodes);
		Assert.Empty(activeNetwork.Links);
		Assert.Empty(activeNetwork.Cells);
	}

	[Fact]
	public void Construct_FromExisting_Empty_Succeeds()
	{
		var initialActiveNetwork = new ActiveNetwork();

		var duplicateActiveNetwork = new ActiveNetwork(initialActiveNetwork);
		
		Assert.Empty(duplicateActiveNetwork.Nodes);
		Assert.Empty(duplicateActiveNetwork.Links);
		Assert.Empty(duplicateActiveNetwork.Cells);
	}

	[Fact]
	public void Construct_FromExisting_Fails()
	{
		ActiveNetwork nullActiveNetwork = null;

		Assert.Throws<ArgumentNullException>(() => new ActiveNetwork(nullActiveNetwork));
	}
	
	#endregion Constructor

	#region Activate

	[Fact]
	public void Activate_Succeeds()
	{
		var activeNetwork = new ActiveNetwork();

		var nodeIndex = GetRandomTemplateNetworkNodeIndex();

		Node node = s_templateNetwork.Nodes[nodeIndex];
		
		Assert.Empty(activeNetwork.Nodes);
		Assert.True(activeNetwork.Activate(node));
		Assert.NotEmpty(activeNetwork.Nodes);

		Assert.True(node.Activated);
		Assert.Equal(node, activeNetwork.Nodes[0]);
	}

	[Fact]
	public void Activate_NullNode_Fails()
	{
		var activeNetwork = new ActiveNetwork();

		Node nullNode = null;
		
		Assert.Throws<ArgumentNullException>(() => activeNetwork.Activate(nullNode));
	}

	[Fact]
	public void Activate_NodeAlreadyActivated_Fails()
	{
		var activeNetwork = new ActiveNetwork();

		var nodeIndex = GetRandomTemplateNetworkNodeIndex();

		Node node = s_templateNetwork.Nodes[nodeIndex];
		
		Assert.Empty(activeNetwork.Nodes);
		Assert.True(activeNetwork.Activate(node));
		Assert.NotEmpty(activeNetwork.Nodes);
		
		Assert.False(activeNetwork.Activate(node));
		Assert.True(node.Activated);
		
		Assert.Equal(1, activeNetwork.Nodes.Count);
	}

	#endregion Activate
	
	#region Deactivate
	
	[Fact]
	public void Deactivate_Succeeds()
	{
		var activeNetwork = new ActiveNetwork();

		var nodeIndex = GetRandomTemplateNetworkNodeIndex();

		Node node = s_templateNetwork.Nodes[nodeIndex];
		
		Assert.Empty(activeNetwork.Nodes);
		Assert.True(activeNetwork.Activate(node));
		Assert.NotEmpty(activeNetwork.Nodes);

		Assert.True(node.Activated);
		Assert.Equal(node, activeNetwork.Nodes[0]);
		
		activeNetwork.Deactivate(node);
		Assert.False(node.Activated);
		Assert.Empty(activeNetwork.Nodes);
	}

	[Fact]
	public void Deactivate_NullNode_Fails()
	{
		var activeNetwork = new ActiveNetwork();

		Node nullNode = null;
		
		Assert.Throws<ArgumentNullException>(() => activeNetwork.Activate(nullNode));

		Assert.Empty(activeNetwork.Nodes);
	}

	#endregion Deactivate
}