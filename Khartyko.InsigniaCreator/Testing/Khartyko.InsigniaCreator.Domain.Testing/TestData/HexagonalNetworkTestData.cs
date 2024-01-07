﻿using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.TestingLibrary;

namespace Khartyko.InsigniaCreator.Domain.Testing.TestData;

public class HexagonalNetworkTestData
{
    public class OptionalIncluded : TestDataItem
    {
        public override IEnumerable<object[]> GetData()
        {
            /*
             * Other than the required data, will need a combination of the below:
             * Configuration    verticalCentering    horizontalCentering    CellTransform   StartOffset    Done?
             * 1000             Y                   N                   N               N               
             * 0100             N                   Y                   N               N               
             * 1100             Y                   Y                   N               N               
             * 0010             N                   N                   Y               N               
             * 1010             Y                   N                   Y               N               
             * 0110             N                   Y                   Y               N               
             * 1110             Y                   Y                   Y               N               
             * 0001             N                   N                   N               Y               
             * 1001             Y                   N                   N               Y               
             * 0101             N                   Y                   N               Y               
             * 1101             Y                   Y                   N               Y               
             * 0011             N                   N                   Y               Y               
             * 1011             Y                   N                   Y               Y               
             * 0111             N                   Y                   Y               Y               
             */

            // 1000
            yield return new object[]
            {
                new HexagonalNetworkData()
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
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
                }
            };

            // 0100
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    HorizontalCentering = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    HorizontalCentering = true
                }
            };

            // 1100
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = false,
                    HorizontalCentering = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
                    HorizontalCentering = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = false,
                    HorizontalCentering = true
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
                    HorizontalCentering = true
                }
            };

            // 0010
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                }
            };

            // 1010
            yield return new object[]
            {
                new HexagonalNetworkData()
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
                new HexagonalNetworkData()
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

            // 0110
            yield return new object[]
            {
                new HexagonalNetworkData()
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
                new HexagonalNetworkData()
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

            // 1110
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = false,
                    HorizontalCentering = false,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
                    HorizontalCentering = false,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = false,
                    HorizontalCentering = true,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
                    HorizontalCentering = true,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform
                }
            };

            // 0001
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    StartOffset = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    StartOffset = true
                }
            };

            // 1001
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = false,
                    StartOffset = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
                    StartOffset = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = false,
                    StartOffset = true
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
                    StartOffset = true
                },
            };

            // 0101
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    HorizontalCentering = false,
                    StartOffset = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    HorizontalCentering = true,
                    StartOffset = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    HorizontalCentering = false,
                    StartOffset = true
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    HorizontalCentering = true,
                    StartOffset = true
                }
            };

            // 1101
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = false,
                    HorizontalCentering = false,
                    StartOffset = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
                    HorizontalCentering = false,
                    StartOffset = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = false,
                    HorizontalCentering = true,
                    StartOffset = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
                    HorizontalCentering = true,
                    StartOffset = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = false,
                    HorizontalCentering = false,
                    StartOffset = true
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
                    HorizontalCentering = false,
                    StartOffset = true
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = false,
                    HorizontalCentering = true,
                    StartOffset = true
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
                    HorizontalCentering = true,
                    StartOffset = true
                }
            };

            // 0011
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                    StartOffset = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                    StartOffset = true
                }
            };

            // 1011
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = false,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                    StartOffset = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                    StartOffset = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = false,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                    StartOffset = true
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    VerticalCentering = true,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                    StartOffset = true
                }
            };

            // 0111
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    HorizontalCentering = false,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                    StartOffset = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    HorizontalCentering = true,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                    StartOffset = false
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    HorizontalCentering = false,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                    StartOffset = true
                }
            };
            
            yield return new object[]
            {
                new HexagonalNetworkData()
                {
                    Width = 1280,
                    Height = 800,
                    HorizontalCellCount = 13,
                    VerticalCellCount = 13,
                    // PatchWork Variables:
                    HorizontalCentering = true,
                    CellTransform = DataGenerator.GenerateRandomTransformData(true, true, true).Transform,
                    StartOffset = true
                }
            };
        }
    }
}