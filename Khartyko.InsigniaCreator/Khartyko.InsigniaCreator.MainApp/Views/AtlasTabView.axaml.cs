/** \addtogroup MainApp
 * @{
 */

using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Khartyko.InsigniaCreator.MainApp.ViewModels;

namespace Khartyko.InsigniaCreator.MainApp.Views;

/// <summary>
/// Code-behind for the AtlasTabView.xaml file.
/// </summary>
public partial class AtlasTabView : ReactiveUserControl<AtlasTabViewModel>
{
	/// <summary>
	/// Default constructor that calls "InitializeComponents".
	/// </summary>
	public AtlasTabView()
	{
		InitializeComponent();
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