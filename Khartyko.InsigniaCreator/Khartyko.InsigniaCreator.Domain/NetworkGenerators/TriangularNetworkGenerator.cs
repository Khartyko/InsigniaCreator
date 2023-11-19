using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Domain.NetworkGenerators;

/// <summary>
/// Represents an instance of a NetworkGenerator that generates TemplateNetworks based on a triangular grid.
/// </summary>
public class TriangularNetworkGenerator : INetworkGenerator<TriangularNetworkData>
{
    private readonly INetworkCalculator<TriangularNetworkData> _calculator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="calculator"></param>
    public TriangularNetworkGenerator(INetworkCalculator<TriangularNetworkData> calculator)
    {
        _calculator = calculator;
    }

    /// <summary>
    /// Generates a triangular TemplateNetwork with the specified data.
    /// </summary>
    /// <param name="generationData">The triangular NetworkData to use for the TemplateNetwork.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'generationData' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'generationData' isn't the proper type.</exception>
    /// <returns>A TemplateNetwork generated with the specified data.</returns>
    public TemplateNetwork GenerateNetwork(TriangularNetworkData generationData)
    {
        AssertionHelper.NullCheck(generationData, nameof(generationData));
        AssertionHelper.NullCheck(generationData.Width, nameof(generationData.Width));
        AssertionHelper.NullCheck(generationData.Height, nameof(generationData.Height));
        AssertionHelper.NullCheck(generationData.CenterAlongXAxis, nameof(generationData.CenterAlongXAxis));
        AssertionHelper.NullCheck(generationData.CenterAlongYAxis, nameof(generationData.CenterAlongYAxis));
        AssertionHelper.NullCheck(generationData.HorizontalCellCount, nameof(generationData.HorizontalCellCount));
        AssertionHelper.NullCheck(generationData.VerticalCellCount, nameof(generationData.VerticalCellCount));
        AssertionHelper.NullCheck(generationData.CellTransform, nameof(generationData.CellTransform));
        AssertionHelper.NullCheck(generationData.StartFlipped, nameof(generationData.StartFlipped));
        
        // TODO: Fill this out.

        int nodeCount = _calculator.CalculateNodeCount(generationData);
        int linkCount = _calculator.CalculateLinkCount(generationData);
        int cellCount = _calculator.CalculateCellCount(generationData);

        var nodes = new List<Node>(nodeCount);
        var links = new List<Link>(linkCount);
        var cells = new List<Cell>(cellCount);

        return new TemplateNetwork(nodes, links, cells);
    }
}