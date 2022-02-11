using NLog;

namespace CarLeasePPT.Utility
{
    public static class Audit
    {
        #region Constructors and Destructors

        static Audit()
        {
            LogManager.ReconfigExistingLoggers();
            Instance = LogManager.GetLogger("AuditLog");
        }

        #endregion

        #region Public Properties

        public static Logger Instance { get; }

        #endregion
    }
}