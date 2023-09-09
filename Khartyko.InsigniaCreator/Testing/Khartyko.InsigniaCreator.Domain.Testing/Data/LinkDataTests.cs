using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.TestingLibrary;
namespace Khartyko.InsigniaCreator.Domain.Testing.Data;

// TODO: Fill out these

public class LinkDataTests
{
	#region HeadData

	[Fact]
	public void HeadData_Get_ValidValue_Succeeds()
	{
		Vector2 position = DataGenerator.GenerateRandomVector2();

		var headData = new NodeData
		{
			Position = position
		};

		var linkData = new LinkData
		{
			HeadData = headData
		};
		
		Assert.NotNull(linkData.HeadData);
		Assert.Null(linkData.TailData);
		Assert.NotNull(linkData.HeadData.Position);
		Assert.Equal(headData, linkData.HeadData);
	}

	[Fact]
	public void HeadData_Get_Null_Succeeds()
	{
		var linkData = new LinkData();
		
		Assert.Null(linkData.HeadData);
	}

	[Fact]
	public void HeadData_Set_ValidValue_Succeeds()
	{
		var linkData = new LinkData();
		
		Assert.Null(linkData.HeadData);

		Vector2 expectedPosition = DataGenerator.GenerateRandomVector2();

		linkData.HeadData = new NodeData
		{
			Position = expectedPosition
		};
		
		Assert.NotNull(linkData.HeadData);
		Assert.NotNull(linkData.HeadData.Position);
		Assert.Equal(expectedPosition, linkData.HeadData.Position);
	}

	[Fact]
	public void HeadData_Set_Null_Succeeds()
	{
		Vector2 position = DataGenerator.GenerateRandomVector2();

		var headData = new NodeData
		{
			Position = position
		};

		var linkData = new LinkData
		{
			HeadData = headData
		};
		
		Assert.Equal(headData, linkData.HeadData);

		linkData.HeadData = null;
		
		Assert.Null(linkData.HeadData);
	}

	#endregion HeadData

	#region TailData

	[Fact]
	public void TailData_Get_ValidValue_Succeeds()
	{
		Vector2 position = DataGenerator.GenerateRandomVector2();

		var tailData = new NodeData
		{
			Position = position
		};

		var linkData = new LinkData
		{
			TailData = tailData
		};
		
		Assert.NotNull(linkData.TailData);
		Assert.NotNull(linkData.TailData.Position);
		Assert.Equal(tailData, linkData.TailData);
	}

	[Fact]
	public void TailData_Get_Null_Succeeds()
	{
		var linkData = new LinkData();
		
		Assert.Null(linkData.TailData);
	}

	[Fact]
	public void TailData_Set_ValidValue_Succeeds()
	{
		var linkData = new LinkData();
		
		Assert.Null(linkData.TailData);

		Vector2 expectedPosition = DataGenerator.GenerateRandomVector2();

		linkData.TailData = new NodeData
		{
			Position = expectedPosition
		};
		
		Assert.NotNull(linkData.TailData);
		Assert.NotNull(linkData.TailData.Position);
		Assert.Equal(expectedPosition, linkData.TailData.Position);
	}

	[Fact]
	public void TailData_Set_Null_Succeeds()
	{
		Vector2 position = DataGenerator.GenerateRandomVector2();

		var tailData = new NodeData
		{
			Position = position
		};

		var linkData = new LinkData
		{
			TailData = tailData
		};
		
		Assert.Equal(tailData, linkData.TailData);

		linkData.TailData = null;
		
		Assert.Null(linkData.TailData);
	}

	#endregion TailData
}