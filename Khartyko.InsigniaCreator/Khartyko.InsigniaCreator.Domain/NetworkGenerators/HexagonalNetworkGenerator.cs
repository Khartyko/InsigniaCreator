using System.Linq;
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

        int nodeCount = _calculator.CalculateNodeCount(generationData);
        int linkCount = _calculator.CalculateLinkCount(generationData);
        int cellCount = _calculator.CalculateCellCount(generationData);

        Node[] nodes = new Node[nodeCount];
        Link[] links = new Link[linkCount];
        Cell[] cells = new Cell[cellCount];

        int[] nodeRowCounts = CalculateNodeRowCounts(horizontalCount, verticalCount, generationData.StartOffset);
        int[] linkRowCounts = CalculateLinkRowCounts(horizontalCount, verticalCount, generationData.StartOffset);
        int[] cellRowCounts = CalculateCellRowCounts(horizontalCount, verticalCount, generationData.StartOffset);

        return new TemplateNetwork(nodes, links, cells);
    }

    private static int[] CalculateNodeRowCounts(
        int horizontalCount,
        int verticalCount,
        bool startOffset
    )
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

    private static int[] CalculateLinkRowCounts(
        int horizontalCount,
        int verticalCount,
        bool startOffset
    )
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

    private static int[] CalculateCellRowCounts(
        int horizontalCount,
        int verticalCount,
        bool startOffset
    )
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