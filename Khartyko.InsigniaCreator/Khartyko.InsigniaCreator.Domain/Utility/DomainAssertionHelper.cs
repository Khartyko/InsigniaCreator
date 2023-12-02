/** \addtogroup MainApp
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Domain.Utility
{
    public static class DomainAssertionHelper
    {
        public static void CalculatorDataCheck<TNetworkData>(TNetworkData networkData, string? name = null)
            where TNetworkData : NetworkData
        {
            string networkDataName = name ?? nameof(networkData);

            AssertionHelper.NullCheck(networkData, nameof(networkData));
            AssertionHelper.MinimumCheck(networkData.HorizontalCellCount, 1, networkDataName);
            AssertionHelper.MinimumCheck(networkData.VerticalCellCount, 1, networkDataName);
        }
        
        public static void NetworkDataCheck<TNetworkData>(TNetworkData networkData, string? name = null)
            where TNetworkData : NetworkData
        {
            string networkDataName = name ?? nameof(networkData);

            AssertionHelper.NullCheck(networkData, nameof(networkData));
            AssertionHelper.RangeCheck(networkData.Width, 0, double.MaxValue,  networkDataName);
            AssertionHelper.RangeCheck(networkData.Height, 0, double.MaxValue, networkDataName);
            AssertionHelper.MinimumCheck(networkData.HorizontalCellCount, 1, networkDataName);
            AssertionHelper.MinimumCheck(networkData.VerticalCellCount, 1, networkDataName);
        }
    }
}

/** @} */