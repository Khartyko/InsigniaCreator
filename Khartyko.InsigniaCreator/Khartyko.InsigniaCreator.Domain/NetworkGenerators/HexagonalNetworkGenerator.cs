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
    private static readonly double s_CellWidth = Math.Sqrt(0.75);
    private static readonly double s_CellHeight = 0.75;

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
        DomainAssertionHelper.NetworkDataCheck(generationData);

        var horizontalCount = CellCounterHelper.ConstrainCountByCentering(generationData.CenterAlongYAxis, generationData.HorizontalCellCount);
        var verticalCount = CellCounterHelper.ConstrainCountByCentering(generationData.CenterAlongXAxis, generationData.VerticalCellCount);
        var transform = new Transform(generationData.CellTransform);
        var scale = transform.Scale;

        int nodeCount = _calculator.CalculateNodeCount(generationData);
        int linkCount = _calculator.CalculateLinkCount(generationData);
        int cellCount = _calculator.CalculateCellCount(generationData);

        var nodes = new Node[nodeCount];
        var links = new Link[linkCount];
        var cells = new Cell[cellCount];

        int horizontalNodeCount = 2 * horizontalCount + 1;
        int verticalNodeCount = verticalCount + 1;

        int offsetBit = Convert.ToInt32(generationData.StartOffset);
        int normalBit = Convert.ToInt32(!generationData.StartOffset);

        var globalNodeOffset = new Vector2(
            (generationData.Width - (horizontalNodeCount * scale.X)) / 2,
            (generationData.Height - (verticalNodeCount * scale.Y)) / 2
        );

        int currentNodeIndex = 0;
        int currentLinkIndex = 0;
        int currentCellIndex = horizontalCount - offsetBit;
        double oscillationModifier = -0.125;

        // These are for the start of a row
        var startingRowFuncs = new Action[2]
        {
            // Starting normal row
            () =>
            {
                var localPosition = new Vector2(
                    s_CellWidth * scale.X,
                    s_CellHeight * scale.Y + oscillationModifier
                );

                // Create the first Node
                Node node = NodeHelper.Create(transform, globalNodeOffset + localPosition);
                nodes[currentNodeIndex] = node;
                currentNodeIndex++;
                
                // Add 1 because the above Node is already done
                for (var x = 1; x < horizontalNodeCount; x++)
                {
                    localPosition = new Vector2(
                        s_CellWidth * scale.X * (x + 1),
                        s_CellHeight * scale.Y + oscillationModifier
                    );

                    // Create the next Nodes
                    Vector2 followingPosition = globalNodeOffset + localPosition;

                    Node followingNode = NodeHelper.Create(transform, followingPosition);
                    nodes[currentNodeIndex] = followingNode;

                    // Create the angled Link
                    var link = new Link(node, followingNode);
                    links[currentLinkIndex] = link;

                    // Update the variables
                    oscillationModifier *= -1;
                    currentNodeIndex++;
                    currentLinkIndex++;
                }
            },
            // Starting offset row
            () =>
            {
                var localPosition = new Vector2(
                    s_CellWidth * scale.X,
                    s_CellHeight * scale.Y + oscillationModifier
                );

                // Create the offset that's a result from the offsetBit
                Vector2 offsetFromStartOffset = scale / 2;
                
                // Create the local position of the first Node in the row
                Vector2 position = globalNodeOffset + offsetFromStartOffset + localPosition;
                
                // Create the first Node
                Node node = NodeHelper.Create(transform, position);
                nodes[currentNodeIndex] = node;
                currentNodeIndex++;
                
                // Add 1 because the above Node is already done
                for (var x = 2; x < horizontalNodeCount - 1; x++)
                {
                    localPosition = new Vector2(
                        s_CellWidth * scale.X * x,
                        s_CellHeight * scale.Y + oscillationModifier
                    );

                    // Create the next Nodes
                    Vector2 followingPosition = globalNodeOffset + offsetFromStartOffset + localPosition;

                    Node followingNode = NodeHelper.Create(transform, followingPosition);
                    nodes[currentNodeIndex] = followingNode;

                    // Create the angled Link
                    var link = new Link(node, followingNode);
                    links[currentLinkIndex] = link;

                    // Update the variables
                    oscillationModifier *= -1;
                    currentNodeIndex++;
                    currentLinkIndex++;
                }
            }
        };

        // These are for any subsequent rows
        var subsequentRowFuncs = new Action<int>[2]
        {
            // Subsequent normal rows
            (int y) =>
            {
                int startingNodeIndex = currentNodeIndex;
                int startingLinkIndex = currentLinkIndex;

                var topLocalPosition = new Vector2(
                    s_CellWidth * scale.X,
                    s_CellHeight * scale.Y * y + oscillationModifier
                );

                var bottomLocalPosition = new Vector2(
                    s_CellWidth * scale.X,
                    s_CellHeight * scale.Y * (y + 1) + oscillationModifier
                );

                // Create the first Node of the 1st row
                Node topNode = NodeHelper.Create(transform, globalNodeOffset + topLocalPosition);
                
                Node bottomNode = NodeHelper.Create(transform, globalNodeOffset + bottomLocalPosition);

                nodes[currentNodeIndex] = topNode;
                nodes[currentNodeIndex + horizontalNodeCount] = bottomNode;

                currentNodeIndex++;
                
                int bottomLinkIndexDelta = 3 * horizontalCount + 1;

                // Add 1 because the above Node is already done
                for (var x = 1; x < horizontalNodeCount; x++)
                {
                    topLocalPosition = new Vector2(
                        s_CellWidth * scale.X * (x + 1),
                        s_CellHeight * scale.Y * y + oscillationModifier
                    );

                    bottomLocalPosition = new Vector2(
                        s_CellWidth * scale.X * (x + 1),
                        s_CellHeight * scale.Y * (y + 1) + oscillationModifier
                    );

                    Vector2 topFollowingPosition = globalNodeOffset + topLocalPosition;
                    Vector2 bottomFollowingPosition = globalNodeOffset + bottomLocalPosition;
                    
                    // Create the next top and bottom Nodes
                    Node topFollowingNode = NodeHelper.Create(transform, topFollowingPosition);
                    Node bottomFollowingNode = NodeHelper.Create(transform, bottomFollowingPosition);

                    nodes[currentNodeIndex] = topFollowingNode;
                    nodes[currentNodeIndex + horizontalNodeCount] = bottomFollowingNode;

                    // Create the angled Links
                    var topLink = new Link(topNode, topFollowingNode);
                    var bottomLink = new Link(bottomNode, bottomFollowingNode);

                    links[currentLinkIndex] = topLink;
                    links[currentLinkIndex + bottomLinkIndexDelta] = bottomLink;

                    topNode = topFollowingNode;
                    bottomNode = bottomFollowingNode;

                    // Update and/or increment the variables
                    oscillationModifier *= -1;
                    currentNodeIndex++;
                    currentLinkIndex++;
                }

                currentNodeIndex -= horizontalNodeCount;

                int verticalLinksAdded = 0;

                for(var i = 0; i < horizontalNodeCount; i += 2)
                {
                    Node head = nodes[currentNodeIndex + i];
                    Node tail = nodes[currentNodeIndex + i + horizontalNodeCount];

                    var link = new Link(head, tail);
                    links[currentLinkIndex] = link;

                    // Increment the currentLinkIndex
                    currentLinkIndex++;

                    verticalLinksAdded++;
                }

                currentNodeIndex = startingNodeIndex;
                currentLinkIndex = startingLinkIndex;

                for(var x = 0; x < horizontalCount; x++)
                {
                    // Compute the indices for the Nodes
                    int lowerNodePosition = currentNodeIndex + horizontalNodeCount;
                    
                    // Compute the indices for the Links
                    int verticalLinkIndex = currentLinkIndex + (2 * horizontalCount - 1);
                    int lowerLinkIndex = verticalLinkIndex + horizontalCount + 1;

                    // Get the Nodes for the Cell
                    Node topLeft = nodes[currentNodeIndex];
                    Node topRight = nodes[currentNodeIndex + 1];
                    Node left = nodes[currentNodeIndex + 2];
                    Node right = nodes[lowerNodePosition];
                    Node bottomLeft = nodes[lowerNodePosition + 1];
                    Node bottomRight = nodes[lowerNodePosition + 2];

                    // Get the Links for the Cell
                    Link topLeftLink = links[currentLinkIndex];
                    Link topRightLink = links[currentLinkIndex + 1];
                    Link leftLink = links[verticalLinkIndex];
                    Link rightLink = links[verticalLinkIndex + 1];
                    Link bottomLeftLink = links[lowerLinkIndex];
                    Link bottomRightLink = links[lowerLinkIndex + 1];

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
                    currentNodeIndex += 2;
                    currentLinkIndex += 2;
                }

                // Set the Node index for the next offset row
                currentNodeIndex += 2;

                // Set the Link index for the next offset row
                currentLinkIndex += verticalLinksAdded + 1;
            },
            // Subsequent offset row
            (int y) =>
            {
                // Get the starting indices that'll be used for creating the Cells
                int startingNodeIndex = currentNodeIndex;
                int startingLinkIndex = currentLinkIndex;

                int verticalLinkCount = horizontalCount;
                int topAngledLinksCount = 2 * (horizontalCount - 1);

                // Adjust the indices for the current offset row
                currentNodeIndex += horizontalNodeCount - 1;
                currentLinkIndex += topAngledLinksCount + horizontalCount + 1;

                // Create the offset that's a result from the offsetBit
                Vector2 offsetFromStartOffset = scale / 2;
                
                // Create the local position of the first Node in the row
                Vector2 position = globalNodeOffset + offsetFromStartOffset;
                
                // Create the first Node
                Node node = NodeHelper.Create(transform, position);
                nodes[currentNodeIndex] = node;
                currentNodeIndex++;
                
                // Add 1 because the above Node is already done
                for (var x = 2; x < horizontalNodeCount - 1; x++)
                {
                    var localPosition = new Vector2(
                        s_CellWidth * scale.X * x,
                        s_CellHeight * scale.Y * y + oscillationModifier
                    );

                    // Create the next Nodes
                    Vector2 followingPosition = globalNodeOffset + offsetFromStartOffset + localPosition;

                    Node followingNode = NodeHelper.Create(transform, followingPosition);
                    nodes[currentNodeIndex] = followingNode;

                    // Create the angled Link
                    var link = new Link(node, followingNode);
                    links[currentLinkIndex] = link;
                    
                    // Update and/or increment the variables
                    oscillationModifier *= -1;
                    currentNodeIndex++;
                    currentLinkIndex++;
                }

                currentNodeIndex = startingNodeIndex;
                currentLinkIndex = startingLinkIndex + topAngledLinksCount + 1;

                for(var i = 0; i < horizontalNodeCount - 1; i += 2)
                {
                    Node head = nodes[currentNodeIndex + i];
                    Node tail = nodes[currentNodeIndex + i + horizontalNodeCount - 1];

                    var link = new Link(head, tail);
                    links[currentLinkIndex] = link;

                    // Increment the currentLinkIndex
                    currentLinkIndex++;
                }

                currentNodeIndex = startingNodeIndex;
                currentLinkIndex = startingLinkIndex;

                for(var x = 0; x < horizontalCount - 1; x++)
                {
                    // Compute the indices for the Nodes
                    int lowerNodePosition = currentNodeIndex + horizontalNodeCount - 1;
                    
                    // Compute the indices for the Links
                    int verticalLinkIndex = currentLinkIndex + (2 * horizontalCount - 1);
                    int lowerLinkIndex = verticalLinkIndex + horizontalCount;

                    // Get the Nodes for the Cell
                    Node topLeft = nodes[currentNodeIndex];
                    Node topRight = nodes[currentNodeIndex + 1];
                    Node left = nodes[currentNodeIndex + 2];
                    Node right = nodes[lowerNodePosition];
                    Node bottomLeft = nodes[lowerNodePosition + 1];
                    Node bottomRight = nodes[lowerNodePosition + 2];

                    // Get the Links for the Cell
                    Link topLeftLink = links[currentLinkIndex];
                    Link topRightLink = links[currentLinkIndex + 1];
                    Link leftLink = links[verticalLinkIndex];
                    Link rightLink = links[verticalLinkIndex + 1];
                    Link bottomLeftLink = links[lowerLinkIndex];
                    Link bottomRightLink = links[lowerLinkIndex + 1];

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
                    currentNodeIndex += 2;
                    currentLinkIndex += 2;
                }

                // Set the Node index for the next normal row
                currentNodeIndex += 2;

                // Set the Link index for the next normal row
                currentLinkIndex += topAngledLinksCount + 1;
            }
        };

        // Set up the first row
        startingRowFuncs[offsetBit]();

        currentLinkIndex += horizontalCount + normalBit;
        offsetBit = (1 - offsetBit);
        normalBit = (1 - normalBit);

        // Setup the Nodes along with the angled Links
        for (var y = 1; y < verticalNodeCount; y += 1 + normalBit)
        {
            subsequentRowFuncs[offsetBit](y);

            // Toggle the offsetBit
            offsetBit = (1 - offsetBit);
            normalBit = (1 - normalBit);

            // Reset the oscillationModifier
            oscillationModifier = -0.125;
        }

        // Set up the indices
        offsetBit = Convert.ToInt32(generationData.StartOffset);
        currentNodeIndex = 0;
        currentLinkIndex = 2 * (horizontalCount - offsetBit);

        for (var i = 0; i < horizontalNodeCount - offsetBit; i += 2)
        {
            Node head = nodes[currentNodeIndex + i];
            Node tail = nodes[currentNodeIndex + i + horizontalNodeCount - 1];

            var link = new Link(head, tail);
            links[currentLinkIndex] = link;

            // Increment the currentLinkIndex
            currentLinkIndex++;
        }

        currentNodeIndex = 0;
        currentLinkIndex = 0;

        // Add in the rest of the first row of vertical Links and Cells
        for (var x = 0; x < horizontalCount - offsetBit; x++)
        {
            // Compute the indices for the Nodes
            int lowerNodePosition = currentNodeIndex + horizontalNodeCount - 1;

            // Compute the indices for the Links
            int verticalLinkIndex = 2 * (horizontalCount - offsetBit) + x;
            int lowerLinkIndex = verticalLinkIndex + horizontalCount + offsetBit;

            // Get the Nodes for the Cell
            Node topLeft = nodes[currentNodeIndex];
            Node topRight = nodes[currentNodeIndex + 1];
            Node left = nodes[currentNodeIndex + 2];
            Node right = nodes[lowerNodePosition];
            Node bottomLeft = nodes[lowerNodePosition + 1];
            Node bottomRight = nodes[lowerNodePosition + 2];

            // Get the Links for the Cell
            Link topLeftLink = links[currentLinkIndex];
            Link topRightLink = links[currentLinkIndex + 1];
            Link leftLink = links[verticalLinkIndex];
            Link rightLink = links[verticalLinkIndex + 1];
            Link bottomLeftLink = links[lowerLinkIndex];
            Link bottomRightLink = links[lowerLinkIndex + 1];

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

            cells[x] = cell;

            // Increment the counters
            currentNodeIndex += 2;
            currentLinkIndex += 2;
        }

        return new TemplateNetwork(nodes, links, cells);
    }
}