using JetBrains.Annotations;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace LightLog
{
    /// <summary>
    /// Provides functions for writing log messages
    /// </summary>
    public class Logger : IDisposable
    {
        private bool _logFileSet;
        private StreamWriter _logFileStream;
        private string _logFileName;

        #region Public fields

        /// <summary>
        /// Gets or sets log file name
        /// </summary>
        public string LogFileName
        {
            get => _logFileName;
            set
            {
                _logFileName = value;
                _logFileSet = FileNameCorrect(value);
                if (_logFileSet)
                {
                    if (_logFileStream != null)
                    {
                        _logFileStream.Flush();
                        _logFileStream.Close();
                    }
                    _logFileStream = File.AppendText(_logFileName);
                }
            }
        }

        /// <summary>
        /// Observable collection of log records
        /// </summary>
        [UsedImplicitly]
        public ObservableCollection<LogRecord> LogRecords { get; }

        #endregion

        #region Constructor

        internal Logger()
        {
            LogRecords = new ObservableCollection<LogRecord>();
        }

        #endregion

        #region Log records adding

        /// <summary>
        /// Clears <see cref="LogRecords"/> collection (file log not clearing)
        /// </summary>
        public void ClearLog()
        {
            LogRecords.Clear();
        }

        /// <summary>
        /// Adds fatal error message to log
        /// </summary>
        /// <param name="message">Message text</param>
        /// <param name="sender">Sender name (can be anything - name of module or function)</param>
        /// <param name="callerMember">Caller member</param>
        /// <param name="sourcePath">Caller source file</param>
        /// <param name="lineNumber">Caller source file line</param>
        public void Fatal(string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Fatal, callerMember, sourcePath, lineNumber));
        }

        /// <summary>
        /// Adds error message to log
        /// </summary>
        /// <param name="message">Message text</param>
        /// <param name="sender">Sender name (can be anything - name of module or function)</param>
        /// <param name="callerMember">Caller member</param>
        /// <param name="sourcePath">Caller source file</param>
        /// <param name="lineNumber">Caller source file line</param>
        public void Error(string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Error, callerMember, sourcePath, lineNumber));
        }

        /// <summary>
        /// Adds warning message to log
        /// </summary>
        /// <param name="message">Message text</param>
        /// <param name="sender">Sender name (can be anything - name of module or function)</param>
        /// <param name="callerMember">Caller member</param>
        /// <param name="sourcePath">Caller source file</param>
        /// <param name="lineNumber">Caller source file line</param>
        public void Warning(string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Warning, callerMember, sourcePath, lineNumber));
        }

        /// <summary>
        /// Adds notice message to log
        /// </summary>
        /// <param name="message">Message text</param>
        /// <param name="sender">Sender name (can be anything - name of module or function)</param>
        /// <param name="callerMember">Caller member</param>
        /// <param name="sourcePath">Caller source file</param>
        /// <param name="lineNumber">Caller source file line</param>
        public void Notice(string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Notice, callerMember, sourcePath, lineNumber));
        }

        /// <summary>
        /// Adds info message to log
        /// </summary>
        /// <param name="message">Message text</param>
        /// <param name="sender">Sender name (can be anything - name of module or function)</param>
        /// <param name="callerMember">Caller member</param>
        /// <param name="sourcePath">Caller source file</param>
        /// <param name="lineNumber">Caller source file line</param>
        public void Info(string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Info, callerMember, sourcePath, lineNumber));
        }

        /// <summary>
        /// Adds debug message to log
        /// </summary>
        /// <param name="message">Message text</param>
        /// <param name="sender">Sender name (can be anything - name of module or function)</param>
        /// <param name="callerMember">Caller member</param>
        /// <param name="sourcePath">Caller source file</param>
        /// <param name="lineNumber">Caller source file line</param>
        public void Debug(string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Debug, callerMember, sourcePath, lineNumber));
        }

        /// <summary>
        /// Adds trace message to log
        /// </summary>
        /// <param name="message">Message text</param>
        /// <param name="sender">Sender name (can be anything - name of module or function)</param>
        /// <param name="callerMember">Caller member</param>
        /// <param name="sourcePath">Caller source file</param>
        /// <param name="lineNumber">Caller source file line</param>
        public void Trace(string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, LogLevel.Trace, callerMember, sourcePath, lineNumber));
        }

        /// <summary>
        /// Adds message to log
        /// </summary>
        /// <param name="level">Severity level of log message</param>
        /// <param name="message">Message text</param>
        /// <param name="sender">Sender name (can be anything - name of module or function)</param>
        /// <param name="callerMember">Caller member</param>
        /// <param name="sourcePath">Caller source file</param>
        /// <param name="lineNumber">Caller source file line</param>
        public void AddRecord(LogLevel level, string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(DateTime.Now, sender, message, level, callerMember, sourcePath, lineNumber));
        }

        /// <summary>
        /// Adds error message to log
        /// </summary>
        /// ///
        /// <param name="time">Time of log message</param>
        /// <param name="level">Severity level of log message</param>
        /// <param name="message">Message text</param>
        /// <param name="sender">Sender name (can be anything - name of module or function)</param>
        /// <param name="callerMember">Caller member</param>
        /// <param name="sourcePath">Caller source file</param>
        /// <param name="lineNumber">Caller source file line</param>
        public void AddRecord(DateTime time, LogLevel level, string message, string sender = "", [CallerMemberName] string callerMember = "", [CallerFilePath] string sourcePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            AddRecord(new LogRecord(time, sender, message, level, callerMember, sourcePath, lineNumber));
        }

        private void AddRecord(LogRecord record)
        {
            if (_logFileSet) AppendRecordToFile(record);

            LogRecords.Add(record);
        }

        #endregion

        #region IO operations

        private void AppendRecordToFile(LogRecord record)
        {
            var sourceFile = (record.SourceFilePath != "") ? Path.GetFileName(record.SourceFilePath) : "";
            var formattedRecord = $"{record.Time:O}|{record.Sender}|{record.CallerMemberName}|{sourceFile}|{record.SourceFileLine}|{record.Level}|{record.Message}";
            try
            {
                _logFileStream.WriteLine($"{formattedRecord}");
                _logFileStream.Flush();
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

        #endregion

        #region Implementation of IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            _logFileStream?.Dispose();
        }
        
        #endregion

    }
}
