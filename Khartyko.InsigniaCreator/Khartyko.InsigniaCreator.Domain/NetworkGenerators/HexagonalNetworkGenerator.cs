using System.Text.Json;

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Domain.Utility;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Domain.NetworkGenerators;

struct BoundInfo
{
    public int Lower { get; set; }
    public int Upper { get; set; }

    public readonly override string ToString() => $"[{Lower}-{Upper}]";
}

struct CellRowInfo
{
    public int Top { get; set; }
    public int Mid { get; set; }
    public int Bot { get; set; }

    public readonly override string ToString()
    {

        return $"[Top: {Top}, Mid: {Mid}, Bot: {Bot}]";
    }
}

/// <summary>
/// Represents an instance of a NetworkGenerator that generates TemplateNetworks based on a hexagonal grid.
/// </summary>
public class HexagonalNetworkGenerator : INetworkGenerator<HexagonalNetworkData>
{
    private static readonly double s_CellWidth = Math.Sqrt(0.1875);
    private static readonly double s_CellHeight = 0.5;

    private readonly INetworkCalculator<HexagonalNetworkData> _calculator;

    private static string GenerateLoggingFilepath(string directory, string extension)
    {
        string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string path = Path.Combine(userDirectory, "Khartyko", "InsigniaCreator", "Logging", DateTime.Now.ToString("dd MMM yyyy"), directory);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        return Path.Combine(path, $"{DateTime.Now:HH_mm_ss}.{extension}");
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
        int[] cellRowCounts = CalculateCellRowCounts(horizontalCount, verticalCount, startOffset);

        Node[] nodes = GenerateNodes(nodeRowCounts, nodeCount);
        Link[] links = GenerateLinks(nodeRowCounts, linkRowCounts, nodes, linkCount, startOffset);
        Cell[] cells = GenerateCells(nodeRowCounts, linkRowCounts, cellRowCounts, nodes, links, cellCount, startOffset);

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

        var linkInfo = new
        {
            LinkData = new Dictionary<int, string>[linkRowCounts.Length]
        };

        for (var i = 0; i < linkInfo.LinkData.Length; i++)
        {
            linkInfo.LinkData[i] = new Dictionary<int, string>();
        }

        // Create all of the angled Links
        for (var y = 0; y < linkRowCounts.Length; y += 2)
        {
            int rowCount = linkRowCounts[y];

            for (var x = 0; x < rowCount; x++)
            {
                Node head = nodes[lastNodeIndex];
                Node tail = nodes[lastNodeIndex + 1];

                linkInfo.LinkData[y][lastLinkIndex] = $"{lastNodeIndex} - {lastNodeIndex + 1}";

                var link = new Link(head, tail);
                links[lastLinkIndex] = link;

                lastNodeIndex++;
                lastLinkIndex++;
            }

            lastLinkIndex += linkRowCounts[Math.Min(y + 1, linkRowCounts.Length - 1)];

            lastNodeIndex++;
            lastRowIndex++;
        }

        lastNodeIndex = 0;
        lastLinkIndex = linkRowCounts[0];
        lastRowIndex = 0;

        int startOffsetBit = Convert.ToInt32(startOffset);
        int startNormalBit = 1 - startOffsetBit;
        int evenBit = Convert.ToInt32(MathHelper.IsEven((linkRowCounts.Length - 1) / 2));
        int oddBit = 1 - evenBit;
        int nodeCountInRow = nodeRowCounts[1];

        for (var x = 0; x < linkRowCounts[1]; x++)
        {
            Node head = nodes[lastNodeIndex];
            Node tail = nodes[lastNodeIndex + nodeCountInRow - startOffsetBit];

            linkInfo.LinkData[1][lastLinkIndex] = $"{lastNodeIndex} - {lastNodeIndex + nodeCountInRow - startOffsetBit}";

            var link = new Link(head, tail);
            links[lastLinkIndex] = link;

            lastNodeIndex += 2;
            lastLinkIndex++;
        }

        lastRowIndex++;
        lastNodeIndex -= startOffsetBit;

        // Create all of the vertical Links
        for (var y = 3; y < linkRowCounts.Length - 2; y += 2)
        {
            lastLinkIndex += linkRowCounts[y - 1];
            int rowCount = linkRowCounts[y];
            int lastNodeRowCount = nodeRowCounts[lastRowIndex];

            for (var x = 0; x < rowCount; x++)
            {
                Node head = nodes[lastNodeIndex];
                Node tail = nodes[lastNodeIndex + lastNodeRowCount];

                linkInfo.LinkData[y][lastLinkIndex] = $"{lastNodeIndex} - {lastNodeIndex + lastNodeRowCount}";

                var link = new Link(head, tail);
                links[lastLinkIndex] = link;

                lastNodeIndex += 2;
                lastLinkIndex++;
            }

            lastRowIndex++;
        }

        lastLinkIndex += linkRowCounts[^3];
        nodeCountInRow = nodeRowCounts[^2];
        int offsetLastRow = evenBit & startNormalBit | oddBit & startOffsetBit;

        for (var x = 0; x < linkRowCounts[^2]; x++)
        {
            Node head = nodes[lastNodeIndex];
            Node tail = nodes[lastNodeIndex + nodeCountInRow - offsetLastRow];

            linkInfo.LinkData[^2][lastLinkIndex] = $"{lastNodeIndex} - {lastNodeIndex + nodeCountInRow - offsetLastRow}";

            var link = new Link(head, tail);
            links[lastLinkIndex] = link;

            lastNodeIndex += 2;
            lastLinkIndex++;
        }

        // var options = new JsonSerializerOptions
        // {
        //     WriteIndented = true,
        //
        // };

        // string jsonText = JsonSerializer.Serialize(linkInfo, options);

        // var jsonFilepath = GenerateLoggingFilepath("Links", "jsonc");
        // File.WriteAllText(jsonFilepath, jsonText);

        // Environment.Exit(0);

        return links;
    }

    private static Cell[] GenerateCells(int[] nodeRowCounts, int[] linkRowCounts, int[] cellRowCounts, Node[] nodes, Link[] links, int cellCount, bool startOffset)
    {
        var cells = new Cell[cellCount];

        int offsetBit = Convert.ToInt32(startOffset);

        // Node indices
        int nodeIndex = 0;
        int nodeRowIndex = 0;
        int nodeRowCount = nodeRowCounts[nodeRowIndex] - 1;

        int linkIndex = 0;
        int linkRowIndex = 0;
        int linkRowCount = linkRowCounts[linkRowIndex] - 1;

        int cellIndex = 0;
        int cellRowIndex = 0;
        int cellRowCount = cellRowCounts[cellRowIndex];

        var cellRowInfo = new List<CellRowInfo>();

        // Handle the first row of Cells
        for (var x = 0; x < cellRowCount; x++)
        {
            #region Node Indexing
            
            int upperStartIndex = nodeIndex;
            int lowerStartIndex = nodeIndex + nodeRowCount + offsetBit + 1;

            #endregion Node Indexing

            #region Link Indexing

            int topRowCount = linkRowCounts[linkRowIndex];
            int midRowCount = linkRowCounts[linkRowIndex + 1];
            
            int topLinkStartIndex = linkIndex;
            int midLinkStartIndex = linkIndex + topRowCount + offsetBit;
            int botLinkStartIndex = linkIndex + topRowCount + midRowCount + offsetBit;

            #endregion Link Indexing
            
            var cellInfo = new CellRowInfo
            {
                Top = topLinkStartIndex,
                Mid = midLinkStartIndex,
                Bot = botLinkStartIndex
            };

            cellRowInfo.Add(cellInfo);

            nodeIndex += 2;
            cellIndex++;
        }

        cellRowInfo = cellRowInfo.GetRange(0, 1).ToList();
        offsetBit = 1 - offsetBit;
        nodeIndex++;
        nodeRowIndex++;
        cellRowIndex++;

        for (int y = cellRowIndex; y < cellRowCounts.Length - 1; y++)
        {
            cellRowCount = cellRowCounts[y];

            #region Node Indexing

            // Get the length of each Node count in the row
            nodeRowCount = nodeRowCounts[nodeRowIndex] - 1;
            int nodeRowCountWithOffset = nodeRowCount - 2 * offsetBit;

            int upperStartIndex = nodeIndex + offsetBit;
            int upperNodeRowCount = upperStartIndex + nodeRowCountWithOffset;

            int lowerStartIndex = nodeIndex + nodeRowCount + offsetBit + 1;
            int lowerNodeRowCount = lowerStartIndex + nodeRowCountWithOffset;

            #endregion Node Indexing

            #region Link Indexing
            
            int topLinkStartIndex = linkIndex;
            int midLinkStartIndex = linkIndex;
            int botLinkStartIndex = linkIndex;

            #endregion Link Indexing
            
            var cellInfo = new CellRowInfo
            {
                Top = topLinkStartIndex,
                Mid = midLinkStartIndex,
                Bot = botLinkStartIndex
            };

            cellRowInfo.Add(cellInfo);

            nodeIndex += nodeRowCounts[nodeRowIndex];
            cellIndex += cellRowCount;

            // Toggle the 'offset/normal' bits
            offsetBit = 1 - offsetBit;

            nodeRowIndex++;
            
            cellRowIndex++;
        }

        cellRowCount = cellRowCounts[^1];
        nodeRowCount = nodeRowCounts[nodeRowIndex] - 1;
        int normalBit = Convert.ToInt32(!startOffset);
        
        for (var x = 0; x < cellRowCount; x++)
        {
            #region Node Indexing

            // Get the length of each Node count in the row
            int nodeRowCountWithOffset = nodeRowCount - 2 * offsetBit;

            int upperStartIndex = nodeIndex + offsetBit;
            int upperNodeRowCount = upperStartIndex + nodeRowCountWithOffset;

            int lowerStartIndex = nodeIndex + nodeRowCount + 1;
            int lowerNodeRowCount = lowerStartIndex + nodeRowCountWithOffset;

            #endregion Node Indexing

            #region Link Indexing
            
            int topLinkStartIndex = linkIndex;
            int midLinkStartIndex = linkIndex;
            int botLinkStartIndex = linkIndex;

            #endregion Link Indexing
            
            var cellInfo = new CellRowInfo
            {
                Top = topLinkStartIndex,
                Mid = midLinkStartIndex,
                Bot = botLinkStartIndex
            };

            cellRowInfo.Add(cellInfo);

            nodeIndex += 2;
            cellIndex++;
        }

        cellRowInfo = cellRowInfo.GetRange(0, cellRowCounts.Length);
        
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string jsonText = JsonSerializer.Serialize(cellRowInfo, options);

        var jsonFilepath = GenerateLoggingFilepath("Cells", "jsonc");
        File.WriteAllText(jsonFilepath, jsonText);

        Environment.Exit(0);

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
        offsetBit = 1 - offsetBit;

        offsetBit = evenBit & normalBit | oddBit & offsetBit;
        cellRowCounts[^1] = horizontalCount - offsetBit;

        return cellRowCounts;
    }
}