using JetBrains.Annotations;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace LightLog
{
    public static class Logger
    {
        private static bool _logFileSet;

        private static string _logFileName;
        public static string LogFileName
        {
            get => _logFileName;
            set
            {
                _logFileName = value;
                _logFileSet = FileNameCorrect(value);
            }
        }

        [UsedImplicitly]
        public static ObservableCollection<LogRecord> LogRecords { get; } = new ObservableCollection<LogRecord>();


        public static void ClearLog()
        {
            LogRecords.Clear();
        }

        public static void Error(string message, [CallerMemberName] string sender = "")
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Error));
        }

        public static void Warning(string message, [CallerMemberName] string sender = "")
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Warning));
        }

        public static void Notice(string message, [CallerMemberName] string sender = "")
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Notice));
        }

        public static void Info(string message, [CallerMemberName] string sender = "")
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Info));
        }

        public static void Debug(string message, [CallerMemberName] string sender = "")
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Debug));
        }

        public static void Trace(string message, [CallerMemberName] string sender = "")
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Trace));
        }

        public static void AddRecord(LogLevel level, string message, [CallerMemberName] string sender = "")
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, level));
        }

        public static void AddRecord(DateTime time, LogLevel level, string message, [CallerMemberName] string sender = "")
        {
            AddRecord(new LogRecord(time, sender, message, level));
        }

        private static void AddRecord(LogRecord record)
        {
            if (_logFileSet) AppendRecordToFile(record);

            LogRecords.Add(record);
        }

        private static void AppendRecordToFile(LogRecord record)
        {
            var formattedRecord = $"{record.Time}|{record.Sender}|{record.Level}|{record.Message}";
            try
            {
                File.AppendAllText(LogFileName, $"{formattedRecord}" + Environment.NewLine);
            }
            catch (Exception)
            { /*ignored*/ }
        }



        private static bool FileNameCorrect(string filename)
        {
            try
            {
                if (filename.IndexOf(Path.DirectorySeparatorChar) != -1)
                {
                    //path contains "\" (for Windows) - so assume it's absolute path, let's try to create directory
                    Directory.CreateDirectory(Path.GetFullPath(filename));
                }

                File.AppendAllText(filename, GetLogInitMessage());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static string GetLogInitMessage()
        {
            return $"{DateTime.Now:O} - Log file initialized" + Environment.NewLine;
        }
    }
}
