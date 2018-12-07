namespace PingerManager.Logging
{
    public interface ILoggerFactory
    {
        ILogger Logger { get; }
        void AddLoggerProvider(ILoggerProvider loggerProvider);
    }
}
