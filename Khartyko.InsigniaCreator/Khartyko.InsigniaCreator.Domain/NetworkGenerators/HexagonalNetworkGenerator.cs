using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Domain.Utility;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

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

        var indexStrings = new List<string>
        {
            "Input NetworkData:",
            $"\tWidth:                  {networkData.Width}",
            $"\tHeight:                 {networkData.Height}",
            $"\tHorizontalCellCount:    {networkData.HorizontalCellCount}",
            $"\tVerticalCellCount:      {networkData.VerticalCellCount}",
            $"\tCenterAlongXAxis:       {networkData.CenterAlongXAxis}",
            $"\tCenterAlongYAxis:       {networkData.CenterAlongYAxis}",
            $"\tStartOffset:            {networkData.StartOffset}",
            $"\tCellTransform:",
            $"\t\tScale:                {networkData.CellTransform.Scale}",
            $"\t\tRotation:             {networkData.CellTransform.Rotation}",
            $"\t\tTranslation:          {networkData.CellTransform.Translation}",
            "\nLink connections formed:"
        };

        int offsetBit = Convert.ToInt32(networkData.StartOffset);
        int normalBit = Convert.ToInt32(!networkData.StartOffset);

        int currentNodeIndex = 0;
        int currentLinkIndex = 0;

        Link[] links = new Link[linkCount];

        /*
         * For angledRowType:
         * - 0: Starting
         * - 1: Ending
         */
        int angledRowType = 1;
        /*
         * For rowTypeBit:
         * - 0: Angled
         * - 1: Vertical
         */
        int rowTypeBit = 0;
        int horizontalLinkCount = 2 * (horizontalCount - offsetBit);

        var angledLinkFuncs = new Action[2, 2]
        {
            // Offset = 0
            {
                // angledRowType = 0
                () =>
                {

                },
                // angledRowType = 1
                () =>
                {

                }
            },
            // Offset = 1
            {
                // angledRowType = 0
                () =>
                {

                },
                // angledRowType = 1
                () =>
                {

                }
            }
        };

        var verticalLinkFuncs = new Action[2, 2]
        {
            // Offset = 0
            {
                // angledRowType = 0
                () =>
                {
                    
                },
                // angledRowType = 1
                () =>
                {

                }
            },
            // Offset = 1
            {
                // angledRowType = 0
                () =>
                {

                },
                // angledRowType = 1
                () =>
                {

                }
            }
        };

        var linkCreationFuncs = new Action[2]
        {
            // Row Type 0: Angled Links
            () =>
            {
                int localNodeCount = 2 * horizontalCount;

                for (int x = 0; x < localNodeCount; x++)
                {
                    int headIndex = currentNodeIndex + x;
                    int tailIndex = currentNodeIndex + x + 1;

                    Node head = nodes[headIndex];
                    Node tail = nodes[tailIndex];

                    indexStrings.Add($"\t\t{headIndex} -> {tailIndex}");

                    var link = new Link(head, tail);
                    links[currentLinkIndex] = link;

                    currentLinkIndex++;
                }

                // Update the currentNodeIndex if the row is normal
                currentNodeIndex += normalBit;
                
                // Update the angledRowType, since it alternates
                angledRowType = 1 - angledRowType;

                angledLinkFuncs[offsetBit, angledRowType]();

                // Update the offsetBit, if it's an Angled Ending row
                offsetBit = normalBit & angledRowType;
                normalBit = 1 - offsetBit;
            },
            // Row Type 1: Vertical Links
            () =>
            {
                // How to handle if it's the last row of vertical Links
                // and if it's odd and offset, or even and normal?
                int verticalLinkStride = horizontalCount * 2 + normalBit - offsetBit;

                for (int x = 0; x < verticalLinkStride; x += 2)
                {
                    int headIndex = currentNodeIndex + x;
                    int tailIndex = currentNodeIndex + x + verticalLinkStride;

                    Node head = nodes[headIndex];
                    Node tail = nodes[tailIndex];

                    indexStrings.Add($"\t\t{headIndex} -> {tailIndex}");

                    var link = new Link(head, tail);
                    links[currentLinkIndex] = link;

                    currentLinkIndex++;
                }

                // Update the currentNodeIndex for the next row
                currentNodeIndex += verticalLinkStride;

                verticalLinkFuncs[offsetBit, angledRowType]();

                angledRowType = 0;
                offsetBit *= 0;
                normalBit = 1;
            }
        };

        indexStrings.Add($"\tFirst {(networkData.StartOffset ? "Offset" : "Normal")} Row #0");

        // Row type here will always be angled
        for (int x = 0; x < horizontalLinkCount; x++)
        {
            int headIndex = currentNodeIndex + x;
            int tailIndex = currentNodeIndex + x + 1;

            Node head = nodes[headIndex];
            Node tail = nodes[tailIndex];

            indexStrings.Add($"\t\t{headIndex} -> {tailIndex}");

            var link = new Link(head, tail);
            links[currentLinkIndex] = link;

            currentLinkIndex++;
        }

        rowTypeBit = 1;

        for (int y = 1; y < 2 * verticalCount; y++)
        {
            indexStrings.Add($"\n\tSubsequent {(rowTypeBit == 0 ? "Angled" : "Vertical")} Row #{y}");

            linkCreationFuncs[rowTypeBit]();

            // Toggle the row type, since it alternates
            rowTypeBit = 1 - rowTypeBit;
        }

        // Reset this bit, just in case
        angledRowType = 1;

        string descriptor = MathHelper.IsEven(verticalCount)
            ? networkData.StartOffset ? "Normal" : "Offset"
            : networkData.StartOffset ? "Offset" : "Normal";

        indexStrings.Add($"\n\tFinal {descriptor} Row #{2 * verticalCount - 1}");

        int evenBit = Convert.ToInt32(MathHelper.IsEven(verticalCount));
        int oddBit = 1 - evenBit;

        int originalOffsetBit = Convert.ToInt32(networkData.StartOffset);
        int originalNormalBit = 1 - originalOffsetBit;

        int finalLinkCountModifier = originalOffsetBit & oddBit | originalNormalBit & evenBit;
        int finalLinkCount = 2 * (horizontalCount - finalLinkCountModifier);

        // Row type here will always be angled
        for (int x = 0; x < finalLinkCount; x++)
        {
            int headIndex = currentNodeIndex + x;
            int tailIndex = currentNodeIndex + x + 1;

            Node head = nodes[headIndex];
            Node tail = nodes[tailIndex];

            indexStrings.Add($"\t\t{headIndex} -> {tailIndex}");

            var link = new Link(head, tail);
            links[currentLinkIndex] = link;

            currentLinkIndex++;
        };

        string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string fileName = $"HexagonalNetworkGenerator_GenerateNetwork Links {DateTime.Now:dd MMM yyyy HH_mm_ss}.log";
        string path = Path.Combine(userDirectory, "Khartyko", "InsigniaCreator", "Logging");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        path = Path.Combine(path, fileName);
        
        File.WriteAllLines(path, indexStrings);

        return links;
    }

    private Cell[] SetupCells(HexagonalNetworkData networkData, Node[] nodes, Link[] links)
    {
        var horizontalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongYAxis, networkData.HorizontalCellCount);
        var verticalCount = CellCounterHelper.ConstrainCountByCentering(networkData.CenterAlongXAxis, networkData.VerticalCellCount);
        int cellCount = _calculator.CalculateCellCount(networkData);

        int horizontalNodeCount = 2 * horizontalCount + 1;
        int offsetBit = Convert.ToInt32(networkData.StartOffset);

        int currentNodeIndex = 0;
        int currentLinkIndex = 2 * Math.Max(1, horizontalCount - offsetBit);
        int currentCellIndex = 0;

        Cell[] cells = new Cell[cellCount];

        int topNodeIndex = 0;
        int bottomNodeIndex = 2 * (horizontalCount - offsetBit);

        int topLeftLinkIndex = 0;
        int midLeftLinkIndex = 2 * (horizontalCount - offsetBit);
        int bottomLeftLinkIndex = midLeftLinkIndex + horizontalCount + offsetBit;

        // Setup the bridging Links
        for (var y = 0; y < verticalCount; y++)
        {
            int currentNodeCount = horizontalNodeCount - offsetBit;

            for (var x = offsetBit; x < currentNodeCount; x += 2)
            {
                Node head = nodes[currentNodeIndex + x];
                Node tail = nodes[currentNodeIndex + x + currentNodeCount];

                var link = new Link(head, tail);
                links[currentLinkIndex] = link;
                currentLinkIndex++;
            }

            // Update the currentNodePosition
            currentNodeIndex += horizontalNodeCount;

            // Update the Link position to start from
            currentLinkIndex += 2 * horizontalCount + 1;

            // Create the Cells for the row
            for (var x = 0; x < horizontalCount; x++)
            {
                Node topLeft = nodes[topNodeIndex];
                Node topRight = nodes[topNodeIndex + 1];
                Node left = nodes[topNodeIndex + 2];
                Node right = nodes[bottomNodeIndex];
                Node bottomLeft = nodes[bottomNodeIndex + 1];
                Node bottomRight = nodes[bottomNodeIndex + 2];

                Link topLeftLink = links[topLeftLinkIndex];
                Link topRightLink = links[topLeftLinkIndex + 1];
                Link leftLink = links[midLeftLinkIndex];
                Link rightLink = links[midLeftLinkIndex + 1];
                Link bottomLeftLink = links[bottomLeftLinkIndex];
                Link bottomRightLink = links[bottomLeftLinkIndex + 1];

                var cell = new Cell(
                    new List<Node>
                    {
                        topLeft,
                        topRight,
                        left,
                        right,
                        bottomLeft,
                        bottomRight
                    },
                    new List<Link>
                    {
                        topLeftLink,
                        topRightLink,
                        leftLink,
                        rightLink,
                        bottomLeftLink,
                        bottomRightLink
                    }
                );

                cells[currentCellIndex] = cell;

                // Increment the counters
                currentCellIndex++;
                topNodeIndex += 2;
                bottomNodeIndex += 2;
                topLeftLinkIndex += 2;
                midLeftLinkIndex++;
                bottomLeftLinkIndex += 2;
            }

            // Increment the counters for Node indexes
            topNodeIndex += 3;
            bottomNodeIndex += 3 + offsetBit;

            // Toggle the bits
            offsetBit = (1 - offsetBit);
        }

        return cells;
    }
}