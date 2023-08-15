namespace Khartyko.InsigniaCreator.Library.Interfaces.Logging;

public interface ILoggingService
{
    Task LogAsync(ILoggable loggable);
}