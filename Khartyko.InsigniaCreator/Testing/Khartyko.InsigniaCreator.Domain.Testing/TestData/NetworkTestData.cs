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
                    VerticalCentering = false,
                }
            };
            
            yield return new object[]
            {
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
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
                    HorizontalCentering = false,
                }
            };
            
            yield return new object[]
            {
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    HorizontalCentering = true,
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
                    VerticalCentering = false,
                    HorizontalCentering = false,
                }
            };
            
            yield return new object[]
            {
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
                    HorizontalCentering = false,
                }
            };
            
            yield return new object[]
            {
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = false,
                    HorizontalCentering = true,
                }
            };
            
            yield return new object[]
            {
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
                    HorizontalCentering = true,
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
                    VerticalCentering = false,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                }
            };
            
            yield return new object[]
            {
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
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
                    HorizontalCentering = false,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                }
            };
            
            yield return new object[]
            {
                new NetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    HorizontalCentering = true,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                }
            };
        }
    }
}