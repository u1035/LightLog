using System;

namespace LightLog
{
    public class LogRecord
    {
        public DateTime Time { get; }
        public string Sender { get; }
        public string Message { get; }
        public LogLevel Level { get; }

        public LogRecord(DateTime time, string sender, string message, LogLevel level)
        {
            Time = time;
            Sender = sender;
            Message = message;
            Level = level;
        }
    }
}
