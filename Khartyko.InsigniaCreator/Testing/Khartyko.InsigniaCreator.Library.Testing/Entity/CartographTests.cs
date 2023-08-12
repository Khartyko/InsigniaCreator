using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Testing.Utility;

#pragma warning disable CS0219, CS8600, CS8601, CS8604

namespace Khartyko.InsigniaCreator.Library.Testing.Entity;

public class CartographTests
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

	private static Cartograph CreateTestCartograph(long? id = null, long? atlasId = null, string? cartographName = null)
	{
		long usedCartographId = id ?? DataGenerator.GenerateRandomLong(0, 10);
		long usedAtlasId = atlasId ?? DataGenerator.GenerateRandomLong(0, 10);
		string usedCartographName = cartographName ?? "CartographName";

		var cartograph = new Cartograph(usedCartographId, usedAtlasId, usedCartographName, s_templateNetwork);

		return cartograph;
	}
	
	#region Id

	[Fact]
	public void Id_Succeeds()
	{
		long id = DataGenerator.GenerateRandomLong(0, 10);
		
		Cartograph cartograph = CreateTestCartograph(id);
		
		Assert.Equal(id, cartograph.Id);
	}
	
	#endregion Id

	#region AtlasId

	[Fact]
	public void AtlasId_Succeeds()
	{
		int atlasId = DataGenerator.GenerateRandomInt(0, 10);
		
		Cartograph cartograph = CreateTestCartograph(atlasId: atlasId);
		
		Assert.Equal(atlasId, cartograph.AtlasId);
	}
	
	#endregion AtlasId

	#region IsActive

	[Fact]
	public void IsActive_Get_Succeeds()
	{
		Cartograph cartograph = CreateTestCartograph();

		Assert.True(cartograph.IsActive);
	}
	
	[Fact]
	public void IsActive_Set_Succeeds()
	{
		Cartograph cartograph = CreateTestCartograph();
		
		Assert.True(cartograph.IsActive);

		cartograph.IsActive = false;
		
		Assert.False(cartograph.IsActive);

		cartograph.IsActive = true;
		
		Assert.True(cartograph.IsActive);
	}
	
	#endregion IsActive

	#region Name

	[Fact]
	public void Name_Get_Succeeds()
	{
		var cartographName = "Some Cartograph Name";
		
		Cartograph cartograph = CreateTestCartograph(cartographName: cartographName);
		
		Assert.Equal(cartographName, cartograph.Name);
	}

	[Fact]
	public void Name_Set_Succeeds()
	{
		var cartographName = "A Cartograph Name";
		var newCartographName = "Another Cartograph Name";
		
		Cartograph cartograph = CreateTestCartograph(cartographName: cartographName);
		
		Assert.Equal(cartographName, cartograph.Name);

		cartograph.Name = newCartographName;
		
		Assert.NotEqual(cartographName, cartograph.Name);
		Assert.Equal(newCartographName, cartograph.Name);
	}

	[Fact]
	public void Name_Set_NullString_Fails()
	{
		var cartographName = "A Cartograph Name";
		string nullString = null;
		
		Cartograph cartograph = CreateTestCartograph(cartographName: cartographName);

		Assert.Throws<ArgumentNullException>(() => cartograph.Name = nullString);
		
		Assert.NotNull(cartograph.Name);
		Assert.Equal(cartographName, cartograph.Name);
	}

	[Fact]
	public void Name_Set_WhitespaceString_Fails()
	{
		var cartographName = "A Cartograph Name";
		
		Cartograph cartograph = CreateTestCartograph(cartographName: cartographName);

		Assert.Throws<ArgumentException>(() => cartograph.Name = string.Empty);
		Assert.Throws<ArgumentException>(() => cartograph.Name = "\t\n\r");
		
		Assert.Equal(cartographName, cartograph.Name);
	}
	
	#endregion Name

	#region TemplateNetwork

	[Fact]
	public void TemplateNetwork_Get_Succeeds()
	{
		Cartograph cartograph = CreateTestCartograph();
		
		Assert.True(s_templateNetwork.Equals(cartograph.Template));
	}

	#endregion TemplateNetwork

	#region ActiveNetwork

	[Fact]
	public void ActiveNetwork_Get_Succeeds()
	{
		Cartograph cartograph = CreateTestCartograph();
		var expectedActiveNetwork = new ActiveNetwork();

		Assert.Equal(expectedActiveNetwork, cartograph.Active);
	}

	#endregion ActiveNetwork

	#region Constructors

	#region From Values

	[Fact]
	public void Construct_FromValues_Succeeds()
	{
		
	}

	[Fact]
	public void Construct_FromValues_BadIds_Fails()
	{
		
	}

	[Fact]
	public void Construct_FromValues_NullString_Fails()
	{
		
	}

	[Fact]
	public void Construct_FromValues_WhitespaceString_Fails()
	{
		
	}

	[Fact]
	public void Construct_FromValues_NullTemplateNetwork_Fails()
	{
		
	}

	#endregion From Values

	#region From Existing Cartograph

	[Fact]
	public void Construct_FromExisting_Succeeds()
	{
		
	}

	[Fact]
	public void Construct_FromExisting_InvalidId_Fails()
	{
		
	}

	[Fact]
	public void Construct_FromExisting_NullCartograph_Fails()
	{
		
	}

	#endregion From Existing Cartograph
	
	#endregion Constructors

	#region Activate

	[Fact]
	public void Activate_Succeeds()
	{
		Cartograph cartograph = CreateTestCartograph();

		int targetNodeIndex = DataGenerator.GenerateRandomInt(0, s_nodes.Count - 1);

		Node targetNode = s_nodes[targetNodeIndex];

		bool activationResult = cartograph.Activate(targetNode.Position);
		
		Assert.True(activationResult);
		
		Assert.True(targetNode.Activated);
		Assert.NotEmpty(cartograph.Active.Nodes);
	}

	[Fact]
	public void Activate_NullVector_Fails()
	{
		Cartograph cartograph = CreateTestCartograph();
		Vector2 nullVector = null;

		Assert.Throws<ArgumentNullException>(() => cartograph.Activate(nullVector));

		Assert.Empty(cartograph.Active.Nodes);
	}

	[Fact]
	public void Activate_DissimilarVector_Fails()
	{
		Cartograph cartograph = CreateTestCartograph();

		Vector2 targetVector = Vector2.One * 10;

		bool activationResult = cartograph.Activate(targetVector);
		
		Assert.False(activationResult);
		
		Assert.Empty(cartograph.Active.Nodes);
	}
	
	#endregion Activate
}