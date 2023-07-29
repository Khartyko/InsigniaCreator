using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Khartyko.InsigniaCreator.MainApp.ViewModels;

namespace Khartyko.InsigniaCreator.MainApp;

public class ViewLocator : IDataTemplate
{
    public Control Build(object? data)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data), "ViewLocator::Build(>data<); 'data' is null");
        }
        
        var name = data.GetType().FullName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}