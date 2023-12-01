using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.TestingLibrary;
namespace Khartyko.InsigniaCreator.Domain.Testing;

public class NetworkTestData : TestDataItem
{
	public override IEnumerable<object[]> GetData()
	{
		yield return new object[]
		{
			new NetworkData
			{
				Width = 100.0,
				Height = 100.0,
				CenterAlongXAxis = true,
				CenterAlongYAxis = true,
				HorizontalCellCount = 10,
				VerticalCellCount = 10,
				CellTransform = new Transform()
			},
			121,
			220,
			100
		};
		yield return new object[]
		{
			new NetworkData
			{
				Width = 100.0,
				Height = 100.0,
				CenterAlongXAxis = false,
				CenterAlongYAxis = true,
				HorizontalCellCount = 10,
				VerticalCellCount = 10,
				CellTransform = new Transform()
			},
			121,
			220,
			90
		};
		yield return new object[]
		{
			new NetworkData
			{
				Width = 100.0,
				Height = 100.0,
				CenterAlongXAxis = true,
				CenterAlongYAxis = false,
				HorizontalCellCount = 10,
				VerticalCellCount = 10,
				CellTransform = new Transform()
			},
			121,
			220,
			90
		};
		yield return new object[]
		{
			new NetworkData
			{
				Width = 100.0,
				Height = 100.0,
				CenterAlongXAxis = false,
				CenterAlongYAxis = false,
				HorizontalCellCount = 10,
				VerticalCellCount = 10,
				CellTransform = new Transform()
			},
			100,
			220,
			81
		};
	}
}