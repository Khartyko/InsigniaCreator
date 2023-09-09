/** \addtogroup MainAppTests
 * @{
 */

using Avalonia.Controls;
using Khartyko.InsigniaCreator.MainApp.ViewModels;

namespace Khartyko.InsigniaCreator.MainApp.Testing;

internal class InvalidViewModel : ViewModelBase
{
}

public class ViewLocatorTests
{
	#region Build

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

	#endregion Build

	#region Match

	[Fact]
	public void Match_Succeeds()
	{
		var testViewModel = new TestViewModel();
		var viewLocator = new ViewLocator();
		
		Assert.True(viewLocator.Match(testViewModel));
	}

	[Fact]
	public void Match_NullObject_Fails()
	{
		object? nullObject = null;
		var viewLocator = new ViewLocator();
		
		Assert.False(viewLocator.Match(nullObject));
	}

	[Fact]
	public void Match_InvalidObject_Fails()
	{
		object? invalidObject = new object();
		var viewLocator = new ViewLocator();
		
		Assert.False(viewLocator.Match(invalidObject));
	}

	#endregion Match
}

/** @} */