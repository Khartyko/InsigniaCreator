using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;
using Khartyko.InsigniaCreator.Domain.Utility;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Domain.NetworkCalculators;

/// <summary>
/// 
/// </summary>
public class HexagonalNetworkCalculator : INetworkCalculator<HexagonalNetworkData>
{
	/// <summary>
	/// Calculates the number of Nodes with the given NetworkData.
	/// </summary>
	/// <param name="networkData">The given data to generate a TemplateNetwork used for the calculations.</param>
	/// <returns>The number of Nodes to be expected.</returns>
	/// <exception cref="ArgumentNullException">Can be thrown if 'networkData' is null.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Can be thrown if HorizontalCellCount or VerticalCellCount is less than 1.</exception>
	/// <exception cref="ArgumentException">Can be thrown if there are any other errors with the data.</exception>
	public int CalculateNodeCount(HexagonalNetworkData networkData)
	{
		DomainAssertionHelper.CalculatorDataCheck(networkData);

        // Initial variable setup
        var horizontalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongYAxis, networkData.VerticalCellCount);
        var verticalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongXAxis, networkData.HorizontalCellCount);
        bool startOffset = networkData.StartOffset;
		bool isEven = MathHelper.IsEven(verticalCount);
		
		var count = 0;
		
		// All the bits used for the calculations
		var horizontalCountIsNotOneBit = Convert.ToInt32(horizontalCount > 1);
		var horizontalCountIsOneBit = Convert.ToInt32(horizontalCount == 1);
		var verticalCountIsNotOneBit = Convert.ToInt32(verticalCount > 1);
		var evenBit = Convert.ToInt32(isEven);
		var oddBit = Convert.ToInt32(!isEven);
		var offsetBit = Convert.ToInt32(startOffset);
		var normalBit = Convert.ToInt32(!startOffset);
		int oddOffsetBit = oddBit & offsetBit;
		int evenNormalBit = evenBit & normalBit;
		int accountedRowsCount = (offsetBit * evenBit) + (offsetBit * oddBit * 2) + (normalBit * evenBit);

		// Add the top row, if it's offset
		// Note: It would be here where it would be decided to include the first row
		int firstOffsetRowCount = (offsetBit | horizontalCountIsOneBit) * (3 + 2 * Math.Max(0, horizontalCount - 2));
		
		// Calculate the number of normal rows
		// Note: This will be annulled if the horizontal count is 1 or the vertical count is 1
		int normalRowCount = (horizontalCountIsNotOneBit | horizontalCountIsNotOneBit & verticalCountIsNotOneBit)
			* Math.Max(0, verticalCount + normalBit - accountedRowsCount / 2) / 2;
		
		// Add their count
		int normalRowNodeCount = (6 + 4 * (horizontalCount - 1)) * normalRowCount;
		
		// Add the bottom row, if it's offset and the count is even or if just the count is even.
		int lastOffsetRowCount =  (oddOffsetBit | evenNormalBit | horizontalCountIsOneBit) * (3 + 2 * Math.Max(0, horizontalCount - 2));

		count += firstOffsetRowCount + normalRowNodeCount + lastOffsetRowCount;
		
		return count;
	}

	/// <summary>
	/// Calculates the number of Links with the given NetworkData.
	/// </summary>
	/// <param name="networkData">The given data to generate a TemplateNetwork used for the calculations.</param>
	/// <returns>The number of Links to be expected.</returns>
	/// <exception cref="ArgumentNullException">Can be thrown if 'networkData' is null.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Can be thrown if HorizontalCellCount or VerticalCellCount is less than 1.</exception>
	/// <exception cref="ArgumentException">Can be thrown if there are any other errors with the data.</exception>
	public int CalculateLinkCount(HexagonalNetworkData networkData)
	{
		DomainAssertionHelper.CalculatorDataCheck(networkData);

        // Initial variable setup
        var horizontalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongYAxis, networkData.VerticalCellCount);
        var verticalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongXAxis, networkData.HorizontalCellCount);
        bool startOffset = networkData.StartOffset;
		bool isEven = MathHelper.IsEven(verticalCount);
		
		var count = 0;
		
		// All the bits used for the calculations
		var horizontalCountIsNotOneBit = Convert.ToInt32(horizontalCount > 1);
		var verticalCountIsNotOneBit = Convert.ToInt32(verticalCount > 1);
		var evenBit = Convert.ToInt32(isEven);
		var oddBit = Convert.ToInt32(!isEven);
		var offsetBit = Convert.ToInt32(startOffset);
		var normalBit = Convert.ToInt32(!startOffset);
		int oddOffsetBit = oddBit & offsetBit;
		int evenNormalBit = evenBit & normalBit;
		int offsetRowBit = horizontalCountIsNotOneBit | horizontalCountIsNotOneBit & verticalCountIsNotOneBit;
		
		// This is literally just to handle the special case where there's 1 row, but it's offset
		// Since any offset rows are horizontalCount - 1, this would change how the normal row reacts
		int singleRowButOffsetBit = Convert.ToInt32(verticalCount == 1) & offsetBit;
		
		// Calculate the first and last rows if they're offset (and if there are more than 1 rows)
		int offsetFirstRowLinksCount = offsetRowBit * (offsetBit & verticalCountIsNotOneBit) * (4 + 3 * (horizontalCount - 2));
		int offsetLastRowLinksCount = offsetRowBit * ((oddOffsetBit | evenNormalBit) & verticalCountIsNotOneBit) * (4 + 3 * (horizontalCount - 2));

		// Calculate all normal rows
		int normalRowCount = (verticalCount + normalBit + singleRowButOffsetBit) / 2;
		int normalRowLinksCount = normalRowCount * (6 + 5 * Math.Max(0, horizontalCount - 1 - singleRowButOffsetBit));
		
		// Calculate all offset rows (which would just be connectors for normal rows)
		int offsetRowDeductions = ((offsetBit & oddBit) * 2 + (offsetBit & evenBit) + (normalBit & evenBit));
		int offsetRowCount = Math.Max(0, Math.Max(0, verticalCount + offsetBit) / 2 - offsetRowDeductions);
		int offsetRowLinksCount = offsetRowBit * offsetRowCount * horizontalCount;

		count += offsetFirstRowLinksCount + offsetLastRowLinksCount + normalRowLinksCount + offsetRowLinksCount;

		return count;
	}

	/// <summary>
	/// Calculates the number of Cells with the given NetworkData.
	/// </summary>
	/// <param name="networkData">The given data to generate a TemplateNetwork used for the calculations.</param>
	/// <returns>The number of Cells to be expected.</returns>
	/// <exception cref="ArgumentNullException">Can be thrown if 'networkData' is null.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Can be thrown if HorizontalCellCount or VerticalCellCount is less than 1.</exception>
	/// <exception cref="ArgumentException">Can be thrown if there are any other errors with the data.</exception>
	public int CalculateCellCount(HexagonalNetworkData networkData)
	{
		DomainAssertionHelper.CalculatorDataCheck(networkData);

        var horizontalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongYAxis, networkData.VerticalCellCount);
        var verticalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongXAxis, networkData.HorizontalCellCount);
        bool startOffset = networkData.StartOffset;
		
		int normalColumnCount = (verticalCount + Convert.ToInt32(!startOffset)) / 2;
		int offsetColumnCount = (verticalCount + Convert.ToInt32(startOffset)) / 2;
		
		return normalColumnCount * horizontalCount + offsetColumnCount * Math.Max(1, horizontalCount - 1);
	}
}