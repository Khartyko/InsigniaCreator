using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Domain.Utility;
using Khartyko.InsigniaCreator.Library.Data;
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
        AssertionHelper.MinimumCheck(generationData.HorizontalCellCount, 1, nameof(generationData.HorizontalCellCount));
        AssertionHelper.MinimumCheck(generationData.VerticalCellCount, 1, nameof(generationData.VerticalCellCount));
        AssertionHelper.NullCheck(generationData.CellTransform, nameof(generationData.CellTransform));
        AssertionHelper.NullCheck(generationData.StartFlipped, nameof(generationData.StartFlipped));

        var horizontalCount = CellCounterHelper.ConstrainCountByCentering(generationData.CenterAlongYAxis, generationData.VerticalCellCount);
        var verticalCount = CellCounterHelper.ConstrainCountByCentering(generationData.CenterAlongXAxis, generationData.HorizontalCellCount);
        var transform = new Transform(generationData.CellTransform);
        var startFlipped = generationData.StartFlipped;
        var flip = startFlipped;

        int nodeCount = _calculator.CalculateNodeCount(generationData);
        int linkCount = _calculator.CalculateLinkCount(generationData);
        int cellCount = _calculator.CalculateCellCount(generationData);

        var nodes = new List<Node>(nodeCount);
        var links = new List<Link>(linkCount);
        var cells = new List<Cell>(cellCount);

        var verticalNodeCount = verticalCount + 1;
        var halfVerticalNodeCount = verticalCount / 2.0;

        int GetIndex(int y, int x) => y * horizontalCount + y / 2 + x;

        // Generate Nodes
        for (var y = 0; y < verticalNodeCount; y++)
        {
            var horizontalNodeCount = horizontalCount + Convert.ToInt32(flip);
            var halfHorizontalNodeCount = horizontalNodeCount / 2.0;

            for (var x = 0; x < horizontalNodeCount; x++)
            {
                var position = new Vector2(x + 0.5 - halfHorizontalNodeCount, y - halfVerticalNodeCount);
                var node = NodeHelper.Create(transform, position);

                nodes.Add(node);
            }

            flip = !flip;
        }

        flip = startFlipped;
        var previousMiddles = new LinkedList<Link>();
        var horizontalCellCount = horizontalCount * 2 - 1;

        // Generate Links and Cells
        for (var y = 0; y < verticalCount; y++)
        {
            var flippedValue = Convert.ToInt32(flip);
            var invertedFlippedValue = Convert.ToInt32(!flip);

            // Set up the initial triangle nodes
            var leftIndex = GetIndex(y + invertedFlippedValue, 0);
            var middleIndex = GetIndex(y + flippedValue, 0);
            var rightIndex = GetIndex(y + invertedFlippedValue, 1);
            Link? previousRight = null;

            for (var x = 0; x < horizontalCellCount; x++)
            {
                var leftNode = nodes[leftIndex];
                var middleNode = nodes[middleIndex];
                var rightNode = nodes[rightIndex];
                
                Link leftLink;
                Link middleLink;
                Link rightLink;
                
                if (flip)
                {
                    // Consume the middle relevant to this flipped triangle
                    var previousMiddle = previousMiddles.FirstOrDefault();
                    // Remove the link in question
                    previousMiddles.RemoveFirst();

                    leftLink = previousRight?.Reversed() ?? new Link(middleNode, leftNode);
                    middleLink = previousMiddle?.Reversed() ?? new Link(leftNode, rightNode);
                    rightLink = new Link(rightNode, middleNode);
                }
                else
                {
                    leftLink = previousRight?.Reversed() ?? new Link(leftNode, middleNode);
                    middleLink = new Link(rightNode, leftNode);
                    rightLink = new Link(middleNode, rightNode);

                    // Add the middle for the next row
                    previousMiddles.AddLast(middleLink);
                }
                
                var cell = new Cell(
                    new List<Node>
                    {
                        middleNode,
                        rightNode,
                        leftNode
                    },
                    new List<Link>
                    {
                        rightLink,
                        middleLink,
                        leftLink
                    }
                );

                cells.Add(cell);

                // Next row will mirror the previous row
                flip = !flip;
                
                // Set the previous right link used
                previousRight = leftLink;

                Console.WriteLine($"Triangle made with indices: <{leftIndex}, {middleIndex}, {rightIndex}>");
                
                // Set up the nodes for the next triangle
                leftIndex = middleIndex;
                middleIndex = rightIndex;
                rightIndex = leftIndex + 1;

            }
            
            Console.WriteLine("==================================================================================");
        }

        return new TemplateNetwork(nodes, links, cells);
    }
}