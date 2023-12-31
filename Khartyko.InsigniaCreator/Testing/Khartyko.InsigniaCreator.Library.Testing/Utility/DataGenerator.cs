/** \addtogroup LibraryTests
 * @{
 */
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Testing.Utility.Model;

namespace Khartyko.InsigniaCreator.Library.Testing.Utility;

internal static class DataGenerator
{
    private static readonly Random s_random = new();

    public static int GenerateRandomInt(int min, int max) => s_random.Next(min, max);
    public static long GenerateRandomLong(long min, long max) => s_random.NextInt64(min, max);
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

    public static Node GenerateRandomNode() => new(
        GenerateRandomVector2()
    );

    public static Matrix GenerateRandomMatrix() => new Matrix(
        GenerateRandomVector3(),
        GenerateRandomVector3(),
        GenerateRandomVector3()
    );
        
    public static RandomMatrixData GenerateRandomMatrixData()
    {
        return new RandomMatrixData(
            GenerateRandomVector3(),
            GenerateRandomVector3(),
            GenerateRandomVector3()
        );
    }

    public static RandomTransformData GenerateRandomTransformData(bool randomScale, bool randomRotation,
        bool randomTranslation)
    {
        Vector2? scale = randomScale ? GenerateRandomVector2() : new Vector2(1);
        double rotation = randomRotation ? GenerateRandomDouble() : 0.0;
        Vector2? translation = randomTranslation ? GenerateRandomVector2() : new Vector2(0);

        return new RandomTransformData
        {
            Scale = scale,
            Rotation = rotation,
            Translation = translation,
            Transform = new Transform(scale, rotation, translation)
        };
    }
}
/** @} */