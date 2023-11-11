﻿/** \addtogroup Khartyko.InsigniaCreator.Domain
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
    /// <exception cref="NullReferenceException">Can be thrown if 'data' is null.</exception>
    /// <returns>A newly generated Cartograph.</returns>
    public Cartograph Generate(CartographData data)
    {
        AssertionHelper.NullCheck(data, nameof(data));
        AssertionHelper.EmptyOrWhitespaceCheck(data.Name, "data::Name");
        AssertionHelper.NullCheck(data.Network, "data::Network");
        
        return new Cartograph(_currentId++, data.AtlasId, data.Name, data.Network);
    }
}

/** @} */