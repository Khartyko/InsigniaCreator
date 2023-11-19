using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;

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
		int horizontalCount = networkData!.HorizontalCellCount;
		int verticalCount = networkData.VerticalCellCount;

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
		int horizontalCount = networkData!.HorizontalCellCount;
		int verticalCount = networkData.VerticalCellCount;

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
		int horizontalCount = networkData!.HorizontalCellCount;
		int verticalCount = networkData.VerticalCellCount;

		return horizontalCount * verticalCount;
	}
}