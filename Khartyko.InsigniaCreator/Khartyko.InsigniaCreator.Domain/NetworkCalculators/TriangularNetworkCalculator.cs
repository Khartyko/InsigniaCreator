using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;

namespace Khartyko.InsigniaCreator.Domain.NetworkCalculators;

/// <summary>
/// Represents a container for methods to calculate the number of Entities to make up a grid of triangles.
/// </summary>
public class TriangularNetworkCalculator : INetworkCalculator<TriangularNetworkData>
{
	/// <summary>
	/// Calculates the number of Nodes with the given data.
	/// </summary>
	/// <param name="networkData"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public int CalculateNodeCount(TriangularNetworkData networkData)
	{
		int horizontalCount = networkData!.HorizontalCellCount;
		int verticalCount = networkData.VerticalCellCount;
		bool startFlipped = networkData.StartFlipped;
		var flippedBit = Convert.ToInt32(startFlipped);
		int oddBit = verticalCount % 2;
		int rowCount = (verticalCount + 1) / 2;
		int nodesPerRow = 3 + 2 * (horizontalCount - 1);
		int remainingNodes = verticalCount > 1
			? oddBit ^ flippedBit
			: 0;

		return rowCount * nodesPerRow + remainingNodes;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="networkData"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public int CalculateLinkCount(TriangularNetworkData networkData)
	{
		int horizontalCount = networkData!.HorizontalCellCount;
		int verticalCount = networkData.VerticalCellCount;
		bool startFlipped = networkData.StartFlipped;
		var flippedBit = Convert.ToInt32(startFlipped);
		int angledLinksCount = horizontalCount * verticalCount * 2;
		int overestimatedBaseLinksCount = horizontalCount * (verticalCount + 1);
		int overestimateCorrection = (verticalCount + flippedBit) / 2 - Convert.ToInt32(!startFlipped);

		return angledLinksCount + overestimatedBaseLinksCount - overestimateCorrection;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="networkData"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public int CalculateCellCount(TriangularNetworkData networkData)
	{
		int horizontalCount = networkData!.HorizontalCellCount;
		int verticalCount = networkData.VerticalCellCount;
		
		return verticalCount * (horizontalCount + (horizontalCount - 1));
	}
}