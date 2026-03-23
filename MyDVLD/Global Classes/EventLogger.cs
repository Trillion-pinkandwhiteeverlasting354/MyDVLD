using System;
using System.Diagnostics;

namespace MyDVLD.Global_Classes
{
    public static class EventLogger
    {
        private const string SourceName = "DVLD Application";
        private const string LogName = "Application";

        static EventLogger()
        {
            try
            {
                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, LogName);
                }
            }
            catch
            {
                // NEVER throw from logger
                // Logging must not crash the app
            }
        }

        public static void LogException(Exception ex)
        {
            try
            {
                string message =
                    $"Message: {ex.Message}\n\n" +
                    $"Stack Trace:\n{ex.StackTrace}";

                EventLog.WriteEntry(SourceName, message, EventLogEntryType.Error);
            }
            catch
            {
                // Swallow exception – logger must never break the app
            }
        }
    }
}
