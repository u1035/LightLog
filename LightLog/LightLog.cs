namespace LightLog
{
    public static class LightLog
    {
        public static string LogFileName { get; private set; }

        public static void InitLog(string logFileName = "")
        {
            LogFileName = logFileName;
        }
    }
}
