using Avalonia.Controls;
using Khartyko.InsigniaCreator.MainApp.ViewModels;
namespace Khartyko.InsigniaCreator.MainApp.Testing;

internal class InvalidViewModel : ViewModelBase
{
}

public class ViewLocatorTests
{
	[Fact]
	public void Build_Succeeds()
	{
		var mainWindowViewModel = new TestViewModel();
		var viewLocator = new ViewLocator();

		Control buildResult = viewLocator.Build(mainWindowViewModel);

		Assert.Equal("TestView", buildResult.GetType().Name);
	}

	[Fact]
	public void Build_NullObject_Fails()
	{
		var viewLocator = new ViewLocator();

		Control buildResult = viewLocator.Build(null);
		
		Assert.True(buildResult is TextBlock);

		var textBlock = buildResult as TextBlock;
		
		Assert.NotNull(textBlock);
		Assert.Equal("Null object passed.", textBlock.Text);
	}

	[Fact]
	public void Build_InvalidViewModel_Fails()
	{
		var testViewModel = new InvalidViewModel();
		var viewLocator = new ViewLocator();

		Control buildResult = viewLocator.Build(testViewModel);
		
		Assert.True(buildResult is TextBlock);

		var textBlock = buildResult as TextBlock;
		
		Assert.NotNull(textBlock);

		const string expectedText = "Not Found: Khartyko.InsigniaCreator.MainApp.Testing.InvalidView";
		
		Assert.Equal(expectedText, textBlock.Text);
	}
}