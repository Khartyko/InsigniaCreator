/** \addtogroup Domain.Testing
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.NetworkCalculators;

#pragma warning disable CS8625

namespace Khartyko.InsigniaCreator.Domain.Testing.NetworkCalculators;

public class SquareNetworkCalculatorTests
{
	#region CalculateNodeCount

	[Theory]
	[InlineData(true, true, 1, 1, 4)]
	[InlineData(false, true, 1, 1, 6)]
	[InlineData(true, false, 1, 1, 6)]
	[InlineData(false, false, 1, 1, 9)]
	[InlineData(true, true, 4, 4, 16)]
	public void CalculateNodeCount_Succeeds(
		bool centerAlongXAxis,
		bool centerAlongYAxis,
		int horizontalCellCount,
		int verticalCellCount,
		int expectedCount
	)
	{
		var networkData = new NetworkData
		{
			CenterAlongXAxis = centerAlongXAxis,
			CenterAlongYAxis = centerAlongYAxis,
			HorizontalCellCount = horizontalCellCount,
			VerticalCellCount = verticalCellCount
		};
		
		var calculator = new SquareNetworkCalculator();

		int actualCount = calculator.CalculateNodeCount(networkData);
		
		Assert.Equal(expectedCount, actualCount);
	}

	[Fact]
	public void CalculateNodeCount_NullData_Fails()
	{
		var calculator = new SquareNetworkCalculator();

		Assert.Throws<ArgumentNullException>(() => calculator.CalculateNodeCount(null));
	}

	[Fact]
	public void CalculateNodeCount_InvalidHorizontalCellCount_Fails()
	{
		var networkData = new NetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 0,
			VerticalCellCount = 1
		};
		
		var calculator = new SquareNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateNodeCount(networkData));
	}

	[Fact]
	public void CalculateNodeCount_InvalidVerticalCellCount_Fails()
	{
		var networkData = new NetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 1,
			VerticalCellCount = 0
		};
		
		var calculator = new SquareNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateNodeCount(networkData));
	}

	#endregion CalculateNodeCount
	
	#region CalculateLinkCount

	[Theory]
	[InlineData(true, true, 1, 1, 4)]
	[InlineData(false, true, 1, 1, 7)]
	[InlineData(true, false, 1, 1, 7)]
	[InlineData(false, false, 1, 1, 12)]
	[InlineData(true, true, 4, 4, 24)]
	public void CalculateLinkCount_Succeeds(
		bool centerAlongXAxis,
		bool centerAlongYAxis,
		int horizontalCellCount,
		int verticalCellCount,
		int expectedCount
	)
	{
		var networkData = new NetworkData
		{
			CenterAlongXAxis = centerAlongXAxis,
			CenterAlongYAxis = centerAlongYAxis,
			HorizontalCellCount = horizontalCellCount,
			VerticalCellCount = verticalCellCount
		};
		
		var calculator = new SquareNetworkCalculator();

		int actualCount = calculator.CalculateLinkCount(networkData);
		
		Assert.Equal(expectedCount, actualCount);
	}

	[Fact]
	public void CalculateLinkCount_NullData_Fails()
	{
		var calculator = new SquareNetworkCalculator();

		Assert.Throws<ArgumentNullException>(() => calculator.CalculateLinkCount(null));
	}

	[Fact]
	public void CalculateLinkCount_InvalidHorizontalCellCount_Fails()
	{
		var networkData = new NetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 0,
			VerticalCellCount = 1
		};
		
		var calculator = new SquareNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateLinkCount(networkData));
	}

	[Fact]
	public void CalculateLinkCount_InvalidVerticalCellCount_Fails()
	{
		var networkData = new NetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 1,
			VerticalCellCount = 0
		};
		
		var calculator = new SquareNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateLinkCount(networkData));
	}

	#endregion CalculateLinkCount
	
	#region CalculateCellCount
	
	[Theory]
	[InlineData(true, true, 1, 1, 1)]
	[InlineData(false, true, 1, 1, 2)]
	[InlineData(true, false, 1, 1, 2)]
	[InlineData(false, false, 1, 1, 4)]
	[InlineData(true, true, 4, 4, 9)]
	public void CalculateCellCount_Succeeds(
		bool centerAlongXAxis,
		bool centerAlongYAxis,
		int horizontalCellCount,
		int verticalCellCount,
		int expectedCount
	)
	{
		var networkData = new NetworkData
		{
			CenterAlongXAxis = centerAlongXAxis,
			CenterAlongYAxis = centerAlongYAxis,
			HorizontalCellCount = horizontalCellCount,
			VerticalCellCount = verticalCellCount
		};
		
		var calculator = new SquareNetworkCalculator();

		int actualCount = calculator.CalculateCellCount(networkData);
		
		Assert.Equal(expectedCount, actualCount);
	}

	[Fact]
	public void CalculateCellCount_NullData_Fails()
	{
		var calculator = new SquareNetworkCalculator();

		Assert.Throws<ArgumentNullException>(() => calculator.CalculateCellCount(null));
	}

	[Fact]
	public void CalculateCellCount_InvalidHorizontalCellCount_Fails()
	{
		var networkData = new NetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 1,
			VerticalCellCount = 0
		};
		
		var calculator = new SquareNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateCellCount(networkData));
	}

	[Fact]
	public void CalculateCellCount_InvalidVerticalCellCount_Fails()
	{
		var networkData = new NetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 1,
			VerticalCellCount = 0
		};
		
		var calculator = new SquareNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateCellCount(networkData));
	}

	#endregion CalculateCellCount
}

/** @} */