/** \addtogroup MainAppTests
 * @{
 */

using Khartyko.InsigniaCreator.MainApp.ViewModels;

namespace Khartyko.InsigniaCreator.MainApp.Testing;

public class MainWindowViewModelTests
{
	[Fact]
	public void Construct_Succeeds()
	{
		var mainWindowViewModel = new MainWindowViewModel();
		
		Assert.NotNull(mainWindowViewModel);
	}
}

/** @} */