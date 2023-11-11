/** \addtogroup DomainTesting
* @{
*/

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Generators;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.TestingLibrary;

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
		var data = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph",
			Network = DataGenerator.GenerateSquareNetwork()
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
			Network = DataGenerator.GenerateSquareNetwork()
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
			Network = DataGenerator.GenerateSquareNetwork()
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
			Network = DataGenerator.GenerateSquareNetwork()
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