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

	private readonly IGenerator<CartographData, Cartograph> _cartographGenerator;

	public AtlasGenerator(IGenerator<CartographData, Cartograph> cartographGenerator)
	{
		_cartographGenerator = cartographGenerator;
	}
    
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

	    var cartographs = new List<Cartograph>();

	    if (data.CartographDatas is not null && data.CartographData is not null)
	    {
		    var cartographDatas = new List<CartographData>(data.CartographDatas);

		    List<CartographData> ids = data.CartographDatas.ToList();

		    if (!ids.Contains(data.CartographData))
		    {
			    cartographDatas.Add(data.CartographData);
		    }
		    
		    cartographDatas.ToList()
			    .ForEach(cartographData =>
			    {
				    Cartograph cartograph = _cartographGenerator.Generate(cartographData);
			    
				    cartographs.Add(cartograph);
			    });
	    }
	    
	    if (data.CartographDatas is not null && data.CartographData is null)
	    {
		    data.CartographDatas.ToList()
			    .ForEach(cartographData =>
			    {
				    Cartograph cartograph = _cartographGenerator.Generate(cartographData);
			    
				    cartographs.Add(cartograph);
			    });
	    }

	    if (data.CartographDatas is null && data.CartographData is not null)
	    {
		    Cartograph cartograph = _cartographGenerator.Generate(data.CartographData);

		    cartographs.Add(cartograph);
	    }
		    
	    return new Atlas(_currentId++, data.Name, data.Width, data.Height, data.Background, cartographs);
    }
}

/** @} */