using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.NetworkCalculators;
using Khartyko.InsigniaCreator.Domain.NetworkGenerators;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Sandbox;

internal static class Program
{
    private static HexagonalNetworkData GenerateData() => new()
    {
        Width = 1280,
        Height = 720,
        HorizontalCellCount = 4,
        VerticalCellCount = 3,
        CenterAlongXAxis = true,
        CenterAlongYAxis = false,
        CellTransform = new Transform(
            new Vector2(10.0),
            0,
            Vector2.Zero
        ),
        StartOffset = false,
    };

    static void Main(string[] _)
    {
        var calculator = new HexagonalNetworkCalculator();
        var generator = new HexagonalNetworkGenerator(calculator);
        var data = GenerateData();

        TemplateNetwork network = generator.GenerateNetwork(data);
    }
}

