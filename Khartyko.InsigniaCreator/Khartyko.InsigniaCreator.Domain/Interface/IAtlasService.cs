/** \addtogroup Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Domain.Interface;

public interface IAtlasService : ICallbackService<Atlas>
{
    Atlas? SelectedAtlas { get; }

    Task DeselectAtlasAsync();
    Task<bool> SelectAtlasAsync(long id);
    Task<long> CreateAtlasAsync(AtlasData atlasData);
    Task<IEnumerable<Atlas>> GetAllAtlasesAsync();
    Task<Atlas?> GetAtlasAsync(long id);
    Task<bool> RemoveCartographFromAtlasAsync(long id, long cartographId);
    Task<bool> DeleteAtlasAsync(long id);
}

/** @} */