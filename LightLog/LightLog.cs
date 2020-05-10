using System;
using System.Collections.ObjectModel;
using System.IO;

namespace LightLog
{
    public class LightLog
    {
        public static string LogFileName { get; private set; }
        public static ObservableCollection<LogRecord> LogRecords { get; } = new ObservableCollection<LogRecord>();

        private static StreamWriter _logFile;

        public static void InitFileLog(string logFileName = "")
        {
            if (string.IsNullOrWhiteSpace(logFileName)) return;
            CreateOrOpenLogFile(logFileName);

            LogFileName = logFileName;
        }

        public static void ClearLog()
        {
            LogRecords.Clear();
        }

        private static void CreateOrOpenLogFile(string logFileName)
        {
            try
            {
                _logFile = new StreamWriter(logFileName, true);
            }
            catch (Exception)
            {
                //ignored
            }
        }


        private static void AppendRecordToFile()
        {

        }
    }


    public sealed class Logger
    {
        private static readonly LightLog _instance = new LightLog();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Logger()
        {
        }

        private Logger()
        {
        }

        public static LightLog Instance => _instance;
    }
}
