using Avalonia;

namespace Khartyko.InsigniaCreator.MainApp.Testing;

public class ProgramTests
{
	[Fact]
	public void BuildAvaloniaApp_Succeeds()
	{
		AppBuilder appBuilder = Program.BuildAvaloniaApp();
		
		/*
		 * As is the case in multiple spots, the core of this
		 *	project cannot be tested, but here's what I can,
		 *	that I know of.
		 */
		
		Assert.NotNull(appBuilder);
		Assert.NotNull(appBuilder.ApplicationType);
		Assert.Equal(typeof(App), appBuilder.ApplicationType);
	}
}