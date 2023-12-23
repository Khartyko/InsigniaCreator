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

    private static readonly string s_loggingFileName = $"HexagonalNetworkGenerator_GenerateNetwork Links {DateTime.Now:HH_mm_ss}.log";

    private static string s_LoggingFilepath
    {
        get
        {
            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string path = Path.Combine(userDirectory, "Khartyko", "InsigniaCreator", "Logging", DateTime.Now.ToString("dd MMM yyyy"));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return Path.Combine(path, s_loggingFileName);
        }
    }

    private static double GetModifiedOscillationModifier(int flippedBit)
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

        int horizontalCount = CellCounterHelper.ConstrainCountByCentering(generationData.CenterAlongYAxis, generationData.HorizontalCellCount);
        int verticalCount = CellCounterHelper.ConstrainCountByCentering(generationData.CenterAlongXAxis, generationData.VerticalCellCount);
        bool startOffset = generationData.StartOffset;

        int nodeCount = _calculator.CalculateNodeCount(generationData);
        int linkCount = _calculator.CalculateLinkCount(generationData);
        int cellCount = _calculator.CalculateCellCount(generationData);

        int[] nodeRowCounts = CalculateNodeRowCounts(horizontalCount, verticalCount, startOffset);
        int[] linkRowCounts = CalculateLinkRowCounts(horizontalCount, verticalCount, startOffset);
        int[] cellRowCounts = CalculateCellRowCounts(horizontalCount, verticalCount, generationData.StartOffset);

        Node[] nodes = GenerateNodes(nodeRowCounts, nodeCount);
        Link[] links = GenerateLinks(nodeRowCounts, linkRowCounts, nodes, linkCount, startOffset);
        Cell[] cells = GenerateCells(nodeRowCounts, linkRowCounts, cellRowCounts, nodes, links, cellCount);

        return new TemplateNetwork(nodes, links, cells);
    }

    private static Node[] GenerateNodes(int[] nodeRowCounts, int nodeCount)
    {
        Node[] nodes = new Node[nodeCount];

        int lastNodeIndex = 0;

        for (var y = 0; y < nodeRowCounts.Length; y++)
        {
            int rowCount = nodeRowCounts[y];

            for (var x = 0; x < rowCount; x++)
            {
                var node = new Node(x, y);

                nodes[lastNodeIndex] = node;

                lastNodeIndex++;
            }
        }

        return nodes;
    }

    private static Link[] GenerateLinks(int[] nodeRowCounts, int[] linkRowCounts, Node[] nodes, int linkCount, bool startOffset)
    {
        Link[] links = new Link[linkCount];

        int lastNodeIndex = 0;
        int lastLinkIndex = 0;
        int lastRowIndex = 0;

        var linkStrings = new List<string>
        {
            "Links:\n\tAngled Link Sections:"
        };

        // Create all of the angled Links
        for (var y = 0; y < linkRowCounts.Length; y += 2)
        {
            int rowCount = linkRowCounts[y];

            for (var x = 0; x < rowCount; x++)
            {
                Node head = nodes[lastNodeIndex];
                Node tail = nodes[lastNodeIndex + 1];

                linkStrings.Add($"\t\t{lastLinkIndex}: {lastNodeIndex} -> {lastNodeIndex + 1}");

                var link = new Link(head, tail);
                links[lastLinkIndex] = link;

                lastNodeIndex++;
                lastLinkIndex++;
            }

            linkStrings.Add(string.Empty);
            lastLinkIndex += linkRowCounts[Math.Min(y + 1, linkRowCounts.Length - 1)];

            lastNodeIndex++;
            lastRowIndex++;
        }

        linkStrings.Add("\tVertical Links Sections:");
        lastNodeIndex = 0;
        lastLinkIndex = linkRowCounts[0] + 1;
        lastRowIndex = 0;

        int startOffsetBit = Convert.ToInt32(startOffset);

        // Create all of the vertical Links
        for (var y = 1; y < linkRowCounts.Length; y += 2)
        {
            int rowCount = linkRowCounts[y];
            int lastNodeRowCount = nodeRowCounts[lastRowIndex];

            for (var x = 0; x < rowCount; x++)
            {
                Node head = nodes[lastNodeIndex];
                Node tail = nodes[lastNodeIndex + lastNodeRowCount];

                linkStrings.Add($"\t\t{lastLinkIndex}: {lastNodeIndex} -> {lastNodeIndex + lastNodeRowCount}");

                var link = new Link(head, tail);
                links[lastLinkIndex] = link;

                lastNodeIndex += 2;
                lastLinkIndex++;
            }

            linkStrings.Add(string.Empty);
            lastNodeIndex -= startOffsetBit;
            lastRowIndex++;

            startOffsetBit = 0;
        }

        File.WriteAllLines(s_LoggingFilepath, linkStrings);

        return links;
    }

    private static Cell[] GenerateCells(int[] nodeRowCounts, int[] linkRowCounts, int[] cellRowCounts, Node[] nodes, Link[] links, int cellCount)
    {
        Cell[] cells = new Cell[cellCount];

        int lastCellIndex = 0;
        int lastNodeIndex = 0;
        int lastNodeRowIndex = 0;
        int lastLinkTopIndex = 0;

        var cellStrings = new List<string>
        {
            "Cells:"
        };

        for (var y = 0; y < cellRowCounts.Length; y++)
        {
            int rowCount = cellRowCounts[y];
            int lastNodeRowCount = nodeRowCounts[lastNodeRowIndex];
            int linkRowIndex = 2 * y;
            int topLinkRowCount = linkRowCounts[linkRowIndex];
            int middleLinkRowCount = linkRowCounts[linkRowIndex + 1];

            for (var x = 0; x < rowCount; x++)
            {
                int nextRowNodeIndex = lastNodeIndex + lastNodeRowCount;

                //Node topLeftNode = nodes[lastNodeIndex];
                //Node topMiddleNode = nodes[lastNodeIndex + 1];
                //Node topRightNode = nodes[lastNodeIndex + 2];
                //Node bottomLeftNode = nodes[nextRowNodeIndex];
                //Node bottomMiddleNode = nodes[nextRowNodeIndex + 1];
                //Node bottomRightNode = nodes[nextRowNodeIndex + 2];

                cellStrings.Add($"Cell #{lastCellIndex + 1}");
                cellStrings.Add("\tCell Nodes:");
                cellStrings.Add($"\t\tTop Left:         {lastNodeIndex}");
                cellStrings.Add($"\t\tTop Middle:       {lastNodeIndex + 1}");
                cellStrings.Add($"\t\tTop Right:        {lastNodeIndex + 2}");
                cellStrings.Add($"\t\tBottom Left:      {nextRowNodeIndex}");
                cellStrings.Add($"\t\tBottom Middle:    {nextRowNodeIndex + 1}");
                cellStrings.Add($"\t\tBottom Right:     {nextRowNodeIndex + 2}");

                int middleRowLinkIndex = lastLinkTopIndex + topLinkRowCount;
                int bottomRowLinkIndex = middleRowLinkIndex + middleLinkRowCount;

                cellStrings.Add("\n\tCell Links:");
                cellStrings.Add($"\t\tTop Left:         {lastLinkTopIndex}");
                cellStrings.Add($"\t\tTop Right:        {lastLinkTopIndex + 1}");
                cellStrings.Add($"\t\tMiddle Left:      {middleRowLinkIndex}");
                cellStrings.Add($"\t\tMiddle Right:     {middleRowLinkIndex + 1}");
                cellStrings.Add($"\t\tBottom Left:      {bottomRowLinkIndex}");
                cellStrings.Add($"\t\tBottom Right:     {bottomRowLinkIndex + 1}");

                lastNodeIndex += 2;
                lastLinkTopIndex += 2;
                lastCellIndex++;

                cellStrings.Add("\n=======================================\n");
            }

            lastNodeIndex += 2;
            lastNodeRowIndex++;
            lastLinkTopIndex += topLinkRowCount + middleLinkRowCount;
        }

        File.AppendAllLines(s_LoggingFilepath, cellStrings);

        return cells;
    }

    private static int[] CalculateNodeRowCounts(int horizontalCount, int verticalCount, bool startOffset)
    {
        int offsetBit = Convert.ToInt32(startOffset);
        int normalBit = 1 - offsetBit;
        int verticalRowCount = verticalCount + 1;

        int[] nodeRowCounts = new int[verticalRowCount];

        nodeRowCounts[0] = 2 * (horizontalCount - offsetBit) + 1;

        int midNodeRowCounts = 2 * horizontalCount + 1;

        for (var i = 1; i < verticalCount; i++)
        {
            nodeRowCounts[i] = midNodeRowCounts;
        }

        int evenBit = Convert.ToInt32(MathHelper.IsEven(verticalCount));
        int oddBit = Convert.ToInt32(MathHelper.IsOdd(verticalCount));

        offsetBit = evenBit & normalBit | oddBit & offsetBit;
        nodeRowCounts[^1] = 2 * (horizontalCount - offsetBit) + 1;

        return nodeRowCounts;
    }

    private static int[] CalculateLinkRowCounts(int horizontalCount, int verticalCount, bool startOffset)
    {
        int offsetBit = Convert.ToInt32(startOffset);
        int normalBit = Convert.ToInt32(!startOffset);
        int verticalNodeCount = 2 * verticalCount + 1;

        /*
         * Row Type:
         * - 0: Angled
         * - 1: Vertical
         */
        int rowType = 0;

        /*
         * Angled Row Type:
         * - 0: Start
         * - 1: End
         */
        int angledRowType = 0;

        var calculateRowCountFuncs = new Func<int>[2]
        {
            () => 2 * (horizontalCount - offsetBit),
            () => horizontalCount + normalBit
        };

        var updateBitsFuncs = new Action[2, 2]
        {
            // Normal row
            {
                // Angled row
                () =>
                {
                    offsetBit = angledRowType;
                    normalBit = 1 - offsetBit;
                    angledRowType = 1 - angledRowType;
                    rowType = 1 - rowType;
                },
                // Vertical row
                () =>
                {
                    angledRowType = 1;
                    offsetBit = 0;
                    normalBit = 1;
                    rowType = 1 - rowType;
                }
            },
            // Offset row
            {
                // Angled row
                () =>
                {
                    rowType = 1;
                    angledRowType = normalBit;
                },
                // Vertical row
                () =>
                {
                    angledRowType = 0;
                    offsetBit = 0;
                    normalBit = 1;
                    rowType = 1 - rowType;
                }
            }
        };

        int[] rowCounts = new int[verticalNodeCount];

        rowCounts[0] = calculateRowCountFuncs[rowType]();

        updateBitsFuncs[offsetBit, rowType]();

        for (var i = 1; i < rowCounts.Length - 1; i++)
        {
            rowCounts[i] = calculateRowCountFuncs[rowType]();
            updateBitsFuncs[offsetBit, rowType]();
        }

        offsetBit = Convert.ToInt32(startOffset);
        normalBit = Convert.ToInt32(!startOffset);

        int evenBit = Convert.ToInt32(MathHelper.IsEven(verticalCount));
        int oddBit = Convert.ToInt32(MathHelper.IsOdd(verticalCount));

        offsetBit = offsetBit & oddBit | normalBit & evenBit;
        angledRowType = 1;
        rowType = 0;

        rowCounts[^1] = calculateRowCountFuncs[rowType]();

        return rowCounts;
    }

    private static int[] CalculateCellRowCounts(int horizontalCount, int verticalCount, bool startOffset)
    {
        int offsetBit = Convert.ToInt32(startOffset);
        int normalBit = 1 - offsetBit;

        int[] cellRowCounts = new int[verticalCount];

        cellRowCounts[0] = horizontalCount - offsetBit;

        for (var i = 1; i < verticalCount - 1; i++)
        {
            offsetBit = 1 - offsetBit;

            cellRowCounts[i] = horizontalCount - offsetBit;
        }

        int evenBit = Convert.ToInt32(MathHelper.IsEven(verticalCount));
        int oddBit = Convert.ToInt32(MathHelper.IsOdd(verticalCount));

        offsetBit = evenBit & normalBit | oddBit & offsetBit;
        cellRowCounts[^1] = horizontalCount - offsetBit;

        return cellRowCounts;
    }
}