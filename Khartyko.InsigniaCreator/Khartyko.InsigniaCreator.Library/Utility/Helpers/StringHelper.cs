namespace Khartyko.InsigniaCreator.Library.Utility.Helpers;

public static class StringHelper
{
    public static void NullCheck(string target, string name)
    {
        if (target is null)
        {
            throw new ArgumentNullException(name, $"{name} is null");
        }
    }

    public static void EmptyOrWhitespaceCheck(string target, string name)
    {
        if (string.IsNullOrWhiteSpace(target))
        {
            throw new ArgumentException($"{name} is null or whitespace", name);
        }
    }
}