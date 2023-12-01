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

		int horizontalCount = INetworkCalculator.ConstrainCountByCentering(networkData.HorizontalCellCount, networkData.CenterAlongYAxis);
		int verticalCount = INetworkCalculator.ConstrainCountByCentering(networkData.VerticalCellCount, networkData.CenterAlongXAxis);

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

		int horizontalCount = INetworkCalculator.ConstrainCountByCentering(networkData.HorizontalCellCount, networkData.CenterAlongYAxis);
		int verticalCount = INetworkCalculator.ConstrainCountByCentering(networkData.VerticalCellCount, networkData.CenterAlongXAxis);

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

		int horizontalCount = INetworkCalculator.ConstrainCountByCentering(networkData.HorizontalCellCount, networkData.CenterAlongYAxis);
		int verticalCount = INetworkCalculator.ConstrainCountByCentering(networkData.VerticalCellCount, networkData.CenterAlongXAxis);

		return horizontalCount * verticalCount;
	}
}