/** \addtogroup Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Domain.Interfaces;

/// <summary>
/// Declares methods for manages Atlases.
/// </summary>
public interface IAtlasService : ICallbackService<Atlas>
{
    /// <summary>
    /// The currently selected Atlas.
    /// </summary>
    Atlas? SelectedAtlas { get; }

    /// <summary>
    /// Deselects the currently selected Atlas by setting it to null.
    /// </summary>
    /// <returns>Other than a Task, nothing.</returns>
    Task DeselectAtlasAsync();

    /// <summary>
    /// Selects an Atlas if the id matches. If the Atlas isn't found, nothing happens.
    /// </summary>
    /// <param name="id">The id of the Atlas.</param>
    /// <returns>True if the Atlas was found and selected; otherwise false.</returns>
    Task<bool> SelectAtlasAsync(ulong id);
    
    /// <summary>
    /// Creates an atlas with the given data and returns the newly created Atlas.
    /// </summary>
    /// <param name="atlasData">The data to use when creating the Atlas</param>
    /// <exception cref="ArgumentNullException">Can be thrown if 'atlasData' or any required data is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if the 'width' or 'height' is below 0.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'width' or 'height' is NaN, PositiveInfinity, or NegativeInfinity.</exception>
    /// <returns>The Atlas that was created</returns>
    Task<Atlas> CreateAtlasAsync(AtlasData atlasData);
    
    /// <summary>
    /// Gets all of the Atlas instances.
    /// </summary>
    /// <returns>All of the present Atlases.</returns>
    Task<IEnumerable<Atlas>> GetAllAtlasesAsync();
    
    /// <summary>
    /// Gets an Atlas by its id.
    /// </summary>
    /// <param name="id">The id of the Atlas.</param>
    /// <returns>The Atlas is found; otherwise null.</returns>
    Task<Atlas?> GetAtlasAsync(ulong id);
    
    /// <summary>
    /// Removes a Cartograph from an Atlas.
    /// </summary>
    /// <param name="id">The id of the Atlas.</param>
    /// <param name="cartographId">The id of the Cartograph</param>
    /// <returns>True if the Cartograph was removed from the Atlas; otherwise false.</returns>
    Task<bool> RemoveCartographFromAtlasAsync(ulong id, ulong cartographId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">The id of the Atlas.</param>
    /// <returns>True if the Atlas was found and deleted; false otherwise</returns>
    Task<bool> DeleteAtlasAsync(ulong id);
}

/** @} */