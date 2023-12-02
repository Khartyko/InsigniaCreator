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
    /* The following number was calculated by:
    * a² + b² = c²
    * a² + 0.5² = 1²
    * a² + 0.25 = 1
    * a = √(1 - 0.25)
    * a = √(0.75)
    */
    private static double s_CellWidth = Math.Sqrt(0.75);

    private static Vector2 s_TopVector = new Vector2(0, 0.5);
    private static Vector2 s_TopRightVector = new Vector2(Math.Sqrt(3) / 4.0, 0.25);
    private static Vector2 s_BottomRightVector = new Vector2(Math.Sqrt(3) / 4.0, -0.25);
    private static Vector2 s_BottomVector = new Vector2(0, -0.5);
    private static Vector2 s_BottomLeftVector = new Vector2(-Math.Sqrt(3) / 4.0, -0.25);
    private static Vector2 s_TopLeftVector = new Vector2(-Math.Sqrt(3) / 4.0, 0.25);

    private readonly INetworkCalculator<HexagonalNetworkData> _calculator;

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
        var startOffset = generationData.StartOffset;
        var centerAlongXAxis = generationData.CenterAlongXAxis;
        var centerAlongYAxis = generationData.CenterAlongYAxis;
        var horizontalCount = CellCounterHelper.ConstrainCountByCentering(generationData.CenterAlongXAxis, generationData.VerticalCellCount);
        var verticalCount = CellCounterHelper.ConstrainCountByCentering(generationData.CenterAlongYAxis, generationData.HorizontalCellCount);
        var transform = new Transform(generationData.CellTransform);
        var scale = transform.Scale;
        var scaledCellWidth = scale.X * s_CellWidth;

        int nodeCount = _calculator.CalculateNodeCount(generationData);
        int linkCount = _calculator.CalculateLinkCount(generationData);
        int cellCount = _calculator.CalculateCellCount(generationData);

        var nodes = new List<Node>(nodeCount);
        var links = new List<Link>(linkCount);
        var cells = new List<Cell>(cellCount);

        var previousLinks = new LinkedList<Link>();

        for (int y = 1; y < verticalCount + 1; y++)
        {
            var offsetBit = Convert.ToInt32(startOffset);

            var verticalOffset = (verticalCount - 1) * (scale.Y * 0.75);
            Link? previousLink = null;

            var horizontalIterationsCount = horizontalCount - offsetBit + 1;

            for (int x = 1; x < horizontalIterationsCount; x++)
            {
                var horizontalOffset = (horizontalCount - offsetBit) * (scale.X * s_CellWidth);

                // Calculate the translation
                transform.Translation = new Vector2(
                    horizontalOffset / -2 + (x * scale.X * s_CellWidth) - (scale.X * s_CellWidth / 2),
                    verticalOffset / -2 + ((y - 1) * scale.Y * 0.75)
                );

                var cellNodes = new Node[6];
                var cellLinks = new Link[6];
                var nodeAdditions = new List<int>();
                var linkAdditions = new List<int>();

                /*
                * For the Node indices:
                * 0: Top
                * 1: Top-Right
                * 2: Bottom-Right
                * 3: Bottom
                * 4: Bottom-Left
                * 5: Top-Left
                */

                var useTopLeft = () =>
                {
                    var topLeft = previousLinks.First!.Value;
                    previousLinks.RemoveFirst();

                    cellNodes[5] = topLeft.Head;
                    cellLinks[5] = topLeft.Reversed();
                };

                var useTopRight = () =>
                {
                    var topRight = previousLinks.First!.Value;
                    previousLinks.RemoveFirst();

                    cellNodes[0] = topRight.Tail;
                    cellNodes[1] = topRight.Head;
                    cellLinks[0] = topRight.Reversed();
                };

                if (previousLink is null)
                {
                    // It's null, so that means generate the left-most Link
                    cellNodes[4] = NodeHelper.Create(transform, s_BottomLeftVector);
                    nodeAdditions.Add(4);
                }
                else
                {
                    // It's found, so that knocks off 2 Nodes and a Link that need to be created
                    cellNodes[4] = previousLink.Tail;
                }

                // Check if the top layer has any
                if (previousLinks.Any())
                {
                    // There are some, so they'll have to be retrieved

                    if (startOffset)
                    {
                        /*
                        * Nodes Handled: 0, 1, 5
                        * Links Handled: 0, 5
                        */
                        useTopLeft();
                        useTopRight();
                    }

                    if (x == 1)
                    {
                        /*
                        * Nodes Handled: 0, 1, 5
                        * Links Handled: 0, 5
                        */
                        useTopRight();

                        cellNodes[5] = NodeHelper.Create(transform, s_TopLeftVector);
                        cellLinks[5] = new Link(cellNodes[5], cellNodes[0]);

                        nodeAdditions.Add(5);
                        linkAdditions.Add(5);
                    }

                    if (x == horizontalIterationsCount - 1)
                    {
                        /*
                        * Nodes Handled: 0, 1, 5
                        * Links Handled: 0, 5
                        */
                        useTopLeft();

                        cellNodes[0] = NodeHelper.Create(transform, s_TopVector);
                        cellNodes[1] = NodeHelper.Create(transform, s_TopRightVector);
                        cellLinks[0] = new Link(cellNodes[0], cellNodes[1]);

                        nodeAdditions.Add(0);
                        nodeAdditions.Add(1);
                        linkAdditions.Add(0);
                    }
                }
                else
                {
                    // There isn't a top row, so the relevant Nodes and Links will have to be generated
                    cellNodes[0] = NodeHelper.Create(transform, s_TopVector);
                    cellNodes[1] = NodeHelper.Create(transform, s_TopRightVector);
                    cellNodes[5] = NodeHelper.Create(transform, s_TopLeftVector);
                    cellLinks[0] = new Link(cellNodes[0], cellNodes[1]);
                    cellLinks[5] = new Link(cellNodes[5], cellNodes[0]);

                    nodeAdditions.Add(0);
                    nodeAdditions.Add(1);
                    nodeAdditions.Add(5);
                    linkAdditions.Add(0);
                    linkAdditions.Add(5);
                }

                if (previousLink is null)
                {
                    // It's null, so that means generate the left-most Link
                    cellLinks[4] = new Link(cellNodes[4], cellNodes[5]);

                    linkAdditions.Add(4);
                }
                else
                {
                    // It's found, so that knocks off 2 Nodes and a Link that need to be created
                    cellLinks[4] = previousLink.Reversed();
                }

                // Generate the rest of the Nodes and Links:

                // Nodes: 2, 3
                cellNodes[2] = NodeHelper.Create(transform, s_BottomRightVector);
                cellNodes[3] = NodeHelper.Create(transform, s_BottomVector);

                // Links: 1, 2, 3
                cellLinks[1] = new Link(cellNodes[1], cellNodes[2]);
                cellLinks[2] = new Link(cellNodes[2], cellNodes[3]);
                cellLinks[3] = new Link(cellNodes[3], cellNodes[4]);

                nodeAdditions.Add(2);
                nodeAdditions.Add(3);
                linkAdditions.Add(1);
                linkAdditions.Add(2);
                linkAdditions.Add(3);

                var cell = new Cell(cellNodes, cellLinks);
                cells.Add(cell);

                nodeAdditions.Sort();
                linkAdditions.Sort();

                nodes.AddRange(nodeAdditions.Select(index => cellNodes[index]));
                links.AddRange(linkAdditions.Select(index => cellLinks[index]));

                previousLink = cellLinks[1];

                if (startOffset)
                {
                    previousLinks.Append(cellLinks[3]);
                }

                previousLinks.Append(cellLinks[2]);
            }

            startOffset = !startOffset;
        }

        return new TemplateNetwork(nodes, links, cells);
    }
}