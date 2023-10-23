/** \addtogroup Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Domain.Utility;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Domain.Services;

public class AtlasService : IAtlasService
{
    public Atlas? SelectedAtlas { get; }

    private readonly IRepository<AtlasData, Atlas> _atlasRepository;

    public AtlasService(IRepository<AtlasData, Atlas> atlasRepository)
    {
        AssertionHelper.NullCheck(atlasRepository, nameof(atlasRepository));

        _atlasRepository = atlasRepository;
    }

    #region Callback-Related Items

    public void RegisterCallback(Action<Atlas?> callback, CallbackType callbackType)
    {
        throw new NotImplementedException();
    }

    public void RegisterCallback(Action<Atlas?> callback, params CallbackType[] callbackTypes)
    {
        throw new NotImplementedException();
    }

    public void RemoveCallback(Action<Atlas?> callback, CallbackType callbackType)
    {
        throw new NotImplementedException();
    }

    #endregion Callback-Related Items

    #region IAtlasService Methods

    public async Task DeselectAtlasAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SelectAtlasAsync(ulong id)
    {
        throw new NotImplementedException();
    }

    public async Task<Atlas> CreateAtlasAsync(AtlasData atlasData)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Atlas>> GetAllAtlasesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Atlas?> GetAtlasAsync(ulong id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveCartographFromAtlasAsync(ulong id, ulong cartographId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAtlasAsync(ulong id)
    {
        throw new NotImplementedException();
    }

    #endregion IAtlasService Methods
}

/** @} */