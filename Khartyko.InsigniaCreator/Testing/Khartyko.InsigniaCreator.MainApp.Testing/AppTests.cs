/** \addtogroup MainAppTests
 * @{
 */

namespace Khartyko.InsigniaCreator.MainApp.Testing;

public class AppTests
{
	[Fact]
	public void Construct_Succeeds()
	{
		var app = new App();
		
		/*
		 * Considering the app isn't configured, especially with how
		 *	Avalonia UI sets it up, there's only so much that can be tested
		 */
		
		Assert.NotNull(app);
		Assert.Equal("Avalonia Application", app.Name);
	}
}

/** @} */