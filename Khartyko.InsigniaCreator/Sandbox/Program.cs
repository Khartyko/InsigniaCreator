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
        Width = 1024,
        Height = 1024,
        HorizontalCellCount = 3,
        VerticalCellCount = 3,
        VerticalCentering = true,
        HorizontalCentering = true,
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

