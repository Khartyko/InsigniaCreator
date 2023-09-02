/** \addtogroup MainApp
 * @{
 */

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Khartyko.InsigniaCreator.MainApp.Views.Windows;

public partial class AtlasCreationWindow : Window
{
	public AtlasCreationWindow()
	{
		InitializeComponent();
#if DEBUG
		this.AttachDevTools();
#endif
	}

	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}
}

/** @} */