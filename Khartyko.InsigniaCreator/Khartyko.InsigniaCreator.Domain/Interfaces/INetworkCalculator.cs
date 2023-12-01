using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Domain.Interface;

/// <summary>
/// 
/// </summary>
public interface INetworkCalculator
{
	protected static int ConstrainCountByCentering(int count, bool centerAlongAxis)
	{
		bool isEven = MathHelper.IsEven(count);

		int result = count;

		switch (centerAlongAxis)
		{
			case true when isEven:
				result--;
				break;

			case false when !isEven:
				result++;
				break;
		}
		
		return result;
	}
}

/// <summary>
/// Exposes methods that count the various components of a TemplateNetwork.
/// </summary>
/// <typeparam name="TData">The type of NetworkData that is related to the counts being made</typeparam>
public interface INetworkCalculator<in TData> : INetworkCalculator
	where TData : NetworkData
{
	/// <summary>
	/// Calculates the number of Nodes with the given NetworkData instance.
	/// </summary>
	/// <param name="networkData">The data in which to base the calculation on.</param>
	/// <returns>The count of Nodes from the given data.</returns>
	int CalculateNodeCount(TData networkData);
	
	/// <summary>
	/// Calculates the number of Links with the given NetworkData instance.
	/// </summary>
	/// <param name="networkData">The data in which to base the calculation on.</param>
	/// <returns>The count of Links from the given data.</returns>
	int CalculateLinkCount(TData networkData);
	
	/// <summary>
	/// Calculates the number of Cells with the given NetworkData instance.
	/// </summary>
	/// <param name="networkData">The data in which to base the calculation on.</param>
	/// <returns>The count of Cells from the given data.</returns>
	int CalculateCellCount(TData networkData);
}