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
	 * Cases to test for:
	 * - Width restriction scale.x * HorizontalCellCount:
	 * - Height restricting scale.y * VerticalCellCount
	 *
	 * These two (should force it to be even/odd):
	 * - Center Along X-Axis
	 * - Center Along Y-Axis
	 *
	 * Check the edge cases here
	 * - StartFlipped
	 */

	[Fact]
	public void CalculateNodeCount_CountRestrictedByHorizontalScaleAndWidth_Succeeds()
	{
	}

	[Fact]
	public void CalculateNodeCount_CountRestrictedByVerticalScaleAndWidth_Succeeds()
	{
	}

	[Fact]
	public void CalculateNodeCount_CountRestrictionByCentering_Succeeds()
	{
	}

	[Fact]
	public void CalculateNodeCount_StartFlipped_Succeeds()
	{
	}

	private TriangularNetworkData CreateNetworkData(
		bool centerAlongXAxis = false,
		bool centerAlongYAxis = false,
		int horizontalCellCount = 1,
		int verticalCellCount = 1,
		Transform? cellTransform = null,
		bool startFlipped = false
	)
	{
		return new TriangularNetworkData
		{
			CenterAlongXAxis = centerAlongXAxis,
			CenterAlongYAxis = centerAlongYAxis,
			HorizontalCellCount = horizontalCellCount,
			VerticalCellCount = verticalCellCount,
			CellTransform = cellTransform ?? new Transform(),
			StartFlipped = startFlipped
		};
	}

	[Fact]
	public void CalculateNodeCount_Succeeds()
	{
		var networkData = CreateNetworkData();
		const int expectedCount = 1;
		var calculator = new TriangularNetworkCalculator();

		int actualCount = calculator.CalculateNodeCount(networkData);
		
		Assert.Equal(expectedCount, actualCount);
	}

	[Fact]
	public void CalculateNodeCount_VariableValues_CenterAlongXAxis_Succeeds()
	{
	}

	[Fact]
	public void CalculateNodeCount_VariableValues_CenterAlongYAxis_Succeeds()
	{
	}

	[Fact]
	public void CalculateNodeCount_VariableValues_HorizontalCellCount_Succeeds()
	{
	}

	[Fact]
	public void CalculateNodeCount_VariableValues_VerticalCellCount_Succeeds()
	{
	}

	[Fact] public void CalculateNodeCount_VariableValues_CellTransform_Succeeds()
	{
	}

	[Fact]
	public void CalculateNodeCount_VariableValues_StartFlipped_Succeeds()
	{
	}

	[Fact]
	public void CalculateNodeCount_NullData_Fails()
	{
		var calculator = new TriangularNetworkCalculator();

		Assert.Throws<ArgumentNullException>(() => calculator.CalculateNodeCount(null));
	}

	[Theory, InlineData(0), InlineData(-1)]
	public void CalculateNodeCount_WidthOutOfRange_Fails(double width)
	{
		var networkData = new TriangularNetworkData
		{
			Width = width,
			Height = 720,
			HorizontalCellCount = 1,
			VerticalCellCount = 1
		};
		
		var calculator = new TriangularNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateNodeCount(networkData));
	}

	[Theory, InlineData(0), InlineData(-1)]
	public void CalculateNodeCount_HeightOutOfRange_Fails(double height)
	{
		var networkData = new TriangularNetworkData
		{
			Width = 1280,
			Height = height,
			HorizontalCellCount = 1,
			VerticalCellCount = 1
		};
		
		var calculator = new TriangularNetworkCalculator();

		Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateNodeCount(networkData));
	}

	[Theory, ClassData(typeof(InvalidDoubleData))]
	public void CalculateNodeCount_InvalidWidth_Fails(double width)
	{
		var networkData = new TriangularNetworkData
		{
			Width = width,
			Height = 720,
			HorizontalCellCount = 1,
			VerticalCellCount = 1
		};
		
		var calculator = new TriangularNetworkCalculator();

		Assert.Throws<ArgumentException>(() => calculator.CalculateNodeCount(networkData));
	}

	[Theory, ClassData(typeof(InvalidDoubleData))]
	public void CalculateNodeCount_InvalidHeight_Fails(double height)
	{
		
		var networkData = new TriangularNetworkData
		{
			Width = 1280,
			Height = height,
			HorizontalCellCount = 1,
			VerticalCellCount = 1
		};
		
		var calculator = new TriangularNetworkCalculator();

		Assert.Throws<ArgumentException>(() => calculator.CalculateNodeCount(networkData));
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
	
	[Fact]
	public void CalculateLinkCount_Succeeds()
	{
	}

	[Fact]
	public void CalculateLinkCount_NullData_Fails()
	{
	}

	[Fact]
	public void CalculateLinkCount_InvalidWidth_Fails()
	{
	}

	[Fact]
	public void CalculateLinkCount_InvalidHeight_Fails()
	{
	}

	[Fact]
	public void CalculateLinkCount_InvalidHorizontalCellCount_Fails()
	{
	}

	[Fact]
	public void CalculateLinkCount_InvalidVerticalCellCount_Fails()
	{
	}

	#endregion CalculateLinkCount
	
	#region CalculateCellCount
	
	[Fact]
	public void CalculateCellCount_Succeeds()
	{
	}

	[Fact]
	public void CalculateCellCount_NullData_Fails()
	{
	}

	[Fact]
	public void CalculateCellCount_InvalidWidth_Fails()
	{
	}

	[Fact]
	public void CalculateCellCount_InvalidHeight_Fails()
	{
	}

	[Fact]
	public void CalculateCellCount_InvalidHorizontalCellCount_Fails()
	{
	}

	[Fact]
	public void CalculateCellCount_InvalidVerticalCellCount_Fails()
	{
	}

	#endregion CalculateCellCount
}

/** @} */