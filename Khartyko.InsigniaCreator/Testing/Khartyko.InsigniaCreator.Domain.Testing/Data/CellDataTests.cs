/** \addtogroup DomainTesting
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.TestingLibrary;

namespace Khartyko.InsigniaCreator.Domain.Testing.Data;

public class CellDataTests
{
	private NodeData[] GenerateNodeData(int count)
	{
		var nodeDatas = new List<NodeData>(count);

		for (var i = 0; i < count; i++)
		{
			var data = new NodeData
			{
				Position = DataGenerator.GenerateRandomVector2()
			};
			
			nodeDatas.Add(data);
		}

		return nodeDatas.ToArray();
	}

	private LinkData[] GenerateLinkData(NodeData[] nodeDatas)
	{
		List<NodeData> data = nodeDatas.ToList();

		var linkDatas = new List<LinkData>(nodeDatas.Length);

		if (!nodeDatas.Any())
		{
			return linkDatas.ToArray();
		}
		
		data.Add(nodeDatas.First());

		for (var i = 0; i < nodeDatas.Length; i++)
		{
			NodeData headData = data[i];
			NodeData tailData = data[i + 1];

			var linkData = new LinkData
			{
				HeadData = headData,
				TailData = tailData
			};
			
			linkDatas.Add(linkData);
		}

		return linkDatas.ToArray();
	}
	
	#region NodeDatas

	[Fact]
	public void NodeDatas_Get_ValidValues_Succeeds()
	{
		int count = DataGenerator.GenerateRandomInt(0, 10);
		NodeData[] datas = GenerateNodeData(count);

		var cellData = new CellData
		{
			NodeDatas = datas
		};
		
		Assert.Equal(datas, cellData.NodeDatas);
	}

	[Fact]
	public void NodeDatas_Get_Null_Succeeds()
	{
		var cellData = new CellData();
		
		Assert.Null(cellData.NodeDatas);
	}

	[Fact]
	public void NodeDatas_Set_ValidValues_Succeeds()
	{
		var cellData = new CellData();
		
		Assert.Null(cellData.NodeDatas);
		
		int count = DataGenerator.GenerateRandomInt(0, 10);
		NodeData[] datas = GenerateNodeData(count);

		cellData.NodeDatas = datas;
		
		Assert.NotNull(cellData.NodeDatas);
		Assert.Equal(datas, cellData.NodeDatas);
	}

	[Fact]
	public void NodeDatas_Set_Null_Succeeds()
	{
		int count = DataGenerator.GenerateRandomInt(0, 10);
		NodeData[] datas = GenerateNodeData(count);

		var cellData = new CellData
		{
			NodeDatas = datas
		};
		
		Assert.Equal(datas, cellData.NodeDatas);

		cellData.NodeDatas = null;
		
		Assert.Null(cellData.NodeDatas);
	}

	#endregion NodeDatas

	#region TailDatas

	[Fact]
	public void LinkDatas_Get_ValidValues_Succeeds()
	{
		int count = DataGenerator.GenerateRandomInt(0, 10);
		NodeData[] nodeDatas = GenerateNodeData(count);
		LinkData[] linkDatas = GenerateLinkData(nodeDatas);

		var cellData = new CellData
		{
			NodeDatas = nodeDatas,
			LinkDatas = linkDatas
		};
		
		Assert.NotNull(cellData.LinkDatas);
		Assert.Equal(linkDatas.ToArray(), cellData.LinkDatas);
	}

	[Fact]
	public void LinkDatas_Get_Null_Succeeds()
	{
		var cellData = new CellData();
		
		Assert.Null(cellData.LinkDatas);
	}

	[Fact]
	public void LinkDatas_Set_ValidValues_Succeeds()
	{
		var cellData = new CellData();
		
		Assert.Null(cellData.LinkDatas);
		
		int count = DataGenerator.GenerateRandomInt(0, 10);
		NodeData[] nodeDatas = GenerateNodeData(count);
		LinkData[] linkDatas = GenerateLinkData(nodeDatas);

		cellData.LinkDatas = linkDatas;
		
		Assert.NotNull(cellData.LinkDatas);
		Assert.Equal(linkDatas, cellData.LinkDatas);
	}

	[Fact]
	public void LinkDatas_Set_Null_Succeeds()
	{
		int count = DataGenerator.GenerateRandomInt(0, 10);
		NodeData[] nodeDatas = GenerateNodeData(count);
		LinkData[] linkDatas = GenerateLinkData(nodeDatas);

		var cellData = new CellData
		{
			NodeDatas = nodeDatas,
			LinkDatas = linkDatas
		};

		cellData.LinkDatas = null;
		
		Assert.Null(cellData.LinkDatas);
	}

	#endregion TailDatas
}

/** @} */