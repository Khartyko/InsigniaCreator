/** \addtogroup LibraryTests
 * @{
 */
using Khartyko.InsigniaCreator.Library.Data;

namespace Khartyko.InsigniaCreator.Library.Testing.Utility.Model;

public struct RandomTransformData
{
    public Vector2 Scale { get; set; }
    public double Rotation { get; set; }
    public Vector2 Translation { get; set; }
    public Transform Transform { get; set; }
}
/** @} */