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
public class AtlasGenerator : IGenerator<AtlasData, Atlas>
{
	private ulong _currentId = 1L;
    
	/// <summary>
	/// Generates a Cartograph with the given data, and supplies an id.
	/// </summary>
	/// <param name="data">The data to use to generate a Cartograph.</param>
	/// <exception cref="NullReferenceException">Can be thrown if 'data' is null.</exception>
	/// <returns>A newly generated Cartograph.</returns>
    public Atlas Generate(AtlasData data)
    {
	    AssertionHelper.NullCheck(data, nameof(data));
	    AssertionHelper.EmptyOrWhitespaceCheck(data.Name, "data::Name");
	    /*
	     * TODO: Enforce validations:
	     * - Cannot be less than some value
	     * - Cannot be greater than some value
	     *
	     * Note: Above values are yet to be determined
	     */
	    AssertionHelper.PositiveCheck(data.Width, "data::Width");
	    AssertionHelper.PositiveCheck(data.Height, "data::Height");
	    AssertionHelper.NullCheck(data.Background, "data::Background");

	    if (data.Cartographs is not null && data.Cartograph is not null)
	    {
		    var cartographs = new List<Cartograph>(data.Cartographs);

		    List<ulong> ids = data.Cartographs.Select(cartograph => cartograph.Id).ToList();

		    if (!ids.Contains(data.Cartograph.Id))
		    {
			    cartographs.Add(data.Cartograph);
		    }
		    
		    return new Atlas(_currentId++, data.Name, data.Width, data.Height, data.Background, cartographs);
	    }
	    
	    if (data.Cartographs is not null && data.Cartograph is null)
	    {
		    return new Atlas(_currentId++, data.Name, data.Width, data.Height, data.Background, data.Cartographs);
	    }

	    if (data.Cartographs is null && data.Cartograph is not null)
	    {
		    return new Atlas(_currentId++, data.Name, data.Width, data.Height, data.Background, data.Cartograph); 
	    }
	    
	    return new Atlas(_currentId++, data.Name, data.Width, data.Height, data.Background);
    }
}

/** @} */