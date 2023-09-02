/** \addtogroup MainApp
 * @{
 */

using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Khartyko.InsigniaCreator.MainApp.ViewModels.AtlasCreation;

namespace Khartyko.InsigniaCreator.MainApp.Views.AtlasCreation;

public partial class AtlasCreationView : ReactiveUserControl<AtlasCreationViewModel>
{
	public AtlasCreationView()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}
}

/** @} */