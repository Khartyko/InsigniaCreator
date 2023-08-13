using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Khartyko.InsigniaCreator.MainApp.Testing;

public partial class TestView : UserControl
{
	public TestView()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}
}