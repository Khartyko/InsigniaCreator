/** \addtogroup MainApp
 * @{
 */

using ReactiveUI;

namespace Khartyko.InsigniaCreator.MainApp.ViewModels;

/// <summary>
/// Boilerplate view model that is used for the MainWindow's context
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    private double _width;
    private double _height;

    public double Width
    {
        get => _width;
        set => this.RaiseAndSetIfChanged(ref _width, value);
    }

    public double Height
    {
        get => _height;
        set => this.RaiseAndSetIfChanged(ref _height, value);
    }

    public MainWindowViewModel()
    {
        _width = 1600;
        _height = 960;
    }
}

/** @} */