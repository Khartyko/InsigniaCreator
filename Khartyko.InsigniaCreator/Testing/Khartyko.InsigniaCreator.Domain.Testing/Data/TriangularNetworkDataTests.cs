using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.TestingLibrary;

namespace Khartyko.InsigniaCreator.Domain.Testing.Data;

// TODO: Fill out these

public class TriangularNetworkDataTests
{
	[Fact]
	public void StartFlipped_ValidValue_Succeeds()
	{
		bool value = DataGenerator.GenerateRandomBool();

		var data = new TriangularNetworkData
		{
			StartFlipped = value
		};
		
		Assert.Equal(value, data.StartFlipped);
	}

	[Fact]
	public void StartFlipped_Null_Succeeds()
	{
		var data = new TriangularNetworkData();
		
		Assert.Null(data.StartFlipped);
	}

	[Fact]
	public void StartFlipped_NullToValidValue_Succeeds()
	{
		var data = new TriangularNetworkData();
		
		Assert.Null(data.StartFlipped);
		
		bool value = DataGenerator.GenerateRandomBool();

		data.StartFlipped = value;
		
		Assert.Equal(value, data.StartFlipped);
	}

	[Fact]
	public void StartFlipped_ValidValueToNull_Succeeds()
	{
		bool value = DataGenerator.GenerateRandomBool();

		var data = new TriangularNetworkData
		{
			StartFlipped = value
		};
		
		Assert.Equal(value, data.StartFlipped);

		data.StartFlipped = null;
		
		Assert.Null(data.StartFlipped);
	}
}