/** \addtogroup Domain.Testing
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.NetworkCalculators;

#pragma warning disable CS8625

namespace Khartyko.InsigniaCreator.Domain.Testing.NetworkCalculators;

public class HexagonalNetworkCalculatorTests
{
	#region CalculateNodeCount

	[Theory]
	[InlineData(true, true, 1, 1, true, 6)]
	[InlineData(false, true, 1, 1, true, 6)]
	[InlineData(true, false, 1, 1, true, 6)]
	[InlineData(true, false, 1, 1, false, 10)]
	[InlineData(false, false, 1, 1, true, 13)]
	[InlineData(true, true, 5, 5, true, 62)]
	[InlineData(true, true, 5, 5, false, 66)]
	public void CalculateNodeCount_Succeeds(
		bool verticalCentering,
		bool horizontalCentering,
		int horizontalCellCount,
		int verticalCellCount,
		bool startOffset,
		int expectedCount
	)
	{
		var networkData = new HexagonalNetworkData
		{
			VerticalCentering = verticalCentering,
			HorizontalCentering = horizontalCentering,
			HorizontalCellCount = horizontalCellCount,
			VerticalCellCount = verticalCellCount,
			StartOffset = startOffset
		};
		
		var calculator = new HexagonalNetworkCalculator();

		int actualCount = calculator.CalculateNodeCount(networkData);
		
		Assert.Equal(expectedCount, actualCount);
	}

	[Fact]
	public void CalculateNodeCount_NullData_Fails()
	{
		var calculator = new HexagonalNetworkCalculator();

		Assert.Throws<ArgumentNullException>(() => calculator.CalculateNodeCount(null));
	}

	[Fact]
	public void CalculateNodeCount_InvalidHorizontalCellCount_Fails()
	{
		var networkData = new HexagonalNetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 0,
			VerticalCellCount = 2
		};
		
		var calculator = new HexagonalNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateNodeCount(networkData));
	}

	[Fact]
	public void CalculateNodeCount_InvalidVerticalCellCount_Fails()
	{
		var networkData = new HexagonalNetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 2,
			VerticalCellCount = 0
		};
		
		var calculator = new HexagonalNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateNodeCount(networkData));
	}

	#endregion CalculateNodeCount
	
	#region CalculateLinkCount

	[Theory]
	// For 1x1 grid counts
	[InlineData(true, true, 1, 1, true, 6)]
	[InlineData(true, true, 1, 1, false, 6)]
	[InlineData(true, false, 1, 1, true, 6)]
	[InlineData(true, false, 1, 1, false, 11)]
	[InlineData(false, true, 1, 1, true, 6)]
	[InlineData(false, true, 1, 1, false, 6)]
	[InlineData(false, false, 1, 1, true, 15)]
	[InlineData(false, false, 1, 1, false, 15)]
	// For 2x2 grid counts
	[InlineData(true, true, 3, 3, true, 30)]
	[InlineData(true, true, 3, 3, false, 35)]
	// For either 3x4 or 4x3 grid counts
	[InlineData(true, false, 3, 3, true, 41)]
	[InlineData(true, false, 3, 3, false, 46)]
	[InlineData(false, true, 3, 3, true, 42)]
	[InlineData(false, true, 3, 3, false, 42)]
	// For 4x4 grid counts
	[InlineData(false, false, 3, 3, true, 56)]
	[InlineData(false, false, 3, 3, false, 56)]
	public void CalculateLinkCount_Succeeds(
		bool verticalCentering,
		bool horizontalCentering,
		int horizontalCellCount,
		int verticalCellCount,
		bool startOffset,
		int expectedCount
	)
	{
		var networkData = new HexagonalNetworkData
		{
			VerticalCentering = verticalCentering,
			HorizontalCentering = horizontalCentering,
			HorizontalCellCount = horizontalCellCount,
			VerticalCellCount = verticalCellCount,
			StartOffset = startOffset
		};
		
		var calculator = new HexagonalNetworkCalculator();

		int actualCount = calculator.CalculateLinkCount(networkData);
		
		Assert.Equal(expectedCount, actualCount);
	}

	[Fact]
	public void CalculateLinkCount_NullData_Fails()
	{
		var calculator = new HexagonalNetworkCalculator();

		Assert.Throws<ArgumentNullException>(() => calculator.CalculateLinkCount(null));
	}

	[Fact]
	public void CalculateLinkCount_InvalidHorizontalCellCount_Fails()
	{
		var networkData = new HexagonalNetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 0,
			VerticalCellCount = 1
		};
		
		var calculator = new HexagonalNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateLinkCount(networkData));
	}

	[Fact]
	public void CalculateLinkCount_InvalidVerticalCellCount_Fails()
	{
		var networkData = new HexagonalNetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 1,
			VerticalCellCount = 0
		};
		
		var calculator = new HexagonalNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateLinkCount(networkData));
	}

	#endregion CalculateLinkCount
	
	#region CalculateCellCount
	
	[Theory]
	[InlineData(true, true, 1, 1, true, 1)]
	[InlineData(false, true, 1, 1, true, 2)]
	[InlineData(true, false, 1, 1, true, 1)]
	[InlineData(false, false, 1, 1, true, 3)]
	[InlineData(true, true, 4, 4, true, 7)]
	[InlineData(true, true, 4, 4, false, 8)]
	public void CalculateCellCount_Succeeds(
		bool verticalCentering,
		bool horizontalCentering,
		int horizontalCellCount,
		int verticalCellCount,
		bool startOffset,
		int expectedCount
	)
	{
		var networkData = new HexagonalNetworkData
		{
			VerticalCentering = verticalCentering,
			HorizontalCentering = horizontalCentering,
			HorizontalCellCount = horizontalCellCount,
			VerticalCellCount = verticalCellCount,
			StartOffset = startOffset
		};
		
		var calculator = new HexagonalNetworkCalculator();

		int actualCount = calculator.CalculateCellCount(networkData);
		
		Assert.Equal(expectedCount, actualCount);
	}

	[Fact]
	public void CalculateCellCount_NullData_Fails()
	{
		var calculator = new HexagonalNetworkCalculator();

		Assert.Throws<ArgumentNullException>(() => calculator.CalculateCellCount(null));
	}

	[Fact]
	public void CalculateCellCount_InvalidHorizontalCellCount_Fails()
	{
		var networkData = new HexagonalNetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 1,
			VerticalCellCount = 0
		};
		
		var calculator = new HexagonalNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateCellCount(networkData));
	}

	[Fact]
	public void CalculateCellCount_InvalidVerticalCellCount_Fails()
	{
		var networkData = new HexagonalNetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 1,
			VerticalCellCount = 0
		};
		
		var calculator = new HexagonalNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateCellCount(networkData));
	}

	#endregion CalculateCellCount
}

/** @} */