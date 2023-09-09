using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.TestingLibrary;

namespace Khartyko.InsigniaCreator.Domain.Testing.Data;

public class NetworkDataTests
{
	#region Width

	[Fact]
	public void Width_ValidValue_Succeeds()
	{
		double value = DataGenerator.GenerateRandomDouble();

		var data = new NetworkData
		{
			Width = value
		};
		
		Assert.Equal(value, data.Width);
	}

	[Fact]
	public void Width_Null_Succeeds()
	{
		var data = new NetworkData();
		
		Assert.Null(data.Width);
	}

	[Fact]
	public void Width_NullToValidValue_Succeeds()
	{
		var data = new NetworkData();
		
		Assert.Null(data.Width);
		
		double value = DataGenerator.GenerateRandomDouble();

		data.Width = value;
		
		Assert.Equal(value, data.Width);
	}

	[Fact]
	public void Width_ValidValueToNull_Succeeds()
	{
		double value = DataGenerator.GenerateRandomDouble();

		var data = new NetworkData
		{
			Width = value
		};
		
		Assert.Equal(value, data.Width);

		data.Width = null;
		
		Assert.Null(data.Width);
	}

	#endregion Width
	
	#region Height
	
	[Fact]
	public void Height_ValidValue_Succeeds()
	{
		double value = DataGenerator.GenerateRandomDouble();

		var data = new NetworkData
		{
			Height = value
		};
		
		Assert.Equal(value, data.Height);
	}

	[Fact]
	public void Height_Null_Succeeds()
	{
		var data = new NetworkData();
		
		Assert.Null(data.Height);
	}

	[Fact]
	public void Height_NullToValidValue_Succeeds()
	{
		var data = new NetworkData();
		
		Assert.Null(data.Height);
		
		double value = DataGenerator.GenerateRandomDouble();

		data.Height = value;
		
		Assert.Equal(value, data.Height);
	}

	[Fact]
	public void Height_ValidValueToNull_Succeeds()
	{
		double value = DataGenerator.GenerateRandomDouble();

		var data = new NetworkData
		{
			Height = value
		};
		
		Assert.Equal(value, data.Height);

		data.Height = null;
		
		Assert.Null(data.Height);
	}

	#endregion Height
	
	#region CenterAlongXAxis
	
	[Fact]
	public void CenterAlongXAxis_ValidValue_Succeeds()
	{
		bool value = DataGenerator.GenerateRandomBool();

		var data = new NetworkData
		{
			CenterAlongXAxis = value
		};
		
		Assert.Equal(value, data.CenterAlongXAxis);
	}

	[Fact]
	public void CenterAlongXAxis_Null_Succeeds()
	{
		var data = new NetworkData();
		
		Assert.Null(data.CenterAlongXAxis);
	}

	[Fact]
	public void CenterAlongXAxis_NullToValidValue_Succeeds()
	{
		var data = new NetworkData();
		
		Assert.Null(data.CenterAlongXAxis);
		
		bool value = DataGenerator.GenerateRandomBool();

		data.CenterAlongXAxis = value;
		
		Assert.Equal(value, data.CenterAlongXAxis);
	}

	[Fact]
	public void CenterAlongXAxis_ValidValueToNull_Succeeds()
	{
		bool value = DataGenerator.GenerateRandomBool();

		var data = new NetworkData
		{
			CenterAlongXAxis = value
		};
		
		Assert.Equal(value, data.CenterAlongXAxis);

		data.CenterAlongXAxis = null;
		
		Assert.Null(data.CenterAlongXAxis);
	}

	#endregion CenterAlongXAxis
	
	#region CenterAlongYAxis
	
	[Fact]
	public void CenterAlongYAxis_ValidValue_Succeeds()
	{
		bool value = DataGenerator.GenerateRandomBool();

		var data = new NetworkData
		{
			CenterAlongYAxis = value
		};
		
		Assert.Equal(value, data.CenterAlongYAxis);
	}

	[Fact]
	public void CenterAlongYAxis_Null_Succeeds()
	{
		var data = new NetworkData();
		
		Assert.Null(data.CenterAlongYAxis);
	}

	[Fact]
	public void CenterAlongYAxis_NullToValidValue_Succeeds()
	{
		var data = new NetworkData();
		
		Assert.Null(data.CenterAlongYAxis);
		
		bool value = DataGenerator.GenerateRandomBool();

		data.CenterAlongYAxis = value;
		
		Assert.Equal(value, data.CenterAlongYAxis);
	}

	[Fact]
	public void CenterAlongYAxis_ValidValueToNull_Succeeds()
	{
		bool value = DataGenerator.GenerateRandomBool();

		var data = new NetworkData
		{
			CenterAlongYAxis = value
		};
		
		Assert.Equal(value, data.CenterAlongYAxis);

		data.CenterAlongYAxis = null;
		
		Assert.Null(data.CenterAlongYAxis);
	}

	#endregion CenterAlongYAxis
	
	#region HorizontalCellCount
	
	[Fact]
	public void HorizontalCellCount_ValidValue_Succeeds()
	{
		int value = DataGenerator.GenerateRandomInt(0, 101);

		var data = new NetworkData
		{
			HorizontalCellCount = value
		};
		
		Assert.Equal(value, data.HorizontalCellCount);
	}

	[Fact]
	public void HorizontalCellCount_Null_Succeeds()
	{
		var data = new NetworkData();
		
		Assert.Null(data.HorizontalCellCount);
	}

	[Fact]
	public void HorizontalCellCount_NullToValidValue_Succeeds()
	{
		var data = new NetworkData();
		
		Assert.Null(data.HorizontalCellCount);
		
		int value = DataGenerator.GenerateRandomInt(0, 101);

		data.HorizontalCellCount = value;
		
		Assert.Equal(value, data.HorizontalCellCount);
	}

	[Fact]
	public void HorizontalCellCount_ValidValueToNull_Succeeds()
	{
		int value = DataGenerator.GenerateRandomInt(0, 101);

		var data = new NetworkData
		{
			HorizontalCellCount = value
		};
		
		Assert.Equal(value, data.HorizontalCellCount);

		data.HorizontalCellCount = null;
		
		Assert.Null(data.HorizontalCellCount);
	}

	#endregion HorizontalCellCount
	
	#region VerticalCellCount
	
	[Fact]
	public void VerticalCellCount_ValidValue_Succeeds()
	{
		int value = DataGenerator.GenerateRandomInt(0, 101);

		var data = new NetworkData
		{
			VerticalCellCount = value
		};
		
		Assert.Equal(value, data.VerticalCellCount);
	}

	[Fact]
	public void VerticalCellCount_Null_Succeeds()
	{
		var data = new NetworkData();
		
		Assert.Null(data.VerticalCellCount);
	}

	[Fact]
	public void VerticalCellCount_NullToValidValue_Succeeds()
	{
		var data = new NetworkData();
		
		Assert.Null(data.VerticalCellCount);
		
		int value = DataGenerator.GenerateRandomInt(0, 101);

		data.VerticalCellCount = value;
		
		Assert.Equal(value, data.VerticalCellCount);
	}

	[Fact]
	public void VerticalCellCount_ValidValueToNull_Succeeds()
	{
		int value = DataGenerator.GenerateRandomInt(0, 101);

		var data = new NetworkData
		{
			VerticalCellCount = value
		};
		
		Assert.Equal(value, data.VerticalCellCount);

		data.VerticalCellCount = null;
		
		Assert.Null(data.VerticalCellCount);
	}

	#endregion VerticalCellCount
	
	#region CellTransform
	
	[Fact]
	public void CellTransform_ValidValue_Succeeds()
	{
		RandomTransformData transformData = DataGenerator.GenerateRandomTransformData(true, true, true);

		var data = new NetworkData
		{
			CellTransform = transformData.Transform
		};
		
		Assert.Equal(transformData.Transform, data.CellTransform);
	}

	[Fact]
	public void CellTransform_Null_Succeeds()
	{
		var transform = new Transform();
		var data = new NetworkData();
		
		Assert.Null(data.CellTransform);
	}

	[Fact]
	public void CellTransform_NullToValidValue_Succeeds()
	{
		var data = new NetworkData();
		
		Assert.Null(data.CellTransform);
		
		RandomTransformData transformData = DataGenerator.GenerateRandomTransformData(true, true, true);

		data.CellTransform = transformData.Transform;
		
		Assert.Equal(transformData.Transform, data.CellTransform);
	}

	[Fact]
	public void CellTransform_ValidValueToNull_Succeeds()
	{
		RandomTransformData transformData = DataGenerator.GenerateRandomTransformData(true, true, true);

		var data = new NetworkData
		{
			CellTransform = transformData.Transform
		};
		
		Assert.Equal(transformData.Transform, data.CellTransform);

		data.CellTransform = null;
		
		Assert.Null(data.CellTransform);
	}

	#endregion CellTransform
}