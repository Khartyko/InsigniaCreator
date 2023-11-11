/** \addtogroup DomainTesting
* @{
*/

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Generators;
using Khartyko.InsigniaCreator.Library.Entity;

#pragma warning disable CS8625

namespace Khartyko.InsigniaCreator.Domain.Testing.Generators;

public class CartographGeneratorTests
{
	/*
	 * Generate:
	 * - Null Data
	 * - Valid data
	 */

	[Fact]
	public void Generate_Succeeds()
	{
		var nodes = new List<Node>
		{
			new(1, 1),
			new(1, -1),
			new(-1, -1),
			new(-1, 1)
		};

		var links = new List<Link>
		{
			new(nodes[0], nodes[1]),
			new(nodes[1], nodes[2]),
			new(nodes[2], nodes[3]),
			new(nodes[3], nodes[0])
		};

		var cells = new List<Cell>
		{
			new(nodes, links)
		};

		var data = new CartographData
		{
			AtlasId = 0L,
			Name = "Cartograph",
			Network = new TemplateNetwork(nodes, links, cells)
		};

		var generator = new CartographGenerator();

		Cartograph generatedCartograph = generator.Generate(data);

		const ulong expectedId = 0L;
		ulong actualId = generatedCartograph.Id;
		
		Assert.Equal(expectedId, actualId);
		Assert.Equal(data.Name, generatedCartograph.Name);
		Assert.Equal(data.Network, generatedCartograph.Template);
	}

	[Fact]
	public void Generate_NullData_Fails()
	{
		var generator = new CartographGenerator();

		Assert.Throws<ArgumentNullException>(() => generator.Generate(null));
	}
}

/** @} */