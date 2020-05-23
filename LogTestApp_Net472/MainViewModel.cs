using LightLog;

namespace LogTestApp_Net472
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Logger.LogFileName = "test.log";
            Logger.Debug("Program started");
            
            PrintInfo();
        }

        private void PrintInfo()
        {
            Logger.Info("Information message 1");
            Logger.Info("Information message 2");
            Logger.Info("Information message 3");
            Logger.Info("Information message 4");

        }
    }
}
