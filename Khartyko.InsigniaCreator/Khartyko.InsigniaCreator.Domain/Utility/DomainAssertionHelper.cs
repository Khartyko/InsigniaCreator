using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khartyko.InsigniaCreator.Domain.Utility
{
    public static class DomainAssertionHelper
    {
        public static void CalculatorDataCheck<TNetworkData>(TNetworkData networkData, string? name = null)
            where TNetworkData : NetworkData
        {
            string networkDataName = name ?? nameof(networkData);

            AssertionHelper.NullCheck(networkData, nameof(networkData));
            AssertionHelper.RangeCheck(networkData.HorizontalCellCount, 0, int.MaxValue, "networkData::HorizontalCellCount");
            AssertionHelper.RangeCheck(networkData.VerticalCellCount, 0, int.MaxValue, "networkData::VerticalCellCount");
        }
        public static void NetworkDataCheck<TNetworkData>(TNetworkData networkData, string? name = null)
            where TNetworkData : NetworkData
        {
            string networkDataName = name ?? nameof(networkData);

            AssertionHelper.NullCheck(networkData, nameof(networkData));
            AssertionHelper.RangeCheck(networkData.Width, 0, double.MaxValue, "networkData::Width");
            AssertionHelper.RangeCheck(networkData.Height, 0, double.MaxValue, "networkData::Height");
            AssertionHelper.RangeCheck(networkData.HorizontalCellCount, 0, int.MaxValue, "networkData::HorizontalCellCount");
            AssertionHelper.RangeCheck(networkData.VerticalCellCount, 0, int.MaxValue, "networkData::VerticalCellCount");
        }
    }
}