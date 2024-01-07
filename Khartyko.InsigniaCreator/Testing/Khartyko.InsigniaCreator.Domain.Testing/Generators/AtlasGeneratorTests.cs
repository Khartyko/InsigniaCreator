
using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Generators;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Domain.NetworkCalculators;
using Khartyko.InsigniaCreator.Domain.NetworkGenerators;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.TestingLibrary;

namespace Khartyko.InsigniaCreator.Domain.Testing.Generators;

public class AtlasGeneratorTests
{
	private CartographGenerator CreateCartographGenerator()
    {
        var triangularCalculator = new TriangularNetworkCalculator();
        var squareCalculator = new SquareNetworkCalculator();
        var hexagonalCalculator = new HexagonalNetworkCalculator();

        INetworkGenerator<TriangularNetworkData> triNetworkGenerator = new TriangularNetworkGenerator(triangularCalculator);
        INetworkGenerator<NetworkData> quadNetworkGenerator = new SquareNetworkGenerator(squareCalculator);
        INetworkGenerator<HexagonalNetworkData> hexNetworkGenerator = new HexagonalNetworkGenerator(hexagonalCalculator);

        return new CartographGenerator(
			triNetworkGenerator,
			quadNetworkGenerator,
			hexNetworkGenerator
		);
	}

	private AtlasGenerator CreateAtlasGenerator()
	{
		var cartographGenerator = CreateCartographGenerator();

		return new AtlasGenerator(cartographGenerator);
	}
	
	[Fact]
	public void Generate_Valid_NeitherCartograph_Succeeds()
	{
		var data = new AtlasData
		{
			Name = "Atlas I",
			Width = 1280,
			Height = 800,
			Background = DataGenerator.GenerateRandomColor()
		};
		
		AtlasGenerator generator = CreateAtlasGenerator();

		Atlas atlas = generator.Generate(data);
		Assert.NotNull(atlas);
		Assert.Equal(data.Name, atlas.Name);
		Assert.Equal(data.Width, atlas.Width);
		Assert.Equal(data.Height, atlas.Height);
		Assert.Equal(data.Background, atlas.BackgroundColor);
		Assert.Empty(atlas.Cartographs);
	}

	[Fact]
	public void Generate_Valid_CartographOnly_Succeeds()
	{
		const double width = 1280;
		const double height = 720;
		
		var atlasData = new AtlasData
		{
			Name = "Atlas I",
			Width = width,
			Height = height,
			Background = DataGenerator.GenerateRandomColor(),
			CartographData = new CartographData
			{
				AtlasId = 1L,
				Name = "Cartograph I",
				NetworkData = new NetworkData
				{
					Width = width,
					Height = height,
					VerticalCentering = true,
					HorizontalCentering = true,
					HorizontalCellCount = 1,
					VerticalCellCount = 1,
					CellTransform = new Transform()
				}
			}
		};
		
		AtlasGenerator atlasGenerator = CreateAtlasGenerator();

		Atlas atlas = atlasGenerator.Generate(atlasData);
		Assert.NotNull(atlas);
		Assert.Equal(atlasData.Name, atlas.Name);
		Assert.Equal(atlasData.Width, atlas.Width);
		Assert.Equal(atlasData.Height, atlas.Height);
		Assert.Equal(atlasData.Background, atlas.BackgroundColor);
		Assert.Equal(1, atlas.Cartographs.Count);
	}

	[Fact]
	public void Generate_Valid_CartographsOnly_Succeeds()
	{
		const double width = 1280;
		const double height = 720;

		var cartographData1 = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph I",
			NetworkData = DataGenerator.GenerateSquareNetworkData()
		};
		var cartographData2 = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph II",
			NetworkData = DataGenerator.GenerateSquareNetworkData()
		};
		
		var atlasData = new AtlasData
		{
			Name = "Atlas I",
			Width = width,
			Height = height,
			Background = DataGenerator.GenerateRandomColor(),
			CartographDatas = new List<CartographData>
			{
				cartographData1,
				cartographData2
			}
		};

		CartographGenerator cartographGenerator = CreateCartographGenerator();
		AtlasGenerator atlasGenerator = CreateAtlasGenerator();

		Cartograph expectedCartograph1 = cartographGenerator.Generate(cartographData1);
		Cartograph expectedCartograph2 = cartographGenerator.Generate(cartographData2);

		Atlas atlas = atlasGenerator.Generate(atlasData);
		Assert.NotNull(atlas);
		Assert.Equal(atlasData.Name, atlas.Name);
		Assert.Equal(atlasData.Width, atlas.Width);
		Assert.Equal(atlasData.Height, atlas.Height);
		Assert.Equal(atlasData.Background, atlas.BackgroundColor);
		Assert.Equal(2, atlas.Cartographs.Count);
		Assert.Equal(expectedCartograph1, atlas.Cartographs[0]);
		Assert.Equal(expectedCartograph2, atlas.Cartographs[1]);
	}

	[Fact]
	public void Generate_Valid_BothWithNoOverlap_Succeeds()
	{
		var cartographData1 = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph I",
			NetworkData = DataGenerator.GenerateSquareNetworkData()
		};
		var cartographData2 = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph II",
			NetworkData = DataGenerator.GenerateSquareNetworkData()
		};
		var cartographData3 = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph III",
			NetworkData = DataGenerator.GenerateSquareNetworkData()
		};
		
		var atlasData = new AtlasData
		{
			Name = "Atlas I",
			Width = 1280,
			Height = 800,
			Background = DataGenerator.GenerateRandomColor(),
			CartographData = cartographData3,
			CartographDatas = new List<CartographData>
			{
				cartographData1,
				cartographData2
			}
		};
		
		CartographGenerator cartographGenerator = CreateCartographGenerator();
		AtlasGenerator atlasGenerator = CreateAtlasGenerator();

		Cartograph expectedCartograph1 = cartographGenerator.Generate(cartographData1);
		Cartograph expectedCartograph2 = cartographGenerator.Generate(cartographData2);
		Cartograph expectedCartograph3 = cartographGenerator.Generate(cartographData3);

		Atlas atlas = atlasGenerator.Generate(atlasData);
		Assert.NotNull(atlas);
		Assert.Equal(atlasData.Name, atlas.Name);
		Assert.Equal(atlasData.Width, atlas.Width);
		Assert.Equal(atlasData.Height, atlas.Height);
		Assert.Equal(atlasData.Background, atlas.BackgroundColor);
		Assert.Equal(3, atlas.Cartographs.Count);
		Assert.Equal(expectedCartograph1, atlas.Cartographs[0]);
		Assert.Equal(expectedCartograph2, atlas.Cartographs[1]);
		Assert.Equal(expectedCartograph3, atlas.Cartographs[2]);
	}

	[Fact]
	public void Generate_Valid_BothWithOverlap_Succeeds()
	{
		var cartographData1 = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph I",
			NetworkData = DataGenerator.GenerateSquareNetworkData()
		};
		var cartographData2 = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph II",
			NetworkData = DataGenerator.GenerateSquareNetworkData()
		};
		
		var atlasData = new AtlasData
		{
			Name = "Atlas I",
			Width = 1280,
			Height = 800,
			Background = DataGenerator.GenerateRandomColor(),
			CartographData = cartographData1,
			CartographDatas = new List<CartographData>
			{
				cartographData1,
				cartographData2
			}
		};
		
		CartographGenerator cartographGenerator = CreateCartographGenerator();
		AtlasGenerator atlasGenerator = CreateAtlasGenerator();

		Cartograph expectedCartograph1 = cartographGenerator.Generate(cartographData1);
		Cartograph expectedCartograph2 = cartographGenerator.Generate(cartographData2);

		Atlas atlas = atlasGenerator.Generate(atlasData);
		Assert.NotNull(atlas);
		Assert.Equal(atlasData.Name, atlas.Name);
		Assert.Equal(atlasData.Width, atlas.Width);
		Assert.Equal(atlasData.Height, atlas.Height);
		Assert.Equal(atlasData.Background, atlas.BackgroundColor);
		Assert.Equal(2, atlas.Cartographs.Count);
		Assert.Equal(expectedCartograph1, atlas.Cartographs[0]);
		Assert.Equal(expectedCartograph2, atlas.Cartographs[1]);
	}

	[Fact]
	public void Generate_NullName_Succeeds()
	{
		var data = new AtlasData
		{
			Width = 1280,
			Height = 800,
			Background = DataGenerator.GenerateRandomColor()
		};
		
		AtlasGenerator generator = CreateAtlasGenerator();

		Assert.Throws<ArgumentNullException>(() => generator.Generate(data));
	}

	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData("\r\t\n")]
	public void Generate_InvalidName_Succeeds(string name)
	{
		var data = new AtlasData
		{
			Name = name,
			Width = 1280,
			Height = 800,
			Background = DataGenerator.GenerateRandomColor()
		};
		
		AtlasGenerator generator = CreateAtlasGenerator();

		Assert.Throws<ArgumentException>(() => generator.Generate(data));
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	public void Generate_WidthOutOfRange_Succeeds(double width)
	{
		var data = new AtlasData
		{
			Name = "Atlas I",
			Width = width,
			Height = 800,
			Background = DataGenerator.GenerateRandomColor()
		};
		
		AtlasGenerator generator = CreateAtlasGenerator();

		Assert.Throws<ArgumentOutOfRangeException>(() => generator.Generate(data));
	}

	[Theory]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NaN)]
	[InlineData(double.NegativeInfinity)]
	public void Generate_InvalidWidth_Succeeds(double width)
	{
		var data = new AtlasData
		{
			Name = "Atlas I",
			Width = width,
			Height = 800,
			Background = DataGenerator.GenerateRandomColor()
		};
		
		AtlasGenerator generator = CreateAtlasGenerator();

		Assert.Throws<ArgumentException>(() => generator.Generate(data));
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	public void Generate_HeightOutOfRange_Succeeds(double height)
	{
		var data = new AtlasData
		{
			Name = "Atlas I",
			Width = 1280,
			Height = height,
			Background = DataGenerator.GenerateRandomColor()
		};
		
		AtlasGenerator generator = CreateAtlasGenerator();

		Assert.Throws<ArgumentOutOfRangeException>(() => generator.Generate(data));
	}

	[Theory]
	[InlineData(double.PositiveInfinity)]
	[InlineData(double.NaN)]
	[InlineData(double.NegativeInfinity)]
	public void Generate_InvalidHeight_Succeeds(double height)
	{
		var data = new AtlasData
		{
			Name = "Atlas I",
			Width = 1280,
			Height = height,
			Background = DataGenerator.GenerateRandomColor()
		};
		
		AtlasGenerator generator = CreateAtlasGenerator();

		Assert.Throws<ArgumentException>(() => generator.Generate(data));
	}
}