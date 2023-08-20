using Khartyko.InsigniaCreator.Library.Interfaces;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Library;

public static class Archon
{
    public static IArchivist? Archivist { get; private set; } = null;

    public static void Initialize(IArchivist archivist)
    {
        ObjectHelper.NullCheck(archivist, nameof(archivist));
        
        if (Archivist is not null)
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(Archon),
                nameof(archivist),
                $"Archivist has already been initialized"
            );
        }

        Archivist = archivist;
    }

    public static void Shutdown()
    {
        if (Archivist is null)
        {
            throw new ApplicationException("Archivist cannot be finalized, as it hasn't been initialized yet");
        }
        
        Archivist = null;
    }
}