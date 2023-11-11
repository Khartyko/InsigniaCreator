
using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Generators;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.TestingLibrary;
namespace Khartyko.InsigniaCreator.Domain.Testing.Generators;

public class AtlasGeneratorTests
{
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
		
		var generator = new AtlasGenerator();

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
		var cartographData = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph",
			Network = DataGenerator.GenerateSquareNetwork()
		};
		
		var cartographGenerator = new CartographGenerator();

		Cartograph cartograph = cartographGenerator.Generate(cartographData);
		
		var atlasData = new AtlasData
		{
			Name = "Atlas I",
			Width = 1280,
			Height = 800,
			Background = DataGenerator.GenerateRandomColor(),
			Cartograph = cartograph
		};
		
		var atlasGenerator = new AtlasGenerator();

		Atlas atlas = atlasGenerator.Generate(atlasData);
		Assert.NotNull(atlas);
		Assert.Equal(atlasData.Name, atlas.Name);
		Assert.Equal(atlasData.Width, atlas.Width);
		Assert.Equal(atlasData.Height, atlas.Height);
		Assert.Equal(atlasData.Background, atlas.BackgroundColor);
		Assert.Equal(1, atlas.Cartographs.Count);
		Assert.Equal(cartograph, atlas.Cartographs.First());
	}

	[Fact]
	public void Generate_Valid_CartographsOnly_Succeeds()
	{
		var cartographData1 = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph I",
			Network = DataGenerator.GenerateSquareNetwork()
		};
		var cartographData2 = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph II",
			Network = DataGenerator.GenerateSquareNetwork()
		};
		
		var cartographGenerator = new CartographGenerator();

		Cartograph cartograph1 = cartographGenerator.Generate(cartographData1);
		Cartograph cartograph2 = cartographGenerator.Generate(cartographData2);
		
		var atlasData = new AtlasData
		{
			Name = "Atlas I",
			Width = 1280,
			Height = 800,
			Background = DataGenerator.GenerateRandomColor(),
			Cartographs = new List<Cartograph>
			{
				cartograph1,
				cartograph2
			}
		};
		
		var atlasGenerator = new AtlasGenerator();

		Atlas atlas = atlasGenerator.Generate(atlasData);
		Assert.NotNull(atlas);
		Assert.Equal(atlasData.Name, atlas.Name);
		Assert.Equal(atlasData.Width, atlas.Width);
		Assert.Equal(atlasData.Height, atlas.Height);
		Assert.Equal(atlasData.Background, atlas.BackgroundColor);
		Assert.Equal(2, atlas.Cartographs.Count);
		Assert.Contains(atlasData.Cartographs[0], atlas.Cartographs);
		Assert.Contains(atlasData.Cartographs[1], atlas.Cartographs);
	}

	[Fact]
	public void Generate_Valid_BothWithNoOverlap_Succeeds()
	{
		var cartographData1 = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph I",
			Network = DataGenerator.GenerateSquareNetwork()
		};
		var cartographData2 = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph II",
			Network = DataGenerator.GenerateSquareNetwork()
		};
		var cartographData3 = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph III",
			Network = DataGenerator.GenerateSquareNetwork()
		};
		
		var cartographGenerator = new CartographGenerator();

		Cartograph cartograph1 = cartographGenerator.Generate(cartographData1);
		Cartograph cartograph2 = cartographGenerator.Generate(cartographData2);
		Cartograph cartograph3 = cartographGenerator.Generate(cartographData3);
		
		var atlasData = new AtlasData
		{
			Name = "Atlas I",
			Width = 1280,
			Height = 800,
			Background = DataGenerator.GenerateRandomColor(),
			Cartograph = cartograph3,
			Cartographs = new List<Cartograph>
			{
				cartograph1,
				cartograph2
			}
		};
		
		var atlasGenerator = new AtlasGenerator();

		Atlas atlas = atlasGenerator.Generate(atlasData);
		Assert.NotNull(atlas);
		Assert.Equal(atlasData.Name, atlas.Name);
		Assert.Equal(atlasData.Width, atlas.Width);
		Assert.Equal(atlasData.Height, atlas.Height);
		Assert.Equal(atlasData.Background, atlas.BackgroundColor);
		Assert.Equal(3, atlas.Cartographs.Count);
		Assert.Contains(atlasData.Cartograph, atlas.Cartographs);
		Assert.Contains(atlasData.Cartographs[0], atlas.Cartographs);
		Assert.Contains(atlasData.Cartographs[1], atlas.Cartographs);
	}

	[Fact]
	public void Generate_Valid_BothWithOverlap_Succeeds()
	{
		var cartographData1 = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph I",
			Network = DataGenerator.GenerateSquareNetwork()
		};
		var cartographData2 = new CartographData
		{
			AtlasId = 1L,
			Name = "Cartograph II",
			Network = DataGenerator.GenerateSquareNetwork()
		};
		
		var cartographGenerator = new CartographGenerator();

		Cartograph cartograph1 = cartographGenerator.Generate(cartographData1);
		Cartograph cartograph2 = cartographGenerator.Generate(cartographData2);
		
		var atlasData = new AtlasData
		{
			Name = "Atlas I",
			Width = 1280,
			Height = 800,
			Background = DataGenerator.GenerateRandomColor(),
			Cartograph = cartograph1,
			Cartographs = new List<Cartograph>
			{
				cartograph1,
				cartograph2
			}
		};
		
		var atlasGenerator = new AtlasGenerator();

		Atlas atlas = atlasGenerator.Generate(atlasData);
		Assert.NotNull(atlas);
		Assert.Equal(atlasData.Name, atlas.Name);
		Assert.Equal(atlasData.Width, atlas.Width);
		Assert.Equal(atlasData.Height, atlas.Height);
		Assert.Equal(atlasData.Background, atlas.BackgroundColor);
		Assert.Equal(2, atlas.Cartographs.Count);
		Assert.Contains(atlasData.Cartographs[0], atlas.Cartographs);
		Assert.Contains(atlasData.Cartographs[1], atlas.Cartographs);
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
		
		var generator = new AtlasGenerator();

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
		
		var generator = new AtlasGenerator();

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
		
		var generator = new AtlasGenerator();

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
		
		var generator = new AtlasGenerator();

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
		
		var generator = new AtlasGenerator();

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
		
		var generator = new AtlasGenerator();

		Assert.Throws<ArgumentException>(() => generator.Generate(data));
	}
}