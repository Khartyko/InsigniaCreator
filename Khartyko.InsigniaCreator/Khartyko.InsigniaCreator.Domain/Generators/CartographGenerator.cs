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
    private ulong _currentId = 0L;
    
    /// <summary>
    /// Generates a Cartograph with the given data, and supplies an id.
    /// </summary>
    /// <param name="data">The data to use to generate a Cartograph.</param>
    /// <returns>A newly generated Cartograph.</returns>
    public Cartograph Generate(CartographData data)
    {
        AssertionHelper.NullCheck(data, nameof(data));
        
        return new Cartograph(_currentId++, data.AtlasId, data.Name, data.Network);
    }
}

/** @} */