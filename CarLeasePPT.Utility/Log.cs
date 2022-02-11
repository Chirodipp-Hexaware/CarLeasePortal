using NLog;

namespace CarLeasePPT.Utility
{
    public static class Log
    {
        #region Constructors and Destructors

        static Log()
        {
            LogManager.ReconfigExistingLoggers();
            Instance = LogManager.GetLogger("ErrorLog");
        }

        #endregion

        #region Public Properties

        public static Logger Instance { get; }

        #endregion
    }
}