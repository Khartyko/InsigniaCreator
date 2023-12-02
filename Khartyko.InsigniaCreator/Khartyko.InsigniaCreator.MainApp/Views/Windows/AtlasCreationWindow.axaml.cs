/** \addtogroup MainApp
 * @{
 */

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Khartyko.InsigniaCreator.MainApp.Views.Windows;

/// <summary>
/// Code-behind for the AtlasCreationView.xaml file.
/// </summary>
public partial class AtlasCreationWindow : Window
{
	/// <summary>
	/// Default constructor that calls "InitializeComponents".
	/// </summary>
	/// <remarks>
	/// If the application is ran as "Debug", then "AttachDevTools()" is called.
	/// </remarks>
	public AtlasCreationWindow()
	{
		InitializeComponent();
#if DEBUG
		this.AttachDevTools();
#endif
	}

	/// <summary>
	/// Loads the XAML for this view.
	/// </summary>
	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}
}

/** @} */