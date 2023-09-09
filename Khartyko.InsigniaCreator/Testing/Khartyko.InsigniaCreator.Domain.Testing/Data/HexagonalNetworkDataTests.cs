/** \addtogroup DomainTesting
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.TestingLibrary;

namespace Khartyko.InsigniaCreator.Domain.Testing.Data;

public class HexagonalNetworkDataTests
{
	[Fact]
	public void StartOffset_ValidValue_Succeeds()
	{
		bool value = DataGenerator.GenerateRandomBool();

		var data = new HexagonalNetworkData
		{
			StartOffset = value
		};
		
		Assert.Equal(value, data.StartOffset);
	}

	[Fact]
	public void StartOffset_Null_Succeeds()
	{
		var data = new HexagonalNetworkData();
		
		Assert.Null(data.StartOffset);
	}

	[Fact]
	public void StartOffset_NullToValidValue_Succeeds()
	{
		var data = new HexagonalNetworkData();
		
		Assert.Null(data.StartOffset);
		
		bool value = DataGenerator.GenerateRandomBool();

		data.StartOffset = value;
		
		Assert.Equal(value, data.StartOffset);
	}

	[Fact]
	public void StartOffset_ValidValueToNull_Succeeds()
	{
		bool value = DataGenerator.GenerateRandomBool();

		var data = new HexagonalNetworkData
		{
			StartOffset = value
		};
		
		Assert.Equal(value, data.StartOffset);

		data.StartOffset = null;
		
		Assert.Null(data.StartOffset);
	}
}

/** @} */