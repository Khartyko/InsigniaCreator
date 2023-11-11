﻿/** \addtogroup DomainTesting
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

	private TemplateNetwork GenerateNetwork()
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

		return new TemplateNetwork(nodes, links, cells);
	}
	
	[Fact]
	public void Generate_Succeeds()
	{
		var data = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph",
			Network = GenerateNetwork()
		};

		var generator = new CartographGenerator();

		Cartograph generatedCartograph = generator.Generate(data);

		const ulong expectedId = 1L;
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

	[Fact]
	public void Generator_InvalidAtlasId_Fails()
	{
		var data = new CartographData
		{
			Name = "Cartograph",
			Network = GenerateNetwork()
		};

		var generator = new CartographGenerator();

		Assert.Throws<ArgumentOutOfRangeException>(() => generator.Generate(data));
	}

	[Fact]
	public void Generator_NullName_Fails()
	{
		var data = new CartographData
		{
			AtlasId = 1L,
			Network = GenerateNetwork()
		};

		var generator = new CartographGenerator();

		Assert.Throws<ArgumentNullException>(() => generator.Generate(data));
	}

	[Fact]
	public void Generator_EmptyName_Fails()
	{
		var data = new CartographData
		{
			AtlasId = 1L,
			Name = "",
			Network = GenerateNetwork()
		};

		var generator = new CartographGenerator();

		Assert.Throws<ArgumentException>(() => generator.Generate(data));
	}
	
	[Fact]
	public void Generator_NullNetwork_Fails()
	{
		var data = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph"
		};

		var generator = new CartographGenerator();

		Assert.Throws<ArgumentNullException>(() => generator.Generate(data));
	}
}

/** @} */