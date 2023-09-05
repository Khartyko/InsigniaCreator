/** \addtogroup LibraryTests
 * @{
 */

using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.TestingLibrary;

#pragma warning disable CS8600, CS8604, CS8601, CS8625

namespace Khartyko.InsigniaCreator.Library.Testing.Entity;

public class AtlasTests
{
	private static Atlas ConstructAtlas()
	{
		const ulong id = 1L;
		const string name = "Atlas";
		double width = DataGenerator.GenerateRandomDouble() + 1.0;
		double height = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);

		return new Atlas(id, name, width, height, rgbColor);
	}

	private static Cartograph ConstructCartograph()
	{
		const ulong id = 1L;
		const string name = "Cartograph #1";
		
		var nodes = new List<Node>
		{
			new(Vector2.Zero),
			new(new Vector2(-5, 5)),
			new(new Vector2(-5, -5))
		};

		var links = new List<Link>
		{
			new(nodes[0], nodes[1]),
			new(nodes[1], nodes[2]),
			new(nodes[2], nodes[0])
		};

		var cells = new List<Cell>
		{
			new(nodes, links)
		};

		var templateNetwork = new TemplateNetwork(nodes, links, cells);

		return new Cartograph(id, id, name, templateNetwork);
	}

	#region Name

	[Fact]
	public void Name_Get_Succeeds()
	{
		Atlas atlas = ConstructAtlas();
		
		Assert.Equal("Atlas", atlas.Name);
	}

	[Fact]
	public void Name_Set_Fails()
	{
		Atlas atlas = ConstructAtlas();

		const string newAtlasName = "Some other Atlas name";

		atlas.Name = newAtlasName;
		
		Assert.Equal(newAtlasName, atlas.Name);
	}

	[Fact]
	public void Name_Set_NullString_Fails()
	{
		Atlas atlas = ConstructAtlas();
		string previousName = atlas.Name;
		string nullString = null;

		Assert.Throws<ArgumentNullException>(() => atlas.Name = nullString);
		Assert.NotNull(atlas.Name);
		Assert.Equal(previousName, atlas.Name);
	}

	[Theory]
	[InlineData("\r")]
	[InlineData("\t")]
	[InlineData("\n")]
	[InlineData(" ")]
	[InlineData("    ")]
	public void Name_Set_Whitespace_Fails(string invalidName)
	{
		Atlas atlas = ConstructAtlas();

		Assert.Throws<ArgumentException>(() => atlas.Name = invalidName);
		Assert.NotEqual(invalidName, atlas.Name);
	}
	
	#endregion Name

	#region Width

	[Fact]
	public void Width_Get_Succeeds()
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const double width = 100;
		double height = DataGenerator.GenerateRandomDouble() + 1.0;
		const string name = "Atlas";
		var rgbColor = new RgbColor(0);

		var atlas = new Atlas(id, name, width, height, rgbColor);
		
		Assert.Equal(width, atlas.Width);
	}

	[Fact]
	public void Width_Set_Succeeds()
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const double boundary = 100;
		const string name = "Atlas";
		var rgbColor = new RgbColor(0);
		
		var atlas = new Atlas(id, name, boundary, boundary, rgbColor);

		const double newWidth = 1000.0;
		
		atlas.Width = newWidth;
		
		Assert.NotEqual(boundary, atlas.Width);
		Assert.Equal(newWidth, atlas.Width);
	}

	[Theory, ClassData(typeof(InvalidDoubleData))]
	public void Width_Set_InvalidWidth_Fails(double invalidWidth)
	{
		Atlas atlas = ConstructAtlas();
		double previousWidth = atlas.Width;
		
		Assert.Throws<ArgumentException>(() => atlas.Width = invalidWidth);
		Assert.Equal(previousWidth, atlas.Width);
	}

	[Theory]
	[InlineData(-1.0)]
	[InlineData(0.0)]
	public void Width_Set_WidthOutOfRange_Fails(double invalidWidth)
	{
		Atlas atlas = ConstructAtlas();
		double previousWidth = atlas.Width;
		
		Assert.Throws<ArgumentOutOfRangeException>(() => atlas.Width = invalidWidth);
		Assert.Equal(previousWidth, atlas.Width);
	}

	#endregion Width
	
	#region Height
	
	[Fact]
	public void Height_Get_Succeeds()
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        double width = DataGenerator.GenerateRandomDouble() + 1.0;
		const double height = 100.0;
		const string name = "Atlas";
		var rgbColor = new RgbColor(0);

		var atlas = new Atlas(id, name, width, height, rgbColor);
		
		Assert.Equal(height, atlas.Height);
	}

	[Fact]
	public void Height_Set_Succeeds()
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const double boundary = 100;
		const string name = "Atlas";
		var rgbColor = new RgbColor(0);
		
		var atlas = new Atlas(id, name, boundary, boundary, rgbColor);

		const double newHeight = 1000.0;

		atlas.Height = newHeight;
		
		Assert.NotEqual(boundary, atlas.Height);
		Assert.Equal(newHeight, atlas.Height);
	}

	[Theory, ClassData(typeof(InvalidDoubleData))]
	public void Height_Set_InvalidWidth_Fails(double invalidHeight)
	{
		Atlas atlas = ConstructAtlas();
		double previousHeight = atlas.Height;
		
		Assert.Throws<ArgumentException>(() => atlas.Width = invalidHeight);
		Assert.Equal(previousHeight, atlas.Height);
	}

	[Theory]
	[InlineData(-1.0)]
	[InlineData(0.0)]
	public void Height_Set_WidthOutOfBounds_Fails(double invalidHeight)
	{
		Atlas atlas = ConstructAtlas();
		double previousHeight = atlas.Height;
		
		Assert.Throws<ArgumentOutOfRangeException>(() => atlas.Width = invalidHeight);
		Assert.Equal(previousHeight, atlas.Height);
	}

	#endregion Height

	#region BackgroundColor

	[Fact]
	public void BackgroundColor_Get_Succeeds()
	{
		Atlas atlas = ConstructAtlas();

		RgbColor previousColor = atlas.BackgroundColor;

		var newColor = new RgbColor(255);
		
		atlas.BackgroundColor = newColor;
		
		Assert.NotEqual(previousColor, atlas.BackgroundColor);
		Assert.Equal(newColor, atlas.BackgroundColor);
	}

	[Fact]
	public void BackgroundColor_Set_Fails()
	{
		Atlas atlas = ConstructAtlas();

		RgbColor previousColor = atlas.BackgroundColor;
		RgbColor nullColor = null;

		Assert.Throws<ArgumentNullException>(() => atlas.BackgroundColor = nullColor);
		Assert.Equal(previousColor, atlas.BackgroundColor);
	}
	
	#endregion BackgroundColor

	#region Constructor

	#region No Cartographs

	[Fact]
	public void Construct_NoCartographs_Succeeds()
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double width = DataGenerator.GenerateRandomDouble() + 1.0;
		double height = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);

		var atlas = new Atlas(id, name, width, height, rgbColor);
		
		Assert.Equal(id, atlas.Id);
		Assert.Equal(width, atlas.Width);
		Assert.Equal(height, atlas.Height);
		Assert.Equal(name, atlas.Name);
		Assert.Equal(rgbColor, atlas.BackgroundColor);
		Assert.Empty(atlas.Cartographs);
	}

	[Theory, ClassData(typeof(InvalidDoubleData))]
	public void Construct_NoCartographs_InvalidBounds_Fails(double invalidBoundary)
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double validBoundary = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);
		
		Assert.Throws<ArgumentException>(() => new Atlas(id, name, invalidBoundary, validBoundary, rgbColor));
		Assert.Throws<ArgumentException>(() => new Atlas(id, name, validBoundary, invalidBoundary, rgbColor));
	}

	[Theory]
	[InlineData(-1.0)]
	[InlineData(0.0)]
	public void Construct_NoCartographs_BoundsOutOfRange_Fails(double invalidBoundary)
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double validBoundary = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);
		
		Assert.Throws<ArgumentOutOfRangeException>(() => new Atlas(id, name, invalidBoundary, validBoundary, rgbColor));
		Assert.Throws<ArgumentOutOfRangeException>(() => new Atlas(id, name, validBoundary, invalidBoundary, rgbColor));
	}

	[Fact]
	public void Construct_NoCartographs_NullBackgroundColor_Fails()
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double width = DataGenerator.GenerateRandomDouble() + 1.0;
		double height = DataGenerator.GenerateRandomDouble() + 1.0;
		RgbColor nullColor = null;

		Assert.Throws<ArgumentNullException>(() => new Atlas(id, name, width, height, nullColor));
	}

	#endregion No Cartographs

	#region Single Cartograph

	[Fact]
	public void Construct_FromSingleCartograph_Succeeds()
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double width = DataGenerator.GenerateRandomDouble() + 1.0;
		double height = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);

		Cartograph cartograph = ConstructCartograph();

		var atlas = new Atlas(id, name, width, height, rgbColor, cartograph);
		
		Assert.Equal(id, atlas.Id);
		Assert.Equal(width, atlas.Width);
		Assert.Equal(height, atlas.Height);
		Assert.Equal(name, atlas.Name);
		Assert.Equal(rgbColor, atlas.BackgroundColor);
		Assert.Contains(cartograph, atlas.Cartographs);
	}

	[Theory, ClassData(typeof(InvalidDoubleData))]
	public void Construct_FromSingleCartograph_InvalidBounds_Fails(double invalidBoundary)
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double validBoundary = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);
		
		Cartograph cartograph = ConstructCartograph();

		Assert.Throws<ArgumentException>(() => new Atlas(id, name, invalidBoundary, validBoundary, rgbColor, cartograph));
		Assert.Throws<ArgumentException>(() => new Atlas(id, name, validBoundary, invalidBoundary, rgbColor, cartograph));
	}

	[Theory]
	[InlineData(-1.0)]
	[InlineData(0.0)]
	public void Construct_FromSingleCartograph_BoundsOutOfRange_Fails(double invalidBoundary)
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double validBoundary = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);
		
		Cartograph cartograph = ConstructCartograph();

		Assert.Throws<ArgumentOutOfRangeException>(() => new Atlas(id, name, invalidBoundary, validBoundary, rgbColor, cartograph));
		Assert.Throws<ArgumentOutOfRangeException>(() => new Atlas(id, name, validBoundary, invalidBoundary, rgbColor, cartograph));
	}

	[Fact]
	public void Construct_FromSingleCartograph_NullBackgroundColor_Fails()
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double width = DataGenerator.GenerateRandomDouble() + 1.0;
		double height = DataGenerator.GenerateRandomDouble() + 1.0;
		RgbColor nullColor = null;

		Assert.Throws<ArgumentNullException>(() => new Atlas(id, name, width, height, nullColor));
	}

	[Fact]
	public void Construct_FromSingleCartograph_NullCartograph_Fails()
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double width = DataGenerator.GenerateRandomDouble() + 1.0;
		double height = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);

		Cartograph nullCartograph = null;

		Assert.Throws<ArgumentNullException>(() => new Atlas(id, name, width, height, rgbColor, nullCartograph));
	}

	#endregion Single Cartograph

	#region Multiple Cartographs

	[Fact]
	public void Construct_FromMultipleCartographs_Succeeds()
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double width = DataGenerator.GenerateRandomDouble() + 1.0;
		double height = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);

		IList<Cartograph> cartographs = new List<Cartograph>
		{
			ConstructCartograph()
		};

		var atlas = new Atlas(id, name, width, height, rgbColor, cartographs);
		
		Assert.Equal(id, atlas.Id);
		Assert.Equal(width, atlas.Width);
		Assert.Equal(height, atlas.Height);
		Assert.Equal(name, atlas.Name);
		Assert.Equal(rgbColor, atlas.BackgroundColor);
		Assert.Equal(cartographs, atlas.Cartographs);
	}

	[Theory, ClassData(typeof(InvalidDoubleData))]
	public void Construct_FromMultipleCartographs_InvalidBounds_Fails(double invalidBoundary)
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double validBoundary = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);
		
		IList<Cartograph> cartographs = new List<Cartograph>
		{
			ConstructCartograph()
		};

		Assert.Throws<ArgumentException>(() => new Atlas(id, name, invalidBoundary, validBoundary, rgbColor, cartographs));
		Assert.Throws<ArgumentException>(() => new Atlas(id, name, validBoundary, invalidBoundary, rgbColor, cartographs));
	}

	[Theory]
	[InlineData(-1.0)]
	[InlineData(0.0)]
	public void Construct_FromMultipleCartographs_OutOfRangeBounds_Fails(double invalidBoundary)
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double validBoundary = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);
		
		IList<Cartograph> cartographs = new List<Cartograph>
		{
			ConstructCartograph()
		};

		Assert.Throws<ArgumentOutOfRangeException>(() => new Atlas(id, name, invalidBoundary, validBoundary, rgbColor, cartographs));
		Assert.Throws<ArgumentOutOfRangeException>(() => new Atlas(id, name, validBoundary, invalidBoundary, rgbColor, cartographs));
	}

	[Fact]
	public void Construct_FromMultipleCartographs_NullBackgroundColor_Fails()
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double width = DataGenerator.GenerateRandomDouble() + 1.0;
		double height = DataGenerator.GenerateRandomDouble() + 1.0;
		RgbColor nullColor = null;

		IList<Cartograph> cartographs = new List<Cartograph>
		{
			ConstructCartograph()
		};

		Assert.Throws<ArgumentNullException>(() => new Atlas(id, name, width, height, nullColor));
	}

	[Fact]
	public void Construct_FromMultipleCartographs_NullCartographsList_Fails()
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double width = DataGenerator.GenerateRandomDouble() + 1.0;
		double height = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);
		IList<Cartograph> nullCartographList = null;

		Assert.Throws<ArgumentNullException>(() => new Atlas(id, name, width, height, rgbColor, nullCartographList));
	}

	[Fact]
	public void Construct_FromMultipleCartographs_EmptyCartographs_Fails()
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double width = DataGenerator.GenerateRandomDouble() + 1.0;
		double height = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);
		IList<Cartograph> cartographs = new List<Cartograph>();
		
		Assert.Throws<ArgumentException>(() => new Atlas(id, name, width, height, rgbColor, cartographs));
	}

	[Fact]
	public void Construct_FromMultipleCartographs_OneCartographIsNull_Fails()
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double width = DataGenerator.GenerateRandomDouble() + 1.0;
		double height = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);
		IList<Cartograph> cartographs = new List<Cartograph>
		{
			ConstructCartograph(),
			null
		};
		
		Assert.Throws<ArgumentNullException>(() => new Atlas(id, name, width, height, rgbColor, cartographs));
	}

	[Fact]
	public void Construct_FromMultipleCartographs_DuplicateIds_Fails()
    {
        ulong id = DataGenerator.GenerateRandomULong(100L);
        const string name = "Atlas";
		double width = DataGenerator.GenerateRandomDouble() + 1.0;
		double height = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);
		IList<Cartograph> cartographs = new List<Cartograph>
		{
			ConstructCartograph(),
			ConstructCartograph()
		};
		
		Assert.Throws<ArgumentException>(() => new Atlas(id, name, width, height, rgbColor, cartographs));
	}

	#endregion Multiple Cartographs

	#region From Existing

	[Fact]
	public void Construct_FromExisting_Succeeds()
	{
		Atlas firstAtlas = ConstructAtlas();
		
		var secondAtlas = new Atlas(2L, firstAtlas);
		
		Assert.Equal(firstAtlas, secondAtlas);
	}

	[Fact]
	public void Construct_FromExisting_NullAtlas_Fails()
	{
		Atlas nullAtlas = null;

		Assert.Throws<ArgumentNullException>(() => new Atlas(1L, nullAtlas));
	}
	
	#endregion From Existing

	#endregion Constructor

	#region Equals

	[Fact]
	public void Equals_Succeeds()
	{
		const long id = 1L;
		const string name = "Atlas";
		double width = DataGenerator.GenerateRandomDouble() + 1.0;
		double height = DataGenerator.GenerateRandomDouble() + 1.0;
		var rgbColor = new RgbColor(0);
		
		var atlas0 = new Atlas(id, name, width, height, rgbColor);
		var atlas1 = new Atlas(id, name, width, height, rgbColor);
		
		// ReSharper disable EqualExpressionComparison
		Assert.True(atlas0.Equals(atlas0));
		Assert.True(atlas1.Equals(atlas1));
		// ReSharper restore EqualExpressionComparison
		Assert.True(atlas0.Equals(atlas1));
		Assert.True(atlas1.Equals(atlas0));
	}

	[Fact]
	public void Equals_Null_Fails()
	{
		Atlas atlas = ConstructAtlas();
		Atlas nullAtlas = null;
		
		Assert.False(atlas.Equals(nullAtlas));
	}

	[Fact]
	public void Equals_Dissimilar_Fails()
	{
		Atlas atlas0 = ConstructAtlas();
		
		const long id = 2L;
		const string name = "Atlas II";
		double width = DataGenerator.GenerateRandomDouble(199) + 1.0;
		double height = DataGenerator.GenerateRandomDouble(199) + 1.0;
		var rgbColor = new RgbColor(127);

		var atlas1 = new Atlas(id, name, width, height, rgbColor);
		
		Assert.False(atlas0.Equals(atlas1));
		Assert.False(atlas1.Equals(atlas0));
	}

	[Fact]
	public void Equals_DissimilarType_Fails()
	{
		Atlas atlas = ConstructAtlas();
		var testObject = new object();
		
		Assert.False(atlas.Equals(testObject));
		Assert.False(testObject.Equals(atlas));
	}

	#endregion Equals
}

/** @} */