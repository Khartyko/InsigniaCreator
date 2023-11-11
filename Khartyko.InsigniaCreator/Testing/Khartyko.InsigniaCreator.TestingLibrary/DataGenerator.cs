using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.TestingLibrary;

public static class DataGenerator
{
    private static readonly Random s_random = new();

    public static bool GenerateRandomBool() => GenerateRandomInt(0, 2) == 1;
    public static int GenerateRandomInt(int min, int max) => s_random.Next(min, max);
    public static ulong GenerateRandomULong(long max) => (ulong)s_random.NextInt64(0, max);
    public static double GenerateRandomDouble(double multiplier = 100) => s_random.NextDouble() * multiplier;

    public static Vector2 GenerateRandomVector2() => new(
        GenerateRandomDouble(),
        GenerateRandomDouble()
    );

    public static Vector3 GenerateRandomVector3() => new(
        GenerateRandomDouble(),
        GenerateRandomDouble(),
        GenerateRandomDouble()
    );

    public static RgbColor GenerateRandomColor() => new(
        (byte)s_random.Next(0, 256),
        (byte)s_random.Next(0, 256),
        (byte)s_random.Next(0, 256),
        (byte)s_random.Next(0, 256)
    );
    
    public static Node GenerateRandomNode() => new(
        GenerateRandomVector2()
    );

    public static Matrix GenerateRandomMatrix() => new Matrix(
        GenerateRandomVector3(),
        GenerateRandomVector3(),
        GenerateRandomVector3()
    );

    public static RandomTransformData GenerateRandomTransformData(bool randomScale, bool randomRotation,
        bool randomTranslation)
    {
        Vector2 scale = randomScale ? GenerateRandomVector2() : new Vector2(1);
        double rotation = randomRotation ? GenerateRandomDouble() : 0.0;
        Vector2 translation = randomTranslation ? GenerateRandomVector2() : new Vector2(0);

        return new RandomTransformData
        {
            Scale = scale,
            Rotation = rotation,
            Translation = translation,
            Transform = new Transform(scale, rotation, translation)
        };
    }
    
    
    public static TemplateNetwork GenerateSquareNetwork()
    {
        var nodes = new List<Node>
        {
            new(1, 1),
            new(1, -1),
            new(-1, -1),
            new(-1, 1)
        };

        var links = new List<Link>
        {
            new(nodes[0], nodes[1]),
            new(nodes[1], nodes[2]),
            new(nodes[2], nodes[3]),
            new(nodes[3], nodes[0])
        };

        var cells = new List<Cell>
        {
            new(nodes, links)
        };

        return new TemplateNetwork(nodes, links, cells);
    }
}