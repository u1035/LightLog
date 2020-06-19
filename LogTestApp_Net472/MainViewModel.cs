using LightLog;

namespace LogTestApp_Net472
{
    public class MainViewModel
    {
        public Logger Logger { get; } = LogManager.Instance;

        public MainViewModel()
        {
            Logger.LogFileName = "test.log";
            Logger.NewRecordsFirst = true;

            Logger.Debug("Program started");
            
            PrintInfo();
        }

        private void PrintInfo()
        {
            for (int i = 0; i < 10000; i++)
            {
                Logger.Info($"Information message {i}");
                
                if (i == 5000) 
                    Logger.LogFileName = "test2.log";
            }
        }
    }
}
