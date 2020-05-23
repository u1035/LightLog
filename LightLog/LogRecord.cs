using System;

namespace LightLog
{
    public class LogRecord
    {
        public DateTime Time { get; }
        public string Sender { get; }
        public string Message { get; }
        public LogLevel Level { get; }
        public string CallerMemberName { get; }
        public string SourceFilePath { get; }
        public int SourceFileLine { get; }

        public LogRecord(DateTime time, string sender, string message, LogLevel level, string callerMemberName, string sourceFilePath, int sourceFileLine)
        {
            Time = time;
            Sender = sender;
            Message = message;
            Level = level;
            CallerMemberName = callerMemberName;
            SourceFilePath = sourceFilePath;
            SourceFileLine = sourceFileLine;
        }
    }
}
