using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.TestingLibrary;

namespace Khartyko.InsigniaCreator.Domain.Testing.TestData;

public class NetworkTestData
{
    public class OptionalIncluded : TestDataItem
    {
        public override IEnumerable<object[]> GetData()
        {
            // 100
            yield return new object[]
            {
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CenterAlongXAxis = false,
                },
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CenterAlongXAxis = true,
                }
            };

            // 010
            yield return new object[]
            {
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CenterAlongYAxis = false,
                },
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CenterAlongYAxis = true,
                }
            };

            // 110
            yield return new object[]
            {
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CenterAlongXAxis = false,
                    CenterAlongYAxis = false,
                },
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CenterAlongXAxis = true,
                    CenterAlongYAxis = false,
                },

                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CenterAlongXAxis = false,
                    CenterAlongYAxis = true,
                },
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CenterAlongXAxis = true,
                    CenterAlongYAxis = true,
                }
            };

            // 001
            yield return new object[]
            {
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                }
            };

            // 101
            yield return new object[]
            {
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CenterAlongXAxis = false,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                },
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CenterAlongXAxis = true,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                }
            };

            // 011
            yield return new object[]
            {
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CenterAlongYAxis = false,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                },
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CenterAlongYAxis = true,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                }
            };
        }
    }
}