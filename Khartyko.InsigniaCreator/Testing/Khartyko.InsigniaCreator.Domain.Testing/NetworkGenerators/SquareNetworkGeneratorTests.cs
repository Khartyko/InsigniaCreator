/** \addtogroup DomainTesting
 * @{
 */

#pragma warning disable CS8600, CS8604

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.NetworkCalculators;
using Khartyko.InsigniaCreator.Domain.NetworkGenerators;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Domain.Testing.NetworkGenerators;

public class SquareNetworkGeneratorTests
{
	[Theory, ClassData(typeof(NetworkTestData))]
	public void GenerateNetwork_Succeeds(NetworkData data, int nodeCount, int linkCount, int cellCount)
	{
		var calculator = new SquareNetworkCalculator();
        var generator = new SquareNetworkGenerator(calculator);

		TemplateNetwork network = generator.GenerateNetwork(data);
		
		Assert.Equal(nodeCount, network.Nodes.Count);
		Assert.Equal(linkCount, network.Links.Count);
		Assert.Equal(cellCount, network.Cells.Count);
	}

	[Fact]
	public void GenerateNetwork_NullData_Fails()
	{
		NetworkData nullData = null;
        var calculator = new SquareNetworkCalculator();
        var generator = new SquareNetworkGenerator(calculator);

		Assert.Throws<ArgumentNullException>(() => generator.GenerateNetwork(nullData));
	}
}

/** @} */