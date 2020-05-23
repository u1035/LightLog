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

        public static void Error(string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Error, callerMember, sourcePath, lineNumber));
        }

        public static void Warning(string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Warning, callerMember, sourcePath, lineNumber));
        }

        public static void Notice(string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Notice, callerMember, sourcePath, lineNumber));
        }

        public static void Info(string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Info, callerMember, sourcePath, lineNumber));
        }

        public static void Debug(string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Debug, callerMember, sourcePath, lineNumber));
        }

        public static void Trace(string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Trace, callerMember, sourcePath, lineNumber));
        }

        public static void AddRecord(LogLevel level, string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, level, callerMember, sourcePath, lineNumber));
        }

        public static void AddRecord(DateTime time, LogLevel level, string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(time, sender, message, level, callerMember, sourcePath, lineNumber));
        }

        private static void AddRecord(LogRecord record)
        {
            if (_logFileSet) AppendRecordToFile(record);

            LogRecords.Add(record);
        }

        private static void AppendRecordToFile(LogRecord record)
        {
            var sourceFile = (record.SourceFilePath != "") ? Path.GetFileName(record.SourceFilePath) : "";
            var formattedRecord = $"{record.Time:O}|{record.Sender}|{record.CallerMemberName}|{sourceFile}|{record.SourceFileLine}|{record.Level}|{record.Message}";
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
