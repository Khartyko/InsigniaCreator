/** \addtogroup MainApp
 * @{
 */
using Avalonia;
using Avalonia.ReactiveUI;
using System;

namespace Khartyko.InsigniaCreator.MainApp;

/// <summary>
/// 
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point for the application when the application is executed.
    ///
    /// Initialization code. Don't use any Avalonia, third-party APIs or any
    /// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    /// yet and stuff might break.
    /// </summary>
    /// <param name="args">Command-line arguments that get passed to the Avalonia App</param>
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    /// <summary>
    /// This builds the app and configures is to use various pre-made configurations
    /// </summary>
    /// <returns>An AppBuilder instance that has an Avalonia UI application with predetermined configurations</returns>
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}
/** @} */