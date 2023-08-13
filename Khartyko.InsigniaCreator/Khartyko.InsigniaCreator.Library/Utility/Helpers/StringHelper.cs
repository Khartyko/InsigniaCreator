namespace Khartyko.InsigniaCreator.Library.Utility.Helpers;

public static class StringHelper
{
    public static void EmptyOrWhitespaceCheck(string target, string name)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name), $"{nameof(name)} is null");
        }
        
        if (target is null)
        {
            throw new ArgumentNullException(name, $"{name} is null");
        }
        
        if (string.IsNullOrWhiteSpace(target))
        {
            throw new ArgumentException($"{name} is null or whitespace", name);
        }
    }
}