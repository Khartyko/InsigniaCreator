namespace Khartyko.InsigniaCreator.Library.Utility.Helpers;

public static class ObjectHelper
{
    public static void NullCheck(object target, string name)
    {
        if (target is null)
        {
            throw new ArgumentNullException(name, $"{name} is null");
        }
    }
}