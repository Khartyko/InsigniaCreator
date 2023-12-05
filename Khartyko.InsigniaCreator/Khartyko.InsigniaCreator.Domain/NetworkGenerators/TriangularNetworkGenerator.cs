using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Domain.Utility;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;
using System.ComponentModel;
using System.IO;

namespace Khartyko.InsigniaCreator.Domain.NetworkGenerators;

/// <summary>
/// Represents an instance of a NetworkGenerator that generates TemplateNetworks based on a triangular grid.
/// </summary>
public class TriangularNetworkGenerator : INetworkGenerator<TriangularNetworkData>
{
    private static string s_directory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".khartyko",
        "InsigniaCreator",
        "Logging"
    );
    private static string s_logFile => Path.Combine(
        s_directory,
        @$"TriangularNetworkGenerator_GenerateNetwork {DateTime.Now:dd MMM yyyy HH_mm_ss}.log"
    );

    private readonly INetworkCalculator<TriangularNetworkData> _calculator;

    private static char GetLetter(int index) => (char)('A' + index);

    private static void LogToFile(string content)
    {
        //File.AppendAllText(s_logFile, content);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="calculator"></param>
    public TriangularNetworkGenerator(INetworkCalculator<TriangularNetworkData> calculator)
    {
        _calculator = calculator;

        if (!Directory.Exists(s_directory))
        {
            Directory.CreateDirectory(s_directory);
        }

        if (File.Exists(s_logFile))
        {
            File.Delete(s_logFile);
            File.Create(s_logFile);
        }
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
        AssertionHelper.MinimumCheck(generationData.Width, 1, nameof(generationData.Width));
        AssertionHelper.MinimumCheck(generationData.Height, 1, nameof(generationData.Height));
        AssertionHelper.NullCheck(generationData.CenterAlongXAxis, nameof(generationData.CenterAlongXAxis));
        AssertionHelper.NullCheck(generationData.CenterAlongYAxis, nameof(generationData.CenterAlongYAxis));
        AssertionHelper.MinimumCheck(generationData.HorizontalCellCount, 1, nameof(generationData.HorizontalCellCount));
        AssertionHelper.MinimumCheck(generationData.VerticalCellCount, 1, nameof(generationData.VerticalCellCount));
        AssertionHelper.NullCheck(generationData.CellTransform, nameof(generationData.CellTransform));
        AssertionHelper.NullCheck(generationData.StartFlipped, nameof(generationData.StartFlipped));

        var horizontalCount = CellCounterHelper.ConstrainCountByCentering(generationData.CenterAlongYAxis, generationData.HorizontalCellCount);
        var verticalCount = CellCounterHelper.ConstrainCountByCentering(generationData.CenterAlongXAxis, generationData.VerticalCellCount);
        var transform = new Transform(generationData.CellTransform);
        var startFlipped = generationData.StartFlipped;

        int nodeCount = _calculator.CalculateNodeCount(generationData);
        int linkCount = _calculator.CalculateLinkCount(generationData);
        int cellCount = _calculator.CalculateCellCount(generationData);

        Node[] nodes = new Node[nodeCount];
        Link[] links = new Link[linkCount];
        Cell[] cells = new Cell[cellCount];

        int flippedBit = Convert.ToInt32(startFlipped);
        var verticalNodeCount = verticalCount + 1;
        var halfVerticalNodeCount = verticalCount / 2.0;
        int currentNodePosition = 0;

        // Generate the first row of nodes
        for (var y = 0; y < verticalNodeCount; y++)
        {
            var horizontalNodeCount = horizontalCount + flippedBit;
            var halfHorizontalNodeCount = horizontalNodeCount / 2.0;

            for (var x = 0; x < horizontalNodeCount; x++)
            {
                var position = new Vector2(x + 0.5 - halfHorizontalNodeCount, y - halfVerticalNodeCount);
                var node = NodeHelper.Create(transform, position);

                nodes[currentNodePosition] = node;
                currentNodePosition++;
            }

            // Update the state of flippedBit
            flippedBit = 1 - flippedBit;
        }

        // Reset the flippedBit
        flippedBit = Convert.ToInt32(startFlipped);
        currentNodePosition = Convert.ToInt32(!startFlipped && horizontalCount == 1 && verticalCount == 1);
        int currentLinkIndex = 0;
        int horizontalCellCount = horizontalCount * 2;

        for (int y = 0; y < verticalNodeCount; y++)
        {
            int horizontalNodeCount = horizontalCount - (1 - flippedBit);

            for (int x = 0; x < horizontalNodeCount; x++)
            {
                var position = currentNodePosition + x;

                Node head = nodes[position];
                Node tail = nodes[position + 1];

                var link = new Link(head, tail);
                links[currentLinkIndex] = link;

                currentLinkIndex++;
            }

            flippedBit = 1 - flippedBit;
            currentLinkIndex += horizontalCellCount;
            currentNodePosition += horizontalNodeCount;
        }

        // Go through and create and add the bridging Links and Cells to their respective lists
        int lastCellIndex = 0;
        // Reset the flippedBit
        flippedBit = Convert.ToInt32(startFlipped);
        currentNodePosition = 0;
        int currentLateralLinkIndex = 0;
        currentLinkIndex = horizontalCount + flippedBit;

        var positionRetrievalFuncs = new Func<(int, int, int)>[2]
        {
            // flippedBit is 0
            () =>
            {
                int firstNodeIndex = currentNodePosition;
                int secondNodeIndex = currentNodePosition + horizontalCount;
                int thirdNodeIndex = secondNodeIndex + 1;

                return (firstNodeIndex, secondNodeIndex, thirdNodeIndex);
            },
            // flippedBit is 1
            () =>
            {
                int firstNodeIndex = currentNodePosition;
                int secondNodeIndex = currentNodePosition + 1;
                int thirdNodeIndex = secondNodeIndex + horizontalCount;

                return (firstNodeIndex, secondNodeIndex, thirdNodeIndex);
            }
        };

        var cellCreationFuncs = new Func<Cell>[2, 2]
        {
            // These methods are for any position that isn't the first -- only one Link will have to be created
            {
                // The Cell is upright, with a single Node at the top
                () =>
                {
                    LogToFile("\t\tSubsequent normal Cell:\n");

                    // Get the relevant Nodes
                    var (firstNodePosition, secondNodePosition, thirdNodePosition) = positionRetrievalFuncs[flippedBit]();

                    LogToFile($"\t\t- Retrieved Node positions:  [ {GetLetter(firstNodePosition)}, {GetLetter(secondNodePosition)}, {GetLetter(thirdNodePosition)} ]\n");

                    var cellNodes = new List<Node>
                    {
                        nodes[firstNodePosition],
                        nodes[secondNodePosition],
                        nodes[thirdNodePosition]
                    };

                    // Create the bridging Link
                    var bridgingLink = new Link(cellNodes[2], cellNodes[0]);

                    // Update the bridging Link in the Links list with its respective position
                    int bridgingLinkIndex = currentLinkIndex;
                    links[bridgingLinkIndex] = bridgingLink;

                    // Get the positions of the existing Links
                    int leftLinkIndex = currentLinkIndex - 1;
                    Link leftLink = links[leftLinkIndex];
                    int lateralLinkIndex = currentLateralLinkIndex + (horizontalCount * 3 - 1);
                    Link lateralLink = links[lateralLinkIndex];

                    LogToFile($"\t\t- Using Link positions:      [ {leftLinkIndex}, *{bridgingLinkIndex}, {lateralLinkIndex} ]\n");

                    var cellLinks = new List<Link>
                    {
                        leftLink,
                        bridgingLink,
                        lateralLink
                    };

                    // Create the Cell using the data
                    return new Cell(cellNodes, cellLinks);
                },
                // The Cell is flipped, with a single Node at the bottom
                () =>
                {
                    LogToFile("\t\tSubsequent flipped Cell:\n");

                    // Get the relevant Nodes
                    var (firstNodePosition, secondNodePosition, thirdNodePosition) = positionRetrievalFuncs[flippedBit]();

                    LogToFile($"\t\t- Retrieved Node positions:  [ {GetLetter(firstNodePosition)}, {GetLetter(secondNodePosition)}, {GetLetter(thirdNodePosition)} ]\n");

                    var cellNodes = new List<Node>
                    {
                        nodes[firstNodePosition],
                        nodes[secondNodePosition],
                        nodes[thirdNodePosition]
                    };

                    // Create the bridging Link
                    var bridgingLink = new Link(cellNodes[1], cellNodes[2]);

                    // Update the bridging Link in the Links list with its respective position
                    int bridgingLinkIndex = currentLinkIndex;
                    links[bridgingLinkIndex] = bridgingLink;

                    // Get the positions of the existing Links
                    int leftLinkIndex = currentLinkIndex - 1;
                    Link leftLink = links[leftLinkIndex];
                    Link lateralLink = links[currentLateralLinkIndex];

                    LogToFile($"\t\t- Using Link positions:      [ {leftLinkIndex}, *{bridgingLinkIndex}, {currentLateralLinkIndex} ]\n");

                    var cellLinks = new List<Link>
                    {
                        leftLink,
                        bridgingLink,
                        lateralLink
                    };
                    
                    // Advance to the next lateral Link position
                    currentLateralLinkIndex++;

                    // Create the Cell using the data
                    return new Cell(cellNodes, cellLinks);
                }
            },
            // These methods are for only the first position in a row
            {
                // The Cell is upright, with a single Node at the top
                () =>
                {
                    LogToFile("\t\tFirst normal Cell:\n");

                    // Get the relevant Nodes
                    var (firstNodePosition, secondNodePosition, thirdNodePosition) = positionRetrievalFuncs[flippedBit]();

                    LogToFile($"\t\t- Retrieved Node positions:  [ {GetLetter(firstNodePosition)}, {GetLetter(secondNodePosition)}, {GetLetter(thirdNodePosition)} ]\n");

                    var cellNodes = new List<Node>
                    {
                        nodes[firstNodePosition],
                        nodes[secondNodePosition],
                        nodes[thirdNodePosition]
                    };

                    // Create the bridging Link
                    var bridgingLeftLink = new Link(cellNodes[0], cellNodes[1]);
                    var bridgingRightLink = new Link(cellNodes[2], cellNodes[0]);

                    // Update the bridging Link in the Links list with its respective position
                    int bridgingLeftLinkIndex = currentLinkIndex - 1;
                    links[bridgingLeftLinkIndex] = bridgingLeftLink;
                    int bridgingRightLinkIndex = currentLinkIndex;
                    links[bridgingRightLinkIndex] = bridgingRightLink;

                    // Get the positions of the existing Links
                    int lateralLinkIndex = currentLateralLinkIndex + (horizontalCount * 3 - 1);
                    Link lateralLink = links[lateralLinkIndex];

                    LogToFile($"\t\t- Using Link positions:      [ *{bridgingLeftLinkIndex}, *{bridgingRightLinkIndex}, {lateralLinkIndex} ]\n");

                    var cellLinks = new List<Link>
                    {
                        bridgingLeftLink,
                        bridgingRightLink,
                        lateralLink
                    };

                    // Create the Cell using the data
                    return new Cell(cellNodes, cellLinks);
                },
                // The Cell is flipped, with a single Node at the bottom
                () =>
                {
                    LogToFile("\t\tFirst flipped Cell:\n");

                    // Get the relevant Nodes
                    var (firstNodePosition, secondNodePosition, thirdNodePosition) = positionRetrievalFuncs[flippedBit]();

                    LogToFile($"\t\t- Retrieved Node positions:  [ {GetLetter(firstNodePosition)}, {GetLetter(secondNodePosition)}, {GetLetter(thirdNodePosition)} ]\n");

                    var cellNodes = new List<Node>
                    {
                        nodes[firstNodePosition],
                        nodes[secondNodePosition],
                        nodes[thirdNodePosition]
                    };
                    
                    // Create the bridging Link
                    var bridgingLeftLink = new Link(cellNodes[2], cellNodes[0]);
                    var bridgingRightLink = new Link(cellNodes[1], cellNodes[2]);

                    // Update the bridging Link in the Links list with its respective position
                    int bridgingLeftLinkIndex = currentLinkIndex - 1;
                    links[bridgingLeftLinkIndex] = bridgingLeftLink;
                    int bridgingRightLinkIndex = currentLinkIndex;
                    links[bridgingRightLinkIndex] = bridgingRightLink;

                    // Get the positions of the existing Links
                    Link lateralLink = links[currentLateralLinkIndex];

                    LogToFile($"\t\t- Using Link positions:      [ *{bridgingLeftLinkIndex}, *{bridgingRightLinkIndex}, {currentLateralLinkIndex} ]\n");

                    var cellLinks = new List<Link>
                    {
                        bridgingLeftLink,
                        bridgingRightLink,
                        lateralLink
                    };
                    
                    // Advance to the next lateral Link position
                    currentLateralLinkIndex++;

                    // Create the Cell using the data
                    return new Cell(cellNodes, cellLinks);
                }
            }
        };

        for (int y = 0; y < verticalCount; y++)
        {
            int rowStartFlippedBitState = flippedBit;

            int isFirstNodeBit = 1;

            LogToFile("Vertical Iteration Start:");

            for (int x = 1; x < horizontalCellCount; x++)
            {
                LogToFile("\n\tHorizontal Iteration Start:\n");

                Cell cell = cellCreationFuncs[isFirstNodeBit, flippedBit]();

                // Nullify this bit
                isFirstNodeBit *= 0;

                // Add the cell to the cells list
                cells[lastCellIndex] = cell;

                // Update the current Node index
                currentNodePosition += flippedBit;

                // Advance to the next Cell position
                lastCellIndex++;

                // Advance this for the next Link that'll be used
                currentLinkIndex++;

                // Toggle the flippedBit
                flippedBit = (1 - flippedBit);

                LogToFile("\tHorizontal Iteration End\n");
            }

            // Update the link index
            currentLinkIndex += horizontalCount + flippedBit;
            currentLateralLinkIndex += horizontalCount * 2;
            currentNodePosition++;

            LogToFile("Vertical Iteration End\n\n");
        }

        return new TemplateNetwork(nodes, links, cells);
    }
}