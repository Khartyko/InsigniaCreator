using Khartyko.InsigniaCreator.Domain.Data;

namespace Khartyko.InsigniaCreator.Domain.Interface;

/// <summary>
/// Exposes methods that count the various components of a TemplateNetwork.
/// </summary>
/// <typeparam name="TData">The type of NetworkData that is related to the counts being made</typeparam>
public interface INetworkCalculator<in TData>
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