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
	public ulong AtlasId { get; set; }
	public string Name { get; set; }
	public TemplateNetwork Network { get; set; }
}

/** @} */