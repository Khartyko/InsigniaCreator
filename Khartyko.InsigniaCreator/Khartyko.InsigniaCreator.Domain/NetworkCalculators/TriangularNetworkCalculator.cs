using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;
using Khartyko.InsigniaCreator.Domain.Utility;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

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
        DomainAssertionHelper.CalculatorDataCheck(networkData);

        int horizontalCount = INetworkCalculator.ConstrainCountByCentering(networkData.HorizontalCellCount, networkData.CenterAlongYAxis);
        int verticalCount = INetworkCalculator.ConstrainCountByCentering(networkData.VerticalCellCount, networkData.CenterAlongXAxis);
		bool startFlipped = networkData.StartFlipped;
		var flippedBit = Convert.ToInt32(startFlipped);
		int rowCount = (verticalCount + 1) / 2;
		int nodesPerRow = 3 + 2 * (horizontalCount - 1);
		int remainingNodes = MathHelper.IsEven(verticalCount)
			? horizontalCount + flippedBit
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
        DomainAssertionHelper.CalculatorDataCheck(networkData);

        int horizontalCount = INetworkCalculator.ConstrainCountByCentering(networkData.HorizontalCellCount, networkData.CenterAlongYAxis);
        int verticalCount = INetworkCalculator.ConstrainCountByCentering(networkData.VerticalCellCount, networkData.CenterAlongXAxis);
        
	    int halfVerticalCount = (verticalCount + 1) / 2;
	    var evenBit = Convert.ToInt32(MathHelper.IsEven(verticalCount));
	    var flippedRowsBit = Convert.ToInt32(networkData.StartFlipped);
	    var uprightRowsBit = Convert.ToInt32(!networkData.StartFlipped);
	    
	    // Calculate the number of flipped rows
	    int flippedRowsCount = halfVerticalCount + (evenBit & flippedRowsBit);
	    
	    // Calculate the number of upright rows
	    int uprightRowsCount = halfVerticalCount + (evenBit & uprightRowsBit);
        
	    // Get the Link counts from both of the flipped/upright rows:
	    int flippedLinksCount = flippedRowsCount * horizontalCount;
	    int uprightLinksCount = uprightRowsCount * (horizontalCount - 1);
	    
	    // Count the angled Links
	    int angledLinksCount = horizontalCount * verticalCount * 2;

		return angledLinksCount + flippedLinksCount + uprightLinksCount;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="networkData"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public int CalculateCellCount(TriangularNetworkData networkData)
    {
        DomainAssertionHelper.CalculatorDataCheck(networkData);

        int horizontalCount = INetworkCalculator.ConstrainCountByCentering(networkData.HorizontalCellCount, networkData.CenterAlongYAxis);
        int verticalCount = INetworkCalculator.ConstrainCountByCentering(networkData.VerticalCellCount, networkData.CenterAlongXAxis);
		
		return verticalCount * (horizontalCount + (horizontalCount - 1));
	}
}