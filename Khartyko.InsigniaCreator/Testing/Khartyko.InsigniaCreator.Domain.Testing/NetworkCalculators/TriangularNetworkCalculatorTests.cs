/** \addtogroup Domain.Testing
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.NetworkCalculators;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.TestingLibrary;

#pragma warning disable CS8625

namespace Khartyko.InsigniaCreator.Domain.Testing.NetworkCalculators;

public class TriangularNetworkCalculatorTests
{
	#region CalculateNodeCount

	/*
	 * Things that can affect the count:
	 * - StartFlipped
	 * - HorizontalCellCount
	 * - VerticalCellCount
	 * - CenterAlongXAxis
	 * - CenterAlongYAXis
	 */

	[Theory]
	[InlineData(true, true, 1, 1, true, 3)]
	[InlineData(false, true, 1, 1, true, 5)]
	[InlineData(true, false, 1, 1, true, 5)]
	[InlineData(false, false, 1, 1, true, 8)]
	[InlineData(true, true, 4, 4, true, 14)]
	[InlineData(true, true, 4, 4, false, 14)]
	public void CalculateNodeCount_Succeeds(
		bool centerAlongXAxis,
		bool centerAlongYAxis,
		int horizontalCellCount,
		int verticalCellCount,
		bool startFlipped,
		int expectedCount
	)
	{
		var networkData = new TriangularNetworkData
		{
			CenterAlongXAxis = centerAlongXAxis,
			CenterAlongYAxis = centerAlongYAxis,
			HorizontalCellCount = horizontalCellCount,
			VerticalCellCount = verticalCellCount,
			StartFlipped = startFlipped
		};
		
		var calculator = new TriangularNetworkCalculator();

		int actualCount = calculator.CalculateNodeCount(networkData);
		
		Assert.Equal(expectedCount, actualCount);
	}

	[Fact]
	public void CalculateNodeCount_NullData_Fails()
	{
		var calculator = new TriangularNetworkCalculator();

		Assert.Throws<ArgumentNullException>(() => calculator.CalculateNodeCount(null));
	}

	[Fact]
	public void CalculateNodeCount_InvalidHorizontalCellCount_Fails()
	{
		var networkData = new TriangularNetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 0,
			VerticalCellCount = 1
		};
		
		var calculator = new TriangularNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateNodeCount(networkData));
	}

	[Fact]
	public void CalculateNodeCount_InvalidVerticalCellCount_Fails()
	{
		var networkData = new TriangularNetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 1,
			VerticalCellCount = 0
		};
		
		var calculator = new TriangularNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateNodeCount(networkData));
	}

	#endregion CalculateNodeCount
	
	#region CalculateLinkCount

	[Theory]
	[InlineData(true, true, 1, 1, true, 3)]
	[InlineData(false, true, 1, 1, true, 6)]
	[InlineData(true, false, 1, 1, true, 7)]
	[InlineData(false, false, 1, 1, true, 13)]
	[InlineData(true, true, 4, 4, true, 28)]
	[InlineData(true, true, 4, 4, false, 28)]
	public void CalculateLinkCount_Succeeds(
		bool centerAlongXAxis,
		bool centerAlongYAxis,
		int horizontalCellCount,
		int verticalCellCount,
		bool startFlipped,
		int expectedCount
	)
	{
		var networkData = new TriangularNetworkData
		{
			CenterAlongXAxis = centerAlongXAxis,
			CenterAlongYAxis = centerAlongYAxis,
			HorizontalCellCount = horizontalCellCount,
			VerticalCellCount = verticalCellCount,
			StartFlipped = startFlipped
		};
		
		var calculator = new TriangularNetworkCalculator();

		int actualCount = calculator.CalculateLinkCount(networkData);
		
		Assert.Equal(expectedCount, actualCount);
	}

	[Fact]
	public void CalculateLinkCount_NullData_Fails()
	{
		var calculator = new TriangularNetworkCalculator();

		Assert.Throws<ArgumentNullException>(() => calculator.CalculateLinkCount(null));
	}

	[Fact]
	public void CalculateLinkCount_InvalidHorizontalCellCount_Fails()
	{
		var networkData = new TriangularNetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 0,
			VerticalCellCount = 1
		};
		
		var calculator = new TriangularNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateLinkCount(networkData));
	}

	[Fact]
	public void CalculateLinkCount_InvalidVerticalCellCount_Fails()
	{
		var networkData = new TriangularNetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 1,
			VerticalCellCount = 0
		};
		
		var calculator = new TriangularNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateLinkCount(networkData));
	}

	#endregion CalculateLinkCount
	
	#region CalculateCellCount
	
	[Theory]
	[InlineData(true, true, 1, 1, true, 1)]
	[InlineData(false, true, 1, 1, true, 2)]
	[InlineData(true, false, 1, 1, true, 3)]
	[InlineData(false, false, 1, 1, true, 6)]
	[InlineData(true, true, 4, 4, true, 15)]
	[InlineData(true, true, 4, 4, false, 15)]
	public void CalculateCellCount_Succeeds(
		bool centerAlongXAxis,
		bool centerAlongYAxis,
		int horizontalCellCount,
		int verticalCellCount,
		bool startFlipped,
		int expectedCount
	)
	{
		var networkData = new TriangularNetworkData
		{
			CenterAlongXAxis = centerAlongXAxis,
			CenterAlongYAxis = centerAlongYAxis,
			HorizontalCellCount = horizontalCellCount,
			VerticalCellCount = verticalCellCount,
			StartFlipped = startFlipped
		};
		
		var calculator = new TriangularNetworkCalculator();

		int actualCount = calculator.CalculateCellCount(networkData);
		
		Assert.Equal(expectedCount, actualCount);
	}

	[Fact]
	public void CalculateCellCount_NullData_Fails()
	{
		var calculator = new TriangularNetworkCalculator();

		Assert.Throws<ArgumentNullException>(() => calculator.CalculateCellCount(null));
	}

	[Fact]
	public void CalculateCellCount_InvalidHorizontalCellCount_Fails()
	{
		var networkData = new TriangularNetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 1,
			VerticalCellCount = 0
		};
		
		var calculator = new TriangularNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateCellCount(networkData));
	}

	[Fact]
	public void CalculateCellCount_InvalidVerticalCellCount_Fails()
	{
		var networkData = new TriangularNetworkData
		{
			Width = 1280,
			Height = 800,
			HorizontalCellCount = 1,
			VerticalCellCount = 0
		};
		
		var calculator = new TriangularNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateCellCount(networkData));
	}

	#endregion CalculateCellCount
}

/** @} */