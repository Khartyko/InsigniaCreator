/** \addtogroup LibraryTests
 * @{
 */
using Khartyko.InsigniaCreator.Library.Data;

namespace Khartyko.InsigniaCreator.Library.Testing.Utility.Model;

public class RandomMatrixData
{
    public RandomMatrixData(Vector3 m0, Vector3 m1, Vector3 m2)
    {
        M0 = m0 ?? throw new ArgumentNullException(nameof(m0));
        M1 = m1 ?? throw new ArgumentNullException(nameof(m1));
        M2 = m2 ?? throw new ArgumentNullException(nameof(m2));

        Matrix = new Matrix(M0, M1, M2);
    }

    public Vector3 M0 { get; set; }
    public Vector3 M1 { get; set; }
    public Vector3 M2 { get; set; }
    public Matrix Matrix { get; set; }
}
/** @} */