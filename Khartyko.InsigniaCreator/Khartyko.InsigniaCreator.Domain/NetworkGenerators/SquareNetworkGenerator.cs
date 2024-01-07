using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Domain.Utility;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Domain.NetworkGenerators;

/// <summary>
/// Represents an instance of a NetworkGenerator that generates TemplateNetworks based on a typical square grid.
/// </summary>
public class SquareNetworkGenerator : INetworkGenerator<NetworkData>
{
    private readonly INetworkCalculator<NetworkData> _calculator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="calculator"></param>
    public SquareNetworkGenerator(INetworkCalculator<NetworkData> calculator)
    {
        _calculator = calculator;
    }

    private static List<Node> GeneratePositions(int verticalNodeCount, int horizontalNodeCount, int nodeCount, Transform transform)
    {
        var nodes = new List<Node>(nodeCount);
        
        int halfHorizontalNodeCount = horizontalNodeCount / 2;
        int halfVerticalNodeCount = verticalNodeCount / 2;
        
        for (var y = 0; y < verticalNodeCount; y++)
        {
            for (var x = 0; x < horizontalNodeCount; x++)
            {
                var position = new Vector2(x - halfHorizontalNodeCount, y - halfVerticalNodeCount);
                Node node = NodeHelper.Create(transform, position);

                nodes.Add(node);
            }
        }

        return nodes;
    }
    
    /// <summary>
    /// Generates a square TemplateNetwork with the specified data.
    /// </summary>
    /// <param name="generationData">The square NetworkData to use for the TemplateNetwork.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'generationData' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'generationData' isn't the proper type.</exception>
    /// <returns>A TemplateNetwork generated with the specified data.</returns>
    public TemplateNetwork GenerateNetwork(NetworkData generationData)
    {
        DomainAssertionHelper.NetworkDataCheck(generationData);

        var horizontalCount = CellCounterHelper.ConstrainCountByCentering(generationData.HorizontalCentering, generationData.HorizontalCellCount);
        var verticalCount = CellCounterHelper.ConstrainCountByCentering(generationData.VerticalCentering, generationData.VerticalCellCount);
        var transform = new Transform(generationData.CellTransform!);

        int horizontalNodeCount = horizontalCount + 1;
        int verticalNodeCount = verticalCount + 1;

        int nodeCount = _calculator.CalculateNodeCount(generationData);
        int linkCount = _calculator.CalculateLinkCount(generationData);
        int cellCount = _calculator.CalculateCellCount(generationData);

        var links = new List<Link>(linkCount);
        var cells = new List<Cell>(cellCount);

        List<Node> nodes = GeneratePositions(horizontalNodeCount, verticalNodeCount, nodeCount, transform);

        // Keep track of the previous top link so it doesn't have to be searched for or duplicated
        var previousBottomLinks = new Dictionary<int, Link>(horizontalCount);

        // Generate Links and Cells
        for (var y = 0; y < verticalCount; y++)
        {
            Link? previousRight = null;

            for (var x = 0; x < horizontalCount; x++)
            {
                bool onTopmostRow = y == 0;
                bool onLeftmostColumn = x == 0;

                Node topLeft;
                Node topRight;
                Node bottomLeft;

                Link top;
                Link left;

                if (onTopmostRow || onLeftmostColumn)
                {
                    topLeft = nodes[y * horizontalNodeCount + x];

                    if (onTopmostRow)
                    {
                        topRight = nodes[y * horizontalNodeCount + x + 1];
                        top = new Link(topLeft, topRight);
                        links.Add(top);
                    }
                    else
                    {
                        top = previousBottomLinks[x];
                        topRight = top.Head;
                    }

                    if (onLeftmostColumn)
                    {
                        bottomLeft = nodes[(y + 1) * horizontalNodeCount + x];
                        left = new Link(bottomLeft, topLeft);
                        links.Add(left);
                    }
                    else
                    {
                        left = previousRight!;
                        bottomLeft = left.Tail;
                    }
                }
                else
                {
                    top = previousBottomLinks[x];
                    left = previousRight!;
                    topLeft = top.Tail;
                    topRight = top.Head;
                    bottomLeft = left.Tail;
                }

                Node bottomRight = nodes[(y + 1) * horizontalNodeCount + x + 1];

                var right = new Link(topRight, bottomRight);
                var bottom = new Link(bottomRight, bottomLeft);
                links.Add(right);
                links.Add(bottom);

                var cell = new Cell(
                    new List<Node>
                    {
                        topLeft,
                        topRight,
                        bottomRight,
                        bottomLeft
                    },
                    new List<Link>
                    {
                        top,
                        right,
                        bottom,
                        left
                    }
                );

                cells.Add(cell);

                previousRight = right;
                previousBottomLinks[x] = bottom;
            }
        }

        return new TemplateNetwork(nodes, links, cells);
    }
}