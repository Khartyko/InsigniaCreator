using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;
using Khartyko.InsigniaCreator.Domain.Utility;

namespace Khartyko.InsigniaCreator.Domain.NetworkCalculators;

/// <summary>
/// 
/// </summary>
public class SquareNetworkCalculator : INetworkCalculator<NetworkData>
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="networkData"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public int CalculateNodeCount(NetworkData networkData)
	{
		DomainAssertionHelper.CalculatorDataCheck(networkData);

        var horizontalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongYAxis, networkData.HorizontalCellCount);
        var verticalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongXAxis, networkData.VerticalCellCount);

        return (horizontalCount + 1) * (verticalCount + 1);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="networkData"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public int CalculateLinkCount(NetworkData networkData)
	{
		DomainAssertionHelper.CalculatorDataCheck(networkData);

        var horizontalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongYAxis, networkData.HorizontalCellCount);
        var verticalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongXAxis, networkData.VerticalCellCount);

        return (verticalCount + 1) * horizontalCount + (horizontalCount + 1) * verticalCount;
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="networkData"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public int CalculateCellCount(NetworkData networkData)
	{ 
		DomainAssertionHelper.CalculatorDataCheck(networkData);

        var horizontalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongYAxis, networkData.HorizontalCellCount);
        var verticalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongXAxis, networkData.VerticalCellCount);

        return horizontalCount * verticalCount;
	}
}