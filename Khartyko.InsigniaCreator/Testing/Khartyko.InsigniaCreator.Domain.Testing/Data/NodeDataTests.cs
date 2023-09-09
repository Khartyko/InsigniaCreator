using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.TestingLibrary;
namespace Khartyko.InsigniaCreator.Domain.Testing.Data;

public class NodeDataTests
{
	[Fact]
	public void Position_Succeeds()
	{
		Vector2 position = DataGenerator.GenerateRandomVector2();
		
		var data = new NodeData
		{
			Position = position
		};
		
		Assert.Equal(position, data.Position);
	}

	[Fact]
	public void Position_Set_Null_Succeeds()
	{
		var data = new NodeData
		{
			Position = null
		};
		
		Assert.Null(data.Position);
	}
}