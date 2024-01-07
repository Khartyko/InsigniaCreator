﻿/** \addtogroup DomainTesting
* @{
*/

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.NetworkCalculators;
using Khartyko.InsigniaCreator.Domain.NetworkGenerators;
using Khartyko.InsigniaCreator.Domain.Testing.TestData;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.TestingLibrary;

#pragma warning disable CS8625

namespace Khartyko.InsigniaCreator.Domain.Testing.NetworkGenerators;

public class TriangularNetworkGeneratorTests
{
    private static TriangularNetworkData ValidData() => new()
    {
        Width = 1280,
        Height = 800,
        HorizontalCellCount = DataGenerator.GenerateRandomInt(1, 20),
        VerticalCellCount = DataGenerator.GenerateRandomInt(1, 20)
    };

    [Theory, InlineData(true), InlineData(false)]
    public void GenerateNetwork_1x1TriangleGrid_Succeeds(bool startFlipped)
    {
        var calculator = new TriangularNetworkCalculator();
        var generator = new TriangularNetworkGenerator(calculator);

        var data = new TriangularNetworkData
        {
            Width = 1280,
            Height = 800,
            VerticalCentering = true,
            HorizontalCentering = true,
            HorizontalCellCount = 1,
            VerticalCellCount = 1,
            StartFlipped = startFlipped
        };

        int expectedNodeCount = calculator.CalculateNodeCount(data);
        int expectedLinkCount = calculator.CalculateLinkCount(data);
        int expectedCellCount = calculator.CalculateCellCount(data);

        TemplateNetwork actualResult = generator.GenerateNetwork(data);

        Assert.Equal(expectedNodeCount, actualResult.Nodes.Count);
        Assert.Equal(expectedLinkCount, actualResult.Links.Count);
        Assert.Equal(expectedCellCount, actualResult.Cells.Count);
    }

    [Theory]
    [InlineData(false, false, false)]
    [InlineData(true, false, false)]
    [InlineData(false, true, false)]
    [InlineData(true, true, false)]
    [InlineData(false, false, true)]
    [InlineData(true, false, true)]
    [InlineData(true, true, true)]
    public void GenerateNetwork_AllSet_Succeeds(bool verticalCentering, bool horizontalCentering, bool startFlipped)
    {
        var calculator = new TriangularNetworkCalculator();
        var generator = new TriangularNetworkGenerator(calculator);

        TriangularNetworkData data = ValidData();
        data.VerticalCentering = verticalCentering;
        data.HorizontalCentering = horizontalCentering;
        data.StartFlipped = startFlipped;

        int expectedNodeCount = calculator.CalculateNodeCount(data);
        int expectedLinkCount = calculator.CalculateLinkCount(data);
        int expectedCellCount = calculator.CalculateCellCount(data);

        TemplateNetwork actualResult = generator.GenerateNetwork(data);

        Assert.Equal(expectedNodeCount, actualResult.Nodes.Count);
        Assert.Equal(expectedLinkCount, actualResult.Links.Count);
        Assert.Equal(expectedCellCount, actualResult.Cells.Count);
    }

    [Fact]
    public void GenerateNetwork_RequiredOnly_Succeeds()
    {
        var calculator = new TriangularNetworkCalculator();
        var generator = new TriangularNetworkGenerator(calculator);

        TriangularNetworkData data = ValidData();

        int expectedNodeCount = calculator.CalculateNodeCount(data);
        int expectedLinkCount = calculator.CalculateLinkCount(data);
        int expectedCellCount = calculator.CalculateCellCount(data);

        TemplateNetwork actualResult = generator.GenerateNetwork(data);

        Assert.Equal(expectedNodeCount, actualResult.Nodes.Count);
        Assert.Equal(expectedLinkCount, actualResult.Links.Count);
        Assert.Equal(expectedCellCount, actualResult.Cells.Count);
    }

    [Theory, ClassData(typeof(TriangularNetworkTestData.OptionalIncluded))]
    public void GenerateNetwork_PatchWorkNetworkData_Succeeds(TriangularNetworkData networkData)
    {
        var calculator = new TriangularNetworkCalculator();
        var generator = new TriangularNetworkGenerator(calculator);

        int expectedNodeCount = calculator.CalculateNodeCount(networkData);
        int expectedLinkCount = calculator.CalculateLinkCount(networkData);
        int expectedCellCount = calculator.CalculateCellCount(networkData);

        TemplateNetwork actualResult = generator.GenerateNetwork(networkData);

        Assert.Equal(expectedNodeCount, actualResult.Nodes.Count);
        Assert.Equal(expectedLinkCount, actualResult.Links.Count);
        Assert.Equal(expectedCellCount, actualResult.Cells.Count);
    }

    [Fact]
    public void GenerateNetwork_NullData_Fails()
    {
        var calculator = new TriangularNetworkCalculator();
        var generator = new TriangularNetworkGenerator(calculator);

        Assert.Throws<ArgumentNullException>(() => generator.GenerateNetwork(null));
    }

    [Theory, InlineData(0), InlineData(-1)]
    public void GenerateNetwork_WidthOutOfRange_Fails(double invalidWidth)
    {
        var calculator = new TriangularNetworkCalculator();
        var generator = new TriangularNetworkGenerator(calculator);

        TriangularNetworkData data = ValidData();
        data.Width = invalidWidth;

        Assert.Throws<ArgumentOutOfRangeException>(() => generator.GenerateNetwork(data));
    }

    [Theory, ClassData(typeof(InvalidDoubleData))]
    public void GenerateNetwork_NonrealWidth_Fails(double invalidWidth)
    {
        var calculator = new TriangularNetworkCalculator();
        var generator = new TriangularNetworkGenerator(calculator);

        TriangularNetworkData data = ValidData();
        data.Width = invalidWidth;

        Assert.Throws<ArgumentException>(() => generator.GenerateNetwork(data));
    }

    [Theory, InlineData(0), InlineData(-1)]
    public void GenerateNetwork_HeightOutOfRange_Fails(double invalidHeight)
    {
        var calculator = new TriangularNetworkCalculator();
        var generator = new TriangularNetworkGenerator(calculator);

        TriangularNetworkData data = ValidData();
        data.Width = invalidHeight;

        Assert.Throws<ArgumentOutOfRangeException>(() => generator.GenerateNetwork(data));
    }

    [Theory, ClassData(typeof(InvalidDoubleData))]
    public void GenerateNetwork_NonrealHeight_Fails(double invalidHeight)
    {
        var calculator = new TriangularNetworkCalculator();
        var generator = new TriangularNetworkGenerator(calculator);

        TriangularNetworkData data = ValidData();
        data.Width = invalidHeight;

        Assert.Throws<ArgumentException>(() => generator.GenerateNetwork(data));
    }

    [Theory, InlineData(0), InlineData(-1)]
    public void GenerateNetwork_HorizontalCellCountOutOfRange_Fails(int invalidHorizontalCount)
    {
        var calculator = new TriangularNetworkCalculator();
        var generator = new TriangularNetworkGenerator(calculator);

        TriangularNetworkData data = ValidData();
        data.HorizontalCellCount = invalidHorizontalCount;

        Assert.Throws<ArgumentOutOfRangeException>(() => generator.GenerateNetwork(data));
    }

    [Theory, InlineData(0), InlineData(-1)]
    public void GenerateNetwork_VerticalCellCountOutOfRange_Fails(int invalidVerticalCount)
    {
        var calculator = new TriangularNetworkCalculator();
        var generator = new TriangularNetworkGenerator(calculator);

        TriangularNetworkData data = ValidData();
        data.VerticalCellCount = invalidVerticalCount;

        Assert.Throws<ArgumentOutOfRangeException>(() => generator.GenerateNetwork(data));
    }

    [Fact]
    public void GenerateNetwork_NullCellTransform_Fails()
    {
        var calculator = new TriangularNetworkCalculator();
        var generator = new TriangularNetworkGenerator(calculator);

        TriangularNetworkData data = ValidData();
        data.CellTransform = null;

        Assert.Throws<ArgumentNullException>(() => generator.GenerateNetwork(data));
    }
}

/** @} */