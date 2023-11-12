/** \addtogroup DomainTesting
* @{
*/

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Generators;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.TestingLibrary;

#pragma warning disable CS8625

namespace Khartyko.InsigniaCreator.Domain.Testing.Generators;

public class CartographGeneratorTests
{
	private CartographGenerator CreateGenerator()
	{
		INetworkGenerator<TriangularNetworkData> triNetworkGenerator = new TriangularNetworkGenerator();
		INetworkGenerator<NetworkData> quadNetworkGenerator = new SquareNetworkGenerator();
		INetworkGenerator<HexagonalNetworkData> hexNetworkGenerator = new HexagonalNetworkGenerator();

		return new CartographGenerator(
			triNetworkGenerator,
			quadNetworkGenerator,
			hexNetworkGenerator
		);
	}

	[Fact]
	public void Generate_Succeeds()
	{
		var data = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph",
			NetworkData = DataGenerator.GenerateSquareNetworkData()
		};

		var generator = CreateGenerator();
		
		INetworkGenerator<NetworkData> quadNetworkGenerator = new SquareNetworkGenerator();
		TemplateNetwork expectedNetwork = quadNetworkGenerator.GenerateNetwork(data.NetworkData);

		Cartograph generatedCartograph = generator.Generate(data);

		const ulong expectedId = 1L;
		ulong actualId = generatedCartograph.Id;
		
		Assert.Equal(expectedId, actualId);
		Assert.Equal(data.Name, generatedCartograph.Name);
		Assert.Equal(expectedNetwork, generatedCartograph.Template);
	}

	[Fact]
	public void Generate_NullData_Fails()
	{
		CartographGenerator generator = CreateGenerator();

		Assert.Throws<ArgumentNullException>(() => generator.Generate(null));
	}

	[Fact]
	public void Generator_InvalidAtlasId_Fails()
	{
		var data = new CartographData
		{
			Name = "Cartograph",
			NetworkData = DataGenerator.GenerateSquareNetworkData()
		};

		CartographGenerator generator = CreateGenerator();

		Assert.Throws<ArgumentOutOfRangeException>(() => generator.Generate(data));
	}

	[Fact]
	public void Generator_NullName_Fails()
	{
		var data = new CartographData
		{
			AtlasId = 1L,
			NetworkData = DataGenerator.GenerateSquareNetworkData()
		};

		CartographGenerator generator = CreateGenerator();

		Assert.Throws<ArgumentNullException>(() => generator.Generate(data));
	}

	[Fact]
	public void Generator_EmptyName_Fails()
	{
		var data = new CartographData
		{
			AtlasId = 1L,
			Name = "",
			NetworkData = DataGenerator.GenerateSquareNetworkData()
		};

		CartographGenerator generator = CreateGenerator();

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

		CartographGenerator generator = CreateGenerator();

		Assert.Throws<ArgumentNullException>(() => generator.Generate(data));
	}
}

/** @} */