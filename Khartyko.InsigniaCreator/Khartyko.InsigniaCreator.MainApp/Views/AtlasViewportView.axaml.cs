/** \addtogroup MainApp
 * @{
 */

using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Khartyko.InsigniaCreator.MainApp.ViewModels;

namespace Khartyko.InsigniaCreator.MainApp.Views;

public partial class AtlasViewportView : ReactiveUserControl<AtlasViewportViewModel>
{
	public AtlasViewportView()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		AvaloniaXamlLoader.Load(this);
	}
}

/** @} */