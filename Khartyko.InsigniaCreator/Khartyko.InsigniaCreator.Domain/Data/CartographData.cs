/** \addtogroup Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Domain.Data;

/// <summary>
/// Stores possible Cartograph data that will be used.
/// It can be used for creating or updating.
/// </summary>
public class CartographData : IEntityData
{
	/// <summary>
	/// The id of the Atlas linked to a Cartograph.
	/// </summary>
	public ulong AtlasId { get; set; }
	
	/// <summary>
	/// The name of a Cartograph.
	/// </summary>
	public string Name { get; set; }
	
	/// <summary>
	/// The TemplateNetwork for a Cartograph.
	/// </summary>
	public TemplateNetwork Network { get; set; }
}

/** @} */