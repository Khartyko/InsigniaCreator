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
	 * - CellTransform::Scale
	 */

	public static IEnumerable<object[]> NodeCountData =>
		new List<object[]>
		{
			new object[]
			{
				new TriangularNetworkData
				{
					CenterAlongXAxis = true,
					CenterAlongYAxis = true,

				},
				1
			}
		};

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

	[Theory]
    [MemberData(nameof(NodeCountData))]
	public void CalculateNodeCount_Succeeds()
	{
		var networkData = CreateNetworkData();
		const int expectedCount = 3;
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