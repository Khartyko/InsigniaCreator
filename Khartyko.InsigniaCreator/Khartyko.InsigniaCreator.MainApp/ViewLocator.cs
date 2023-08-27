/** \addtogroup MainApp
 * @{
 */
using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Khartyko.InsigniaCreator.MainApp.ViewModels;

namespace Khartyko.InsigniaCreator.MainApp;

/// <summary>
/// A utility class that handles the building of Controls from their respective ViewModels.
/// </summary>
public class ViewLocator : IDataTemplate
{
    /// <summary>
    /// Builds a Control from a ViewModel, provided the data isn't null and a proper view is created per the ViewModel.
    /// </summary>
    /// <param name="data">The ViewModel instance to base the Control off of.</param>
    /// <returns>A Control that is the View counterpart to the specified ViewModel.</returns>
    public Control Build(object? data)
    {
        if (data is null)
        {
            return new TextBlock { Text = "Null object passed." };
        }

        Type dataType = data.GetType();
        string name = dataType.AssemblyQualifiedName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found: " + dataType.FullName!.Replace("ViewModel", "View") };
    }

    /// <summary>
    /// This checks to see if an object passed in derives from ViewModelBase
    /// </summary>
    /// <param name="data">The object instance in question.</param>
    /// <returns>True if the data passed in derives from ViewModelBase; false if it is null or anything else</returns>
    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
/** @} */