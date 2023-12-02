/** \addtogroup MainApp
 * @{
 */

using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Khartyko.InsigniaCreator.MainApp.ViewModels.AtlasCreation;

namespace Khartyko.InsigniaCreator.MainApp.Views.AtlasCreation;

/// <summary>
/// Code-behind for the AtlasCreationView.xaml file.
/// </summary>
public partial class AtlasCreationView : ReactiveUserControl<AtlasCreationViewModel>
{
	/// <summary>
	/// Default constructor that calls "InitializeComponents".
	/// </summary>
	public AtlasCreationView()
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