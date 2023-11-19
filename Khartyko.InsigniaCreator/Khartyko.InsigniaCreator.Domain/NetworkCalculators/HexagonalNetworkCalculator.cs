using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;

namespace Khartyko.InsigniaCreator.Domain.NetworkCalculators;

/// <summary>
/// 
/// </summary>
public class HexagonalNetworkCalculator : INetworkCalculator<HexagonalNetworkData>
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="networkData"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public int CalculateNodeCount(HexagonalNetworkData networkData)
	{
		int horizontalCount = networkData!.HorizontalCellCount;
		int verticalCount = networkData.VerticalCellCount;
		bool startOffset = networkData.StartOffset;

		int normalColumnCount = (horizontalCount + Convert.ToInt32(!startOffset)) / 2;
		int normalNodeCount = 4 * (verticalCount - 1) + 6;

		int remainingOffsetNodeCount = CalculateHalfOffsetColumnNodeCount(horizontalCount, verticalCount, startOffset);

		return normalNodeCount * normalColumnCount + remainingOffsetNodeCount;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="networkData"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public int CalculateLinkCount(HexagonalNetworkData networkData)
	{
		int horizontalCount = networkData!.HorizontalCellCount;
		int verticalCount = networkData.VerticalCellCount;
		bool startOffset = networkData.StartOffset;
		
		int normalColumnCount = (horizontalCount + Convert.ToInt32(!startOffset)) / 2;
		int normalLinkCount = 5 * (verticalCount - 1) + 6;
		int bridgingLinkCount = verticalCount * (horizontalCount - 1) / 2;
		int remainingOffsetLinkCount = CalculateHalfOffsetColumnLinkCount(horizontalCount, verticalCount, startOffset);
		
		return normalColumnCount * normalLinkCount + bridgingLinkCount + remainingOffsetLinkCount;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="networkData"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public int CalculateCellCount(HexagonalNetworkData networkData)
	{
		int horizontalCount = networkData!.HorizontalCellCount;
		int verticalCount = networkData.VerticalCellCount;
		bool startOffset = networkData.StartOffset;
		
		if (horizontalCount == 1 || verticalCount == 1)
		{
			return horizontalCount * verticalCount;
		}

		int normalColumnCount = (horizontalCount + Convert.ToInt32(!startOffset)) / 2;
		int offsetColumnCount = (horizontalCount + Convert.ToInt32(startOffset)) / 2;
		
		return normalColumnCount * offsetColumnCount * verticalCount;
	}
	
	private static int CalculateHalfOffsetColumnNodeCount(int horizontalCount, int verticalCount, bool startOffset)
	{
		bool isEven = horizontalCount % 2 == 0;

		int halfColumnCount = Math.Abs(2 * Convert.ToInt32(startOffset) - Convert.ToInt32(isEven));
		int halfColumnNodeCount = 2 * (verticalCount - 2) + 3;

		return halfColumnCount * halfColumnNodeCount;
	}

	private static int CalculateHalfOffsetColumnLinkCount(int horizontalCount, int verticalCount, bool startOffset)
	{
		bool isEven = horizontalCount % 2 == 0;
		
		int halfColumnCount = Math.Abs(2 * Convert.ToInt32(startOffset) - Convert.ToInt32(isEven));
		int halfColumnLinkCount = 3 * (verticalCount - 2) + 4;

		return halfColumnCount * halfColumnLinkCount;
	}
}