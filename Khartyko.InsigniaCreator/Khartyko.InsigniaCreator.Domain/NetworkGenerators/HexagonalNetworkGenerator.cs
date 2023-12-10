using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Domain.Utility;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Domain.NetworkGenerators;

/// <summary>
/// Represents an instance of a NetworkGenerator that generates TemplateNetworks based on a hexagonal grid.
/// </summary>
public class HexagonalNetworkGenerator : INetworkGenerator<HexagonalNetworkData>
{
    private static readonly double s_CellWidth = Math.Sqrt(0.1875);
    private static readonly double s_CellHeight = 0.5;

    private readonly INetworkCalculator<HexagonalNetworkData> _calculator;

    private static double UpdateOscillationModifier(int flippedBit)
        => 0.125 * (1 - flippedBit) - 0.125 * flippedBit;

    private static (double, double) CalculateTranslation(Transform transform, double horizontalOffset, double verticalOffset, int xIndex, int yIndex)
    {
        var scale = transform.Scale;
        var translation = transform.Translation;

        double x = translation.X + horizontalOffset / -2 + (xIndex * scale.X * s_CellWidth) - (scale.X * s_CellWidth / 2);
        double y = translation.Y + verticalOffset / -2 + ((yIndex - 1) * scale.Y * 0.75);

        return (x, y);
    }

    /// <summary>
    /// Constructs a Hexagonal NetworkGenerator with an INetworkCalculator.
    /// </summary>
    /// <param name="calculator">The NetworkCalculator uses to calculate Node, Link, and Cell counts.</param>
    public HexagonalNetworkGenerator(INetworkCalculator<HexagonalNetworkData> calculator)
    {
        _calculator = calculator;
    }

    /// <summary>
    /// Generates a hexagonal TemplateNetwork with the specified data.
    /// </summary>
    /// <param name="generationData">The hexagonal NetworkData to use for the TemplateNetwork.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'generationData' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'generationData' isn't the proper type.</exception>
    /// <returns>A TemplateNetwork generated with the specified data.</returns>
    public TemplateNetwork GenerateNetwork(HexagonalNetworkData generationData)
    {
        DomainAssertionHelper.NetworkDataCheck(generationData);

        Node[] nodes = SetupNodes(generationData);
        Link[] links = SetupLinks(generationData, nodes);
        Cell[] cells = SetupCells(generationData, nodes, links);

        return new TemplateNetwork(nodes, links, cells);
    }

    private Node[] SetupNodes(HexagonalNetworkData networkData)
    {
        int flippedBit = 0;
        var horizontalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongYAxis, networkData.HorizontalCellCount);
        var verticalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongXAxis, networkData.VerticalCellCount);
        int nodeCount = _calculator.CalculateNodeCount(networkData);
        var originalTransform = networkData.CellTransform;
        var transform = new Transform(networkData.CellTransform);
        var scale = transform.Scale;
        int currentNodeIndex = 0;

        int offsetBit = Convert.ToInt32(networkData.StartOffset);

        int horizontalNodeCount = 2 * horizontalCount + 1;
        int verticalNodeCount = verticalCount + 1;

        double scaledX = s_CellWidth * scale.X;
        double scaledY = s_CellHeight * scale.Y;
        // This is the delta between higher and lower Nodes
        double oscillationModifier;

        Node[] nodes = new Node[nodeCount];

        var globalNodeOffset = new Vector2(
            (networkData.Width - (horizontalNodeCount * scale.X)) / 2,
            (networkData.Height - (verticalNodeCount * scale.Y)) / 2
        );

        var verticalOffset = (verticalCount - 1) * (scale.Y * s_CellHeight);
        var horizontalOffset = (horizontalCount - offsetBit) * (scale.X * s_CellWidth);

        int offsetRowCount = horizontalNodeCount - offsetBit * 2 * Convert.ToInt32(nodeCount > 6);

        // Generate the first row of Nodes
        for (int x = 0; x < offsetRowCount; x++)
        {
            //oscillationModifier = UpdateOscillationModifier(flippedBit);

            // TODO: Figure out how the local position works for the Nodes

            var localPosition = new Vector2(scaledX * x, 0);
            //var startOffsetModifier = new Vector2(scale.X / 2 * offsetBit, 0);
            //var translation = globalNodeOffset + localPosition;
            
            // Calculate the translation and update it as needed
            //var (translationX, translationY) = CalculateTranslation(originalTransform, horizontalOffset, verticalOffset, x, 0);
            //transform.Translation.X = translationX;
            //transform.Translation.Y = translationY;

            var node = NodeHelper.Create(transform, localPosition);
            nodes[currentNodeIndex] = node;

            currentNodeIndex++;
            flippedBit = 1 - flippedBit;
        }

        // Toggle this bit so it doesn't repeat the row direction
        flippedBit = 1;

        // Generate the middle row of Nodes
        for (var y = 1; y < verticalCount; y++)
        {
            // NOTE: Be careful about using 'x' here -- it could go out of bounds
            for (int x = 2; x < horizontalNodeCount + 2; x++)
            {
                oscillationModifier = UpdateOscillationModifier(flippedBit);

                // TODO: Figure out how the local position works for the Nodes

                var localPosition = new Vector2(scaledX * x, scaledY * (y + oscillationModifier));
                //var position = globalNodeOffset + localPosition;

                // Calculate the translation and update it as needed
                //var (translationX, translationY) = CalculateTranslation(originalTransform, horizontalOffset, verticalOffset, x, y);
                //transform.Translation.X = translationX;
                //transform.Translation.Y = translationY;

                var node = NodeHelper.Create(transform, localPosition);
                nodes[currentNodeIndex] = node;

                currentNodeIndex++;
                flippedBit = 1 - flippedBit;
            }
        }

        // Toggle this bit so it doesn't repeat the row direction
        flippedBit = 1;

        // Generate the last row of Nodes
        for (int x = 0; x < offsetRowCount; x++)
        {
            oscillationModifier = UpdateOscillationModifier(flippedBit);

            int yIndex = verticalCount - 1;
            double yValue = scaledY * (yIndex + oscillationModifier);

            // TODO: Figure out how the local position works for the Nodes

            var localPosition = new Vector2(scaledX * x, yValue);
            //var position = globalNodeOffset + localPosition;

            // Calculate the translation and update it as needed
            //var (translationX, translationY) = CalculateTranslation(originalTransform, horizontalOffset, verticalOffset, x, yIndex);
            //transform.Translation.X = translationX;
            //transform.Translation.Y = translationY;

            var node = NodeHelper.Create(transform, localPosition);
            nodes[currentNodeIndex] = node;

            currentNodeIndex++;
            flippedBit = 1 - flippedBit;
        }

        return nodes;
    }

    private Link[] SetupLinks(HexagonalNetworkData networkData, Node[] nodes)
    {
        var horizontalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongYAxis, networkData.HorizontalCellCount);
        var verticalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongXAxis, networkData.VerticalCellCount);
        int linkCount = _calculator.CalculateLinkCount(networkData);

        int offsetBit = Convert.ToInt32(networkData.StartOffset);
        int normalBit = Convert.ToInt32(!networkData.StartOffset);

        int currentNodeIndex = 0;
        int currentLinkIndex = 0;

        Link[] links = new Link[linkCount];

        return links;
    }

    private Cell[] SetupCells(HexagonalNetworkData networkData, Node[] nodes, Link[] links)
    {
        var horizontalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongYAxis, networkData.HorizontalCellCount);
        var verticalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongXAxis, networkData.VerticalCellCount);
        int cellCount = _calculator.CalculateCellCount(networkData);

        int offsetBit = Convert.ToInt32(networkData.StartOffset);
        int normalBit = Convert.ToInt32(!networkData.StartOffset);

        int currentNodeIndex = 0;
        int currentLinkIndex = 0;
        int currentCellIndex = 0;

        Cell[] cells = new Cell[cellCount];

        return cells;
    }
}