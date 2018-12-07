namespace PingerManager.Logging
{
    public interface ILoggerFactory
    {
        void AddLoggerProvider(ILoggerProvider loggerProvider);
        Logger CreateLogger();
    }
}
