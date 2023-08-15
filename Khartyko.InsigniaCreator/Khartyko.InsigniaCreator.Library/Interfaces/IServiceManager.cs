using Khartyko.InsigniaCreator.Library.Interfaces.Logging;
namespace Khartyko.InsigniaCreator.Library.Interfaces;

public interface IServiceManager
{
    ILoggingService? GetLoggingService();

    bool Register(ILoggingService loggingService);
    
    bool Register(IService service);
    bool Unregister(IService service);
    
    TService? GetService<TService>() where TService : IService;
}