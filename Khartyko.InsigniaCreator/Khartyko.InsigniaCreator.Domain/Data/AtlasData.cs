/** \addtogroup Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Domain.Data;

/// <summary>
/// Stores possible Atlas data that will be used.
/// It can be used for creating or updating.
/// </summary>
public class AtlasData : IEntityData
{
	/// <summary>
	/// The name of an Atlas to be used.
	/// </summary>
	public string Name { get; set; }
	
	/// <summary>
	/// The width of an Atlas to be used.
	/// </summary>
	public double Width { get; set; }
	
	/// <summary>
	/// The height of an Atlas to be used.
	/// </summary>
	public double Height { get; set; }
	
	/// <summary>
	/// The background color of an Atlas to be used.
	/// </summary>
	public RgbColor Background { get; set; }
	
	/// <summary>
	/// A Cartograph associated with an Atlas to be used.
	/// </summary>
	/// Either this or 'Cartographs' can be set; if both are set, then they will be joined.
	/// Any overlap will be ignored.
	/// </remarks>
	public Cartograph Cartograph { get; set; }
	
	/// <summary>
	/// Multiple Cartographs associated with an Atlas to be used.
	/// </summary>
	/// <remarks>
	/// Either this or 'Cartograph' can be set; if both are set, then they will be joined.
	/// Any overlap will be ignored.
	/// </remarks>
	public IList<Cartograph> Cartographs { get; set; }
}

/** @} */