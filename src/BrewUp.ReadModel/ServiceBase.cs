using Microsoft.Extensions.Logging;

namespace BrewUp.ReadModel;

public abstract class ServiceBase
{
    protected readonly ILogger Logger;

    protected ServiceBase(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger(GetType());
    }
}