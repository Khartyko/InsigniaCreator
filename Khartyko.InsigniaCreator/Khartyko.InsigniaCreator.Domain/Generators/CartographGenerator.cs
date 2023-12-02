/** \addtogroup Khartyko.InsigniaCreator.Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Domain.Generators;

/// <summary>
/// Implementation of an IGenerator for Cartographs and CartographDatas.
/// </summary>
public class CartographGenerator : IGenerator<CartographData, Cartograph>
{
    private ulong _currentId = 1L;

    private readonly Dictionary<Type, INetworkGenerator> _networkGenerators;

    private INetworkGenerator<TNetworkData> GetNetworkOfType<TNetworkData>(TNetworkData type)
	    where TNetworkData : NetworkData
    {
	    return (INetworkGenerator<TNetworkData>)_networkGenerators[type.GetType()];
    }
    
    public CartographGenerator(
	    INetworkGenerator<TriangularNetworkData> triangularNetworkGenerator,
	    INetworkGenerator<NetworkData> squareNetworkGenerator,
	    INetworkGenerator<HexagonalNetworkData> hexagonalNetworkGenerator
	)
    {
	    AssertionHelper.NullCheck(triangularNetworkGenerator, nameof(triangularNetworkGenerator));
	    AssertionHelper.NullCheck(squareNetworkGenerator, nameof(squareNetworkGenerator));
	    AssertionHelper.NullCheck(hexagonalNetworkGenerator, nameof(hexagonalNetworkGenerator));

	    _networkGenerators = new Dictionary<Type, INetworkGenerator>
	    {
		    { typeof(TriangularNetworkData), triangularNetworkGenerator },
		    { typeof(NetworkData), squareNetworkGenerator },
		    { typeof(HexagonalNetworkData), hexagonalNetworkGenerator }
	    };
    }
    
    /// <summary>
    /// Generates a Cartograph with the given data, and supplies an id.
    /// </summary>
    /// <param name="data">The data to use to generate a Cartograph.</param>
    /// <exception cref="NullReferenceException">Can be thrown if 'data' is null.</exception>
    /// <returns>A newly generated Cartograph.</returns>
    public Cartograph Generate(CartographData data)
    {
        AssertionHelper.NullCheck(data, nameof(data));
        AssertionHelper.MinimumCheck(data.AtlasId, 1uL, "data::AtlasId");
        AssertionHelper.EmptyOrWhitespaceCheck(data.Name, "data::Name");
        AssertionHelper.NullCheck(data.NetworkData, "data::NetworkData");

        TemplateNetwork network = GetNetworkOfType(data.NetworkData).GenerateNetwork(data.NetworkData);
        
        return new Cartograph(_currentId++, data.AtlasId, data.Name, network);
    }
}

/** @} */