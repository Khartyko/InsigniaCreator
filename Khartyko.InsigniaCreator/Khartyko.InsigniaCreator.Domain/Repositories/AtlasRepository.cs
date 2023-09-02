/** \addtogroup Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Interface;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Domain.Repositories;

public class AtlasRepository : IRepository<AtlasData, Atlas>
{
	public async Task<Atlas?> RetrieveAsync(long id)
	{
		throw new NotImplementedException();
	}
	
	public async Task<IEnumerable<Atlas>> RetrieveAllAsync()
	{
		throw new NotImplementedException();
	}
	
	public async Task<Atlas> CreateAsync(AtlasData data)
	{
		throw new NotImplementedException();
	}
	
	public async Task<bool> UpdateAsync(long id, AtlasData data)
	{
		throw new NotImplementedException();
	}
	
	public async Task<bool> DeleteAsync(long id)
	{
		throw new NotImplementedException();
	}
}

/** @} */