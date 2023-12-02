/** \addtogroup MainApp
 * @{
 */

using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Domain.Utility;

public static class CellCounterHelper
{
    public static int ConstrainCountByCentering(bool centerAlongAxis, int count)
    {
        AssertionHelper.MinimumCheck(count, 1, nameof(count));

		bool isEven = MathHelper.IsEven(count);

		int result = count;

		switch (centerAlongAxis)
		{
			case true when isEven:
				result--;
				break;

			case false when !isEven:
				result++;
                break;
		}
		
		return result;
    }
}

/** @} */