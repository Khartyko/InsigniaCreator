/** \addtogroup MainApp
 * @{
 */

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Khartyko.InsigniaCreator.MainApp.ViewModels;
using Khartyko.InsigniaCreator.MainApp.Views;

namespace Khartyko.InsigniaCreator.MainApp;

/// <summary>
/// Base application that is used by the Program.cs file
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Override of initialize so its XAML is loaded
    /// </summary>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    /// Override that creates the MainWindow and sets its context to its appropriate view model
    /// </summary>
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}

/** @} */